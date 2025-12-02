using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace GestionClientesEcoMercadoAustral.Models
{
    [Index(nameof(Rut), IsUnique = true)]
    public class Cliente : IValidatableObject
    {
        public int ClienteId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Rut { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string TipoCliente { get; set; }  // "Empresa" o "Persona Natural"

        public string? Apellido1 { get; set; } // ahora opcional

        public string? Apellido2 { get; set; } // ahora opcional

        [Required]
        public string Direccion { get; set; }

        [Required]
        public int ComunaId { get; set; }
        public Comuna? Comuna { get; set; }

        [Required]
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        [Required]
        public int SegmentoClienteId { get; set; }
        public SegmentoCliente? SegmentoCliente { get; set; }

        // Validación condicional: apellidos obligatorios solo para Persona Natural
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrWhiteSpace(TipoCliente) && TipoCliente.Trim() == "Persona Natural")
            {
                if (string.IsNullOrWhiteSpace(Apellido1))
                {
                    yield return new ValidationResult("El primer apellido es obligatorio para Persona Natural.", new[] { nameof(Apellido1) });
                }

                if (string.IsNullOrWhiteSpace(Apellido2))
                {
                    yield return new ValidationResult("El segundo apellido es obligatorio para Persona Natural.", new[] { nameof(Apellido2) });
                }
            }
        }
    }
}
