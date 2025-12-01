namespace GestionClientesEcoMercadoAustral.Models
{
    public class Comuna
    {
        public int ComunaId { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public int RegionId { get; set; }

        public Region Region { get; set; }

    }
}
