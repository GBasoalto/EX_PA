using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionClientesEcoMercadoAustral.Data;

public class ReportesController : Controller
{
    private readonly DataContext _context;

    public ReportesController(DataContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // --- REPORTE POR COMUNA ---
        var datos = await _context.Clientes
            .Include(c => c.Comuna)
            .Select(c => new
            {
                NombreComuna = c.Comuna != null ? c.Comuna.Nombre : "Sin comuna"
            })
            .ToListAsync();

        var reporteComunas = datos
            .GroupBy(c => c.NombreComuna)
            .Select(g => new ReporteComuna
            {
                Comuna = g.Key,
                Cantidad = g.Count()
            })
            .OrderBy(r => r.Comuna)
            .ToList();


        // --- REPORTE POR USUARIO ---
        var reporteUsuarios = await _context.Clientes
            .Include(c => c.Usuario)
            .GroupBy(c => new
            {
                Nombre = c.Usuario != null ? c.Usuario.Nombre : "Sin usuario",
                Apellido = c.Usuario != null ? c.Usuario.Apellido1 : "",
                Rut = c.Usuario != null ? c.Usuario.Rut : "N/A"
            })
            .Select(g => new ReporteUsuario
            {
                Usuario = $"{g.Key.Nombre} {g.Key.Apellido} ({g.Key.Rut})",
                Cantidad = g.Count()
            })
            .OrderByDescending(r => r.Cantidad)
            .ToListAsync();

        ViewBag.ReporteUsuarios = reporteUsuarios;

        return View(reporteComunas);
    }
}

public class ReporteComuna
{
    public string Comuna { get; set; }
    public int Cantidad { get; set; }
}

public class ReporteUsuario
{
    public string Usuario { get; set; }
    public int Cantidad { get; set; }
}
