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

        // POST: Login (AJAX)
        [HttpPost]
        public async Task<IActionResult> LoginAjax(string username, string password)
        {
            // Validación manual
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

            // Guardar datos en sesión
            HttpContext.Session.SetInt32("UsuarioId", user.UsuarioId);
            HttpContext.Session.SetString("NombreCompleto", $"{user.Nombre} {user.Apellido1}");
            HttpContext.Session.SetString("Rol", user.Rol);

            return Json(new { success = true });
        }

        // Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
