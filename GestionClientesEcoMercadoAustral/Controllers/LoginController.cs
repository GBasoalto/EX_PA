using GestionClientesEcoMercadoAustral.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionClientesEcoMercadoAustral.Controllers
{
    public class LoginController : Controller
    {
        private readonly DataContext _context;

        public LoginController(DataContext context)
        {
            _context = context;
        }

        // GET: Login
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAjax(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return Json(new { success = false, message = "Debe ingresar usuario y contraseña" });
            }

            var user = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

            if (user == null)
            {
                return Json(new { success = false, message = "Usuario o contraseña incorrecta" });
            }

            // Verificar que el rol no sea nulo o vacío
            if (string.IsNullOrEmpty(user.Rol))
            {
                return Json(new { success = false, message = "El usuario no tiene un rol asignado" });
            }

            // Guardar datos en sesión
            HttpContext.Session.SetInt32("UsuarioId", user.UsuarioId);
            HttpContext.Session.SetString("NombreCompleto", $"{user.Nombre} {user.Apellido1}");
            HttpContext.Session.SetString("Rol", user.Rol);

            // Redirigir según rol
            string redirectUrl;
            if (user.Rol == "Administrador")
            {
                redirectUrl = Url.Action("Index", "Home");
            }
            else if (user.Rol == "Vendedor")
            {
                redirectUrl = Url.Action("DashboardVendedor", "Home");
            }
            else
            {
                // Si hay otro rol, podrías manejarlo aquí, por ejemplo, redirigir a una vista por defecto
                redirectUrl = Url.Action("Index", "Home");
            }

            return Json(new { success = true, redirectUrl });
        }

        // Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
