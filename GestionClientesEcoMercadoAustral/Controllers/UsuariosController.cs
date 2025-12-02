using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionClientesEcoMercadoAustral.Data;
using GestionClientesEcoMercadoAustral.Models;

namespace GestionClientesEcoMercadoAustral.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly DataContext _context;

        public UsuariosController(DataContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.UsuarioId == id);

            if (usuario == null)
                return NotFound();

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsuarioId,Nombre,Apellido1,Apellido2,Rut,Username,Password,Rol")] Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Existen errores en el formulario.";
                return View(usuario);
            }

            // Validar Username único
            bool usernameExiste = await _context.Usuarios
                .AnyAsync(u => u.Username == usuario.Username);

            if (usernameExiste)
            {
                ModelState.AddModelError("Username", "El nombre de usuario ya está en uso.");
                TempData["Error"] = "El nombre de usuario ya está en uso.";
                return View(usuario);
            }

            _context.Add(usuario);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Usuario creado exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                return NotFound();

            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UsuarioId,Nombre,Apellido1,Apellido2,Rut,Username,Password,Rol")] Usuario usuario)
        {
            if (id != usuario.UsuarioId)
                return NotFound();

            var original = await _context.Usuarios.AsNoTracking()
                .FirstOrDefaultAsync(u => u.UsuarioId == id);

            if (original == null)
                return NotFound();

            // Username duplicado
            bool usernameExiste = await _context.Usuarios
                .AnyAsync(u => u.Username == usuario.Username && u.UsuarioId != id);

            if (usernameExiste)
            {
                TempData["Error"] = "El nombre de usuario ya está asignado a otro usuario.";
                ModelState.AddModelError("Username", "Este nombre de usuario ya existe.");
                return View(usuario);
            }

            // Manejo de contraseña
            if (string.IsNullOrWhiteSpace(usuario.Password))
            {
                usuario.Password = original.Password;
                ModelState.Remove("Password");
            }

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Existen errores en la edición.";
                return View(usuario);
            }

            try
            {
                _context.Update(usuario);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Usuario actualizado correctamente.";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Usuarios.Any(e => e.UsuarioId == id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.UsuarioId == id);

            if (usuario == null)
                return NotFound();

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Usuario eliminado correctamente.";
            }
            else
            {
                TempData["Error"] = "No se pudo eliminar el usuario.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
