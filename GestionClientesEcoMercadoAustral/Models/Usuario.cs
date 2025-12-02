using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GestionClientesEcoMercadoAustral.Models
{
    [Index(nameof(Username), IsUnique = true)]
    public class Usuario
    {
    
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El primer apellido es obligatorio.")]
        [Display(Name = "Primer Apellido")]
        public string Apellido1 { get; set; }

        [Display(Name = "Segundo Apellido")]
        public string Apellido2 { get; set; }

        [Required(ErrorMessage = "El RUT es obligatorio.")]
        [MaxLength(20, ErrorMessage = "El RUT no puede superar 20 caracteres.")]
        [Display(Name = "RUT")]
        public string Rut { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [Display(Name = "Usuario")]
        public string Username { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Rol del Usuario")]
        public string Rol { get; set; } // Administrador, Vendedor


        // RELACIÓN INVERSA
        public ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    }
}
