namespace GestionClientesEcoMercadoAustral.Models
{
    public class SegmentoCliente
    {
        public int SegmentoClienteId { get; set; }
        
        public string NombreSegmento { get; set; } // Minorista, Mayorista, Restaurantes, Instituciones, Otros

        public string Descripcion { get; set; }

        public string Estado { get; set; } // Vigente, No Vigente
    }
}
