using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GestionClientesEcoMercadoAustral.Models
{
    [Index(nameof(Rut), IsUnique = true)]
    public class Cliente
    {
        public int IdCliente { get; set; }

        [Required]
        [MaxLength(20)]
        public string Rut { get; set; } 

        public string Nombre { get; set; }

        public string Apellido1 { get; set; }

        public string Apellido2 { get; set; } 

        public string Direccion { get; set; }

        public int CiudadId { get; set; }

        public Ciudad Ciudad { get; set; }
    }
}
