using GestionClientesEcoMercadoAustral.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


public class HomeController : Controller
{
    private readonly DataContext _context;

    public HomeController(DataContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        // Total de clientes
        int totalClientes = _context.Clientes.Count();

        // Último cliente agregado
        var ultimoCliente = _context.Clientes
            .OrderByDescending(c => c.ClienteId)
            .Select(c => new { c.Nombre, c.Apellido1 })
            .FirstOrDefault();

        // Usuario con más clientes
        var usuarioTop = _context.Clientes
            .GroupBy(c => new { c.Usuario.UsuarioId, c.Usuario.Nombre, c.Usuario.Apellido1 })
            .Select(g => new
            {
                NombreCompleto = g.Key.Nombre + " " + g.Key.Apellido1,
                Cantidad = g.Count()
            })
            .OrderByDescending(x => x.Cantidad)
            .FirstOrDefault();

        // Clientes por región (para gráfico)
        var clientesPorRegion = _context.Clientes
            .Include(c => c.Comuna)
            .ThenInclude(c => c.Region)
            .GroupBy(c => c.Comuna.Region.Nombre)
            .Select(g => new
            {
                Region = g.Key,
                Cantidad = g.Count()
            })
            .ToList();

        ViewBag.TotalClientes = totalClientes;
        ViewBag.UltimoCliente = ultimoCliente;
        ViewBag.UsuarioTop = usuarioTop;
        ViewBag.ClientesPorRegion = clientesPorRegion;


        // Clientes por tipo
        var clientesPorTipo = _context.Clientes
            .GroupBy(c => c.TipoCliente)
            .Select(g => new
            {
                Tipo = g.Key,
                Cantidad = g.Count()
            })
            .ToList();

        ViewBag.ClientesPorTipo = clientesPorTipo;


        return View();
    }


    // En HomeController.cs
    public IActionResult DashboardVendedor()
    {
        // Verificar que esté logueado
        var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
        if (!usuarioId.HasValue)
        {
            return RedirectToAction("Index", "Login");
        }

        // Verificar que sea Vendedor
        var rol = HttpContext.Session.GetString("Rol");
        if (rol != "Vendedor")
        {
            // Redirigir al dashboard correspondiente según su rol
            return rol == "Administrador"
                ? RedirectToAction("Index", "Home")
                : RedirectToAction("Index", "Login");
        }

        // Tu lógica para el dashboard del vendedor
        return View();
    }

}
