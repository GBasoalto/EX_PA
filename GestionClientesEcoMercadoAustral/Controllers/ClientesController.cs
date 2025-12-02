using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionClientesEcoMercadoAustral.Data;
using GestionClientesEcoMercadoAustral.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace GestionClientesEcoMercadoAustral.Controllers
{
    public class ClientesController : Controller
    {
        private readonly DataContext _context;
        private readonly ILogger<ClientesController> _logger;

        public ClientesController(DataContext context, ILogger<ClientesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            var rol = HttpContext.Session.GetString("Rol");

            if (usuarioId == null)
            {
                TempData["Error"] = "Debe iniciar sesión para ver los clientes.";
                return RedirectToAction("Index", "Login");
            }

            IQueryable<Cliente> clientesQuery = _context.Clientes
                .Include(c => c.Usuario)
                .Include(c => c.Comuna)
                .ThenInclude(co => co.Region)
                .Include(c => c.SegmentoCliente);

            if (rol != "Administrador")
                clientesQuery = clientesQuery.Where(c => c.UsuarioId == usuarioId);

            var clientes = await clientesQuery.ToListAsync();
            ViewBag.RolUsuario = rol;
            ViewBag.UsuarioId = usuarioId;

            return View(clientes);
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var cliente = await _context.Clientes
                .Include(c => c.Comuna)
                .ThenInclude(co => co.Region)
                .Include(c => c.SegmentoCliente)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.ClienteId == id);

            if (cliente == null) return NotFound();

            var rol = HttpContext.Session.GetString("Rol");
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (rol != "Administrador" && cliente.UsuarioId != usuarioId)
                return Forbid();

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            ViewData["Regiones"] = new SelectList(_context.Regiones, "RegionId", "Nombre");
            ViewData["ComunaId"] = new SelectList(Enumerable.Empty<Comuna>(), "ComunaId", "Nombre");
            ViewData["SegmentoClienteId"] = new SelectList(_context.SegmentosCliente, "SegmentoClienteId", "NombreSegmento");
            return View();
        }

        // POST: Clientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClienteId,Rut,Nombre,TipoCliente,Apellido1,Apellido2,Direccion,ComunaId,SegmentoClienteId")] Cliente cliente)
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
            {
                TempData["Error"] = "Debe iniciar sesión para crear clientes.";
                return RedirectToAction("Index", "Login");
            }

            cliente.UsuarioId = usuarioId.Value;

            if (!string.IsNullOrWhiteSpace(cliente.TipoCliente) && cliente.TipoCliente.Trim() == "Empresa")
            {
                cliente.Apellido1 = string.Empty;
                cliente.Apellido2 = string.Empty;
            }

            if (cliente.ComunaId == 0)
                ModelState.AddModelError("ComunaId", "Seleccione una comuna.");
            if (cliente.SegmentoClienteId == 0)
                ModelState.AddModelError("SegmentoClienteId", "Seleccione un segmento.");

            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Cliente creado exitosamente.";
                return RedirectToAction(nameof(Index));
            }

            int regionId = cliente.ComunaId != 0
                ? _context.Comunas.Where(c => c.ComunaId == cliente.ComunaId).Select(c => c.RegionId).FirstOrDefault()
                : 0;

            ViewData["Regiones"] = new SelectList(_context.Regiones, "RegionId", "Nombre", regionId);
            ViewData["ComunaId"] = new SelectList(_context.Comunas.Where(c => c.RegionId == regionId), "ComunaId", "Nombre", cliente.ComunaId);
            ViewData["SegmentoClienteId"] = new SelectList(_context.SegmentosCliente, "SegmentoClienteId", "NombreSegmento", cliente.SegmentoClienteId);

            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var cliente = await _context.Clientes
                .Include(c => c.Comuna)
                .FirstOrDefaultAsync(c => c.ClienteId == id);
            if (cliente == null) return NotFound();

            var rol = HttpContext.Session.GetString("Rol");
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (rol != "Administrador" && cliente.UsuarioId != usuarioId)
                return Forbid();

            int regionId = cliente.Comuna?.RegionId ?? 0;
            ViewData["Regiones"] = new SelectList(_context.Regiones, "RegionId", "Nombre", regionId);
            ViewData["ComunaId"] = new SelectList(_context.Comunas.Where(c => c.RegionId == regionId), "ComunaId", "Nombre", cliente.ComunaId);
            ViewData["SegmentoClienteId"] = new SelectList(_context.SegmentosCliente, "SegmentoClienteId", "NombreSegmento", cliente.SegmentoClienteId);

            // Usuarios solo para administrador (mostrar RUT + Nombre)
            if (rol == "Administrador")
            {
                ViewData["UsuarioId"] = new SelectList(
                    _context.Usuarios
                        .Select(u => new { u.UsuarioId, Display = u.Rut + " - " + u.Nombre })
                        .ToList(),
                    "UsuarioId",
                    "Display",
                    cliente.UsuarioId
                );
            }

            ViewBag.RolUsuario = rol;

            return View(cliente);
        }

        // POST: Clientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClienteId,Rut,Nombre,TipoCliente,Apellido1,Apellido2,Direccion,ComunaId,SegmentoClienteId")] Cliente cliente)
        {
            if (id != cliente.ClienteId) return NotFound();

            var rol = HttpContext.Session.GetString("Rol");
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            var clienteExistente = await _context.Clientes.AsNoTracking().FirstOrDefaultAsync(c => c.ClienteId == id);
            if (clienteExistente == null) return NotFound();
            if (rol != "Administrador" && clienteExistente.UsuarioId != usuarioId)
                return Forbid();

            if (ModelState.IsValid)
            {
                try
                {
                    if (cliente.TipoCliente == "Empresa")
                    {
                        cliente.Apellido1 = string.Empty;
                        cliente.Apellido2 = string.Empty;
                    }
                    cliente.UsuarioId = clienteExistente.UsuarioId;
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cliente actualizado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Clientes.Any(e => e.ClienteId == id)) return NotFound();
                    throw;
                }
            }

            int regionId = cliente.ComunaId != 0
                ? _context.Comunas.Where(c => c.ComunaId == cliente.ComunaId).Select(c => c.RegionId).FirstOrDefault()
                : 0;

            ViewData["Regiones"] = new SelectList(_context.Regiones, "RegionId", "Nombre", regionId);
            ViewData["ComunaId"] = new SelectList(_context.Comunas.Where(c => c.RegionId == regionId), "ComunaId", "Nombre", cliente.ComunaId);
            ViewData["SegmentoClienteId"] = new SelectList(_context.SegmentosCliente, "SegmentoClienteId", "NombreSegmento", cliente.SegmentoClienteId);

            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var cliente = await _context.Clientes
                .Include(c => c.Comuna)
                .ThenInclude(co => co.Region)
                .Include(c => c.SegmentoCliente)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.ClienteId == id);

            if (cliente == null)
                return NotFound();

            var rol = HttpContext.Session.GetString("Rol");
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (rol != "Administrador" && cliente.UsuarioId != usuarioId)
                return Forbid();

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                return NotFound();

            var rol = HttpContext.Session.GetString("Rol");
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (rol != "Administrador" && cliente.UsuarioId != usuarioId)
                return Forbid();

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Cliente eliminado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        // AJAX: obtener comunas según región
        public JsonResult GetComunas(int regionId)
        {
            var comunas = _context.Comunas
                .Where(c => c.RegionId == regionId)
                .Select(c => new { c.ComunaId, c.Nombre })
                .ToList();

            return Json(comunas);
        }
    }
}
