namespace GestionClientesEcoMercadoAustral.Data
using GestionClientesEcoMercadoAustral.Models
using Microsoft.EntityFrameworkCore;
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public Dbs
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Region> Regiones { get; set; }

        public DbSet<Comuna> Comunas { get; set; }

        public DbSet<Ciudad> Ciudades { get; set; } 


    }
}
