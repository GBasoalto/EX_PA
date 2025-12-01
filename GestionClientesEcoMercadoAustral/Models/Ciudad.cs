namespace GestionClientesEcoMercadoAustral.Models
{
    public class Ciudad
    {
        public int CiudadId { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }

        public int ComunaId { get; set; }
        public Comuna Comuna { get; set; }
      
    }
}
