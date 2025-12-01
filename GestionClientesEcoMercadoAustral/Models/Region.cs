namespace GestionClientesEcoMercadoAustral.Models
{
    public class Region
    {
        public int RegionId { get; set; }

        public string Codigo { get; set; }
        public string Nombre { get; set; }
        
        // Navigation property for related Comuna entities
        public ICollection<Comuna> Comunas { get; set; } = new List<Comuna>();
        
     }
}
