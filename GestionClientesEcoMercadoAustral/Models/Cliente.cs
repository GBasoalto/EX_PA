using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GestionClientesEcoMercadoAustral.Models
{
    [Index(nameof(Rut), IsUnique = true)]
    public class Cliente
    {
        public int ClienteId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Rut { get; set; } 

        public string Nombre { get; set; }

        public string TipoCliente { get; set; }  // "Empresa" o "Persona Natural"

        public string Apellido1 { get; set; }

        public string Apellido2 { get; set; } 

        public string Direccion { get; set; }

        public int ComunaId { get; set; }

        public Comuna Comuna { get; set; }

        // RELACIÓN CON USUARIO
        public int UsuarioId { get; set; }        
        public Usuario Usuario { get; set; }

        //RELACION CON SEGMENTO
        public int SegmentoClienteId { get; set; }
        public SegmentoCliente SegmentoCliente { get; set; }

    }
}
