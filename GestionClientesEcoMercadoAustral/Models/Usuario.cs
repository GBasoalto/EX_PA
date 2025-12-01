using System.ComponentModel.DataAnnotations;

namespace GestionClientesEcoMercadoAustral.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }

        public string Nombre { get; set; }

        public string Apellido1 { get; set; }

        public string Apellido2 { get; set; }

        [Required]
        [MaxLength(20)]
        public string Rut { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Rol { get; set; } // Administrador, Vendedor
    }
}
