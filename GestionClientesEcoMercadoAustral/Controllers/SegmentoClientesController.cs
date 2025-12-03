using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionClientesEcoMercadoAustral.Data;
using GestionClientesEcoMercadoAustral.Models;

namespace GestionClientesEcoMercadoAustral.Controllers
{
    public class SegmentoClientesController : Controller
    {
        private readonly DataContext _context;

        public SegmentoClientesController(DataContext context)
        {
            _context = context;
        }

        // GET: SegmentoClientes
        public async Task<IActionResult> Index()
        {
            return View(await _context.SegmentosCliente.ToListAsync());
        }

        // GET: SegmentoClientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var segmentoCliente = await _context.SegmentosCliente
                .FirstOrDefaultAsync(m => m.SegmentoClienteId == id);

            if (segmentoCliente == null)
                return NotFound();

            return View(segmentoCliente);
        }

        // GET: SegmentoClientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SegmentoClientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SegmentoClienteId,NombreSegmento,Descripcion,Estado")] SegmentoCliente segmentoCliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(segmentoCliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(segmentoCliente);
        }

        // GET: SegmentoClientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var segmentoCliente = await _context.SegmentosCliente.FindAsync(id);
            if (segmentoCliente == null)
                return NotFound();

            return View(segmentoCliente);
        }

        // POST: SegmentoClientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SegmentoClienteId,NombreSegmento,Descripcion,Estado")] SegmentoCliente segmentoCliente)
        {
            if (id != segmentoCliente.SegmentoClienteId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(segmentoCliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SegmentoClienteExists(segmentoCliente.SegmentoClienteId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(segmentoCliente);
        }

        // POST: SegmentoClientes/Delete (AJAX SweetAlert)
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var segmentoCliente = await _context.SegmentosCliente.FindAsync(id);

            if (segmentoCliente == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Segmento no encontrado."
                });
            }

            // Verificar si tiene clientes asociados
            bool tieneClientes = await _context.Clientes
                .AnyAsync(c => c.SegmentoClienteId == id);

            if (tieneClientes)
            {
                return Json(new
                {
                    success = false,
                    message = "No se puede eliminar el segmento porque tiene clientes asociados."
                });
            }

            _context.SegmentosCliente.Remove(segmentoCliente);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        private bool SegmentoClienteExists(int id)
        {
            return _context.SegmentosCliente.Any(e => e.SegmentoClienteId == id);
        }
    }
}
