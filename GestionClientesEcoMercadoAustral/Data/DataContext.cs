namespace GestionClientesEcoMercadoAustral.Data;
using GestionClientesEcoMercadoAustral.Models;
using Microsoft.EntityFrameworkCore;

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<SegmentoCliente> SegmentosCliente { get; set; }

        public DbSet<Region> Regiones { get; set; }

        public DbSet<Comuna> Comunas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ====== Usuario - Cliente (1:N) ======
        modelBuilder.Entity<Cliente>()
            .HasOne(c => c.Usuario)
            .WithMany(u => u.Clientes)
            .HasForeignKey(c => c.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict); // evita borrar usuario si tiene clientes

        // ====== SegmentoCliente - Cliente (1:N) ======
        modelBuilder.Entity<Cliente>()
            .HasOne(c => c.SegmentoCliente)
            .WithMany(s => s.Clientes)
            .HasForeignKey(c => c.SegmentoClienteId)
            .OnDelete(DeleteBehavior.Restrict); // evita borrar segmento si tiene clientes

        // ====== Region - Comuna (1:N) ======
        modelBuilder.Entity<Comuna>()
            .HasOne(c => c.Region)
            .WithMany(r => r.Comunas)
            .HasForeignKey(c => c.RegionId)
            .OnDelete(DeleteBehavior.Restrict);

        // ====== Cliente - Comuna (N:1) ======
        modelBuilder.Entity<Cliente>()
            .HasOne(c => c.Comuna)
            .WithMany()
            .HasForeignKey(c => c.ComunaId)
            .OnDelete(DeleteBehavior.Restrict);

        // ====== Índices únicos ======
        modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.Username)
            .IsUnique();

        modelBuilder.Entity<Cliente>()
            .HasIndex(c => c.Rut)
            .IsUnique();
   
    
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuario>().HasData(
            new Usuario {UsuarioId = 1 ,Nombre= "Gonzalo",Apellido1= "Basoalto", Apellido2= "Gallegos", Rut= "15.907.638-5", Username= "admin", Password= "admin123", Rol= "Administrador" },
            new Usuario {UsuarioId = 2 ,Nombre = "Juan", Apellido1 = "Perez", Apellido2 = "Lopez", Rut = "12.345.678-9", Username = "user", Password = "user123", Rol = "Vendedor" }
            );
        
        modelBuilder.Entity<Region>().HasData(
        new Region { RegionId = 1, Codigo = "01", Nombre = "Región de Tarapacá" },
        new Region { RegionId = 2, Codigo = "02", Nombre = "Región de Antofagasta" },
        new Region { RegionId = 3, Codigo = "03", Nombre = "Región de Atacama" },
        new Region { RegionId = 4, Codigo = "04", Nombre = "Región de Coquimbo" },
        new Region { RegionId = 5, Codigo = "05", Nombre = "Región de Valparaíso" },
        new Region { RegionId = 6, Codigo = "06", Nombre = "Región del Libertador General Bernardo O’Higgins" },
        new Region { RegionId = 7, Codigo = "07", Nombre = "Región del Maule" },
        new Region { RegionId = 8, Codigo = "08", Nombre = "Región del Biobío" },
        new Region { RegionId = 9, Codigo = "09", Nombre = "Región de La Araucanía" },
        new Region { RegionId = 10, Codigo = "10", Nombre = "Región de Los Lagos" },
        new Region { RegionId = 11, Codigo = "11", Nombre = "Región de Aysén del General Carlos Ibáñez del Campo" },
        new Region { RegionId = 12, Codigo = "12", Nombre = "Región de Magallanes y de la Antártica Chilena" },
        new Region { RegionId = 13, Codigo = "13", Nombre = "Región Metropolitana de Santiago" },
        new Region { RegionId = 14, Codigo = "14", Nombre = "Región de Los Ríos" },
        new Region { RegionId = 15, Codigo = "15", Nombre = "Región de Arica y Parinacota" },
        new Region { RegionId = 16, Codigo = "16", Nombre = "Región de Ñuble" }
        );


        modelBuilder.Entity<Comuna>().HasData(
        // Region 1 – Arica y Parinacota
        new Comuna { ComunaId = 1, Nombre = "Arica", RegionId = 1 },
        new Comuna { ComunaId = 2, Nombre = "Camarones", RegionId = 1 },
        new Comuna { ComunaId = 3, Nombre = "Putre", RegionId = 1 },
        new Comuna { ComunaId = 4, Nombre = "General Lagos", RegionId = 1 },

        // Region 2 – Tarapacá
        new Comuna { ComunaId = 6, Nombre = "Alto Hospicio", RegionId = 2 },
        new Comuna { ComunaId = 7, Nombre = "Pozo Almonte", RegionId = 2 },
        new Comuna { ComunaId = 8, Nombre = "Camiña", RegionId = 2 },
        new Comuna { ComunaId = 9, Nombre = "Colchane", RegionId = 2 },
        new Comuna { ComunaId = 10, Nombre = "Huara", RegionId = 2 },
        new Comuna { ComunaId = 11, Nombre = "Pica", RegionId = 2 },
        new Comuna { ComunaId = 5, Nombre = "Iquique", RegionId = 2 },

        // Region 3 – Antofagasta
        new Comuna { ComunaId = 12, Nombre = "Antofagasta", RegionId = 3 },
        new Comuna { ComunaId = 13, Nombre = "Mejillones", RegionId = 3 },
        new Comuna { ComunaId = 14, Nombre = "Sierra Gorda", RegionId = 3 },
        new Comuna { ComunaId = 15, Nombre = "Taltal", RegionId = 3 },
        new Comuna { ComunaId = 16, Nombre = "Calama", RegionId = 3 },
        new Comuna { ComunaId = 17, Nombre = "San Pedro de Atacama", RegionId = 3 },
        new Comuna { ComunaId = 18, Nombre = "Ollagüe", RegionId = 3 },
        new Comuna { ComunaId = 19, Nombre = "Tocopilla", RegionId = 3 },
        new Comuna { ComunaId = 20, Nombre = "María Elena", RegionId = 3 },

        // Region 4 – Atacama
        new Comuna { ComunaId = 21, Nombre = "Copiapó", RegionId = 4 },
        new Comuna { ComunaId = 22, Nombre = "Caldera", RegionId = 4 },
        new Comuna { ComunaId = 23, Nombre = "Tierra Amarilla", RegionId = 4 },
        new Comuna { ComunaId = 24, Nombre = "Chañaral", RegionId = 4 },
        new Comuna { ComunaId = 25, Nombre = "Diego de Almagro", RegionId = 4 },
        new Comuna { ComunaId = 26, Nombre = "Vallenar", RegionId = 4 },
        new Comuna { ComunaId = 27, Nombre = "Huasco", RegionId = 4 },
        new Comuna { ComunaId = 28, Nombre = "Freirina", RegionId = 4 },
        new Comuna { ComunaId = 29, Nombre = "Alto del Carmen", RegionId = 4 },

        // Region 5 – Coquimbo
        new Comuna { ComunaId = 30, Nombre = "La Serena", RegionId = 5 },
        new Comuna { ComunaId = 31, Nombre = "Coquimbo", RegionId = 5 },
        new Comuna { ComunaId = 32, Nombre = "Andacollo", RegionId = 5 },
        new Comuna { ComunaId = 33, Nombre = "La Higuera", RegionId = 5 },
        new Comuna { ComunaId = 34, Nombre = "Paihuano", RegionId = 5 },
        new Comuna { ComunaId = 35, Nombre = "Vicuña", RegionId = 5 },
        new Comuna { ComunaId = 36, Nombre = "Illapel", RegionId = 5 },
        new Comuna { ComunaId = 37, Nombre = "Los Vilos", RegionId = 5 },
        new Comuna { ComunaId = 38, Nombre = "Salamanca", RegionId = 5 },
        new Comuna { ComunaId = 39, Nombre = "Ovalle", RegionId = 5 },
        new Comuna { ComunaId = 40, Nombre = "Combarbalá", RegionId = 5 },
        new Comuna { ComunaId = 41, Nombre = "Monte Patria", RegionId = 5 },
        new Comuna { ComunaId = 42, Nombre = "Punitaqui", RegionId = 5 },
        new Comuna { ComunaId = 43, Nombre = "Río Hurtado", RegionId = 5 },

            // Región 6 – Valparaíso
        new Comuna { ComunaId = 44, Nombre = "Valparaíso", RegionId = 6 },
        new Comuna { ComunaId = 45, Nombre = "Viña del Mar", RegionId = 6 },
        new Comuna { ComunaId = 46, Nombre = "Concón", RegionId = 6 },
        new Comuna { ComunaId = 47, Nombre = "Quintero", RegionId = 6 },
        new Comuna { ComunaId = 48, Nombre = "Puchuncaví", RegionId = 6 },
        new Comuna { ComunaId = 49, Nombre = "Casablanca", RegionId = 6 },
        new Comuna { ComunaId = 50, Nombre = "Juan Fernández", RegionId = 6 },
        new Comuna { ComunaId = 51, Nombre = "Quilpué", RegionId = 6 },
        new Comuna { ComunaId = 52, Nombre = "Villa Alemana", RegionId = 6 },
        new Comuna { ComunaId = 53, Nombre = "Limache", RegionId = 6 },
        new Comuna { ComunaId = 54, Nombre = "Olmué", RegionId = 6 },
        new Comuna { ComunaId = 55, Nombre = "Quillota", RegionId = 6 },
        new Comuna { ComunaId = 56, Nombre = "La Calera", RegionId = 6 },
        new Comuna { ComunaId = 57, Nombre = "La Cruz", RegionId = 6 },
        new Comuna { ComunaId = 58, Nombre = "Nogales", RegionId = 6 },
        new Comuna { ComunaId = 59, Nombre = "Hijuelas", RegionId = 6 },
        new Comuna { ComunaId = 60, Nombre = "San Antonio", RegionId = 6 },
        new Comuna { ComunaId = 61, Nombre = "Cartagena", RegionId = 6 },
        new Comuna { ComunaId = 62, Nombre = "El Tabo", RegionId = 6 },
        new Comuna { ComunaId = 63, Nombre = "El Quisco", RegionId = 6 },
        new Comuna { ComunaId = 64, Nombre = "Algarrobo", RegionId = 6 },
        new Comuna { ComunaId = 65, Nombre = "Santo Domingo", RegionId = 6 },
        new Comuna { ComunaId = 66, Nombre = "San Felipe", RegionId = 6 },
        new Comuna { ComunaId = 67, Nombre = "Llaillay", RegionId = 6 },
        new Comuna { ComunaId = 68, Nombre = "Catemu", RegionId = 6 },
        new Comuna { ComunaId = 69, Nombre = "Putaendo", RegionId = 6 },
        new Comuna { ComunaId = 70, Nombre = "Santa María", RegionId = 6 },
        new Comuna { ComunaId = 71, Nombre = "Panquehue", RegionId = 6 },

        // Región 7 – Metropolitana de Santiago
        new Comuna { ComunaId = 72, Nombre = "Santiago", RegionId = 7 },
        new Comuna { ComunaId = 73, Nombre = "Cerrillos", RegionId = 7 },
        new Comuna { ComunaId = 74, Nombre = "Cerro Navia", RegionId = 7 },
        new Comuna { ComunaId = 75, Nombre = "Conchalí", RegionId = 7 },
        new Comuna { ComunaId = 76, Nombre = "El Bosque", RegionId = 7 },
        new Comuna { ComunaId = 77, Nombre = "Estación Central", RegionId = 7 },
        new Comuna { ComunaId = 78, Nombre = "Huechuraba", RegionId = 7 },
        new Comuna { ComunaId = 79, Nombre = "Independencia", RegionId = 7 },
        new Comuna { ComunaId = 80, Nombre = "La Cisterna", RegionId = 7 },
        new Comuna { ComunaId = 81, Nombre = "La Florida", RegionId = 7 },
        new Comuna { ComunaId = 82, Nombre = "La Granja", RegionId = 7 },
        new Comuna { ComunaId = 83, Nombre = "La Pintana", RegionId = 7 },
        new Comuna { ComunaId = 84, Nombre = "La Reina", RegionId = 7 },
        new Comuna { ComunaId = 85, Nombre = "Las Condes", RegionId = 7 },
        new Comuna { ComunaId = 86, Nombre = "Lo Barnechea", RegionId = 7 },
        new Comuna { ComunaId = 87, Nombre = "Lo Espejo", RegionId = 7 },
        new Comuna { ComunaId = 88, Nombre = "Lo Prado", RegionId = 7 },
        new Comuna { ComunaId = 89, Nombre = "Macul", RegionId = 7 },
        new Comuna { ComunaId = 90, Nombre = "Maipú", RegionId = 7 },
        new Comuna { ComunaId = 91, Nombre = "Ñuñoa", RegionId = 7 },
        new Comuna { ComunaId = 92, Nombre = "Pedro Aguirre Cerda", RegionId = 7 },
        new Comuna { ComunaId = 93, Nombre = "Peñalolén", RegionId = 7 },
        new Comuna { ComunaId = 94, Nombre = "Providencia", RegionId = 7 },
        new Comuna { ComunaId = 95, Nombre = "Pudahuel", RegionId = 7 },
        new Comuna { ComunaId = 96, Nombre = "Quilicura", RegionId = 7 },
        new Comuna { ComunaId = 97, Nombre = "Quinta Normal", RegionId = 7 },
        new Comuna { ComunaId = 98, Nombre = "Recoleta", RegionId = 7 },
        new Comuna { ComunaId = 99, Nombre = "Renca", RegionId = 7 },
        new Comuna { ComunaId = 100, Nombre = "San Joaquín", RegionId = 7 },
        new Comuna { ComunaId = 101, Nombre = "San Miguel", RegionId = 7 },
        new Comuna { ComunaId = 102, Nombre = "San Ramón", RegionId = 7 },
        new Comuna { ComunaId = 103, Nombre = "Vitacura", RegionId = 7 },
        new Comuna { ComunaId = 104, Nombre = "Puente Alto", RegionId = 7 },
        new Comuna { ComunaId = 105, Nombre = "Pirque", RegionId = 7 },
        new Comuna { ComunaId = 106, Nombre = "San José de Maipo", RegionId = 7 },
        new Comuna { ComunaId = 107, Nombre = "Colina", RegionId = 7 },
        new Comuna { ComunaId = 108, Nombre = "Lampa", RegionId = 7 },
        new Comuna { ComunaId = 109, Nombre = "Tiltil", RegionId = 7 },
        new Comuna { ComunaId = 110, Nombre = "Talagante", RegionId = 7 },
        new Comuna { ComunaId = 111, Nombre = "El Monte", RegionId = 7 },
        new Comuna { ComunaId = 112, Nombre = "Isla de Maipo", RegionId = 7 },
        new Comuna { ComunaId = 113, Nombre = "Padre Hurtado", RegionId = 7 },
        new Comuna { ComunaId = 114, Nombre = "Peñaflor", RegionId = 7 },
        new Comuna { ComunaId = 115, Nombre = "Melipilla", RegionId = 7 },
        new Comuna { ComunaId = 116, Nombre = "Curacaví", RegionId = 7 },
        new Comuna { ComunaId = 117, Nombre = "María Pinto", RegionId = 7 },
        new Comuna { ComunaId = 118, Nombre = "San Pedro", RegionId = 7 },
        new Comuna { ComunaId = 119, Nombre = "Buin", RegionId = 7 },
        new Comuna { ComunaId = 120, Nombre = "Calera de Tango", RegionId = 7 },
        new Comuna { ComunaId = 121, Nombre = "Paine", RegionId = 7 },
        new Comuna { ComunaId = 122, Nombre = "San Bernardo", RegionId = 7 },


        // Región 8 – O'Higgins
        new Comuna { ComunaId = 123, Nombre = "Rancagua", RegionId = 8 },
        new Comuna { ComunaId = 124, Nombre = "Machalí", RegionId = 8 },
        new Comuna { ComunaId = 125, Nombre = "Graneros", RegionId = 8 },
        new Comuna { ComunaId = 126, Nombre = "Mostazal", RegionId = 8 },
        new Comuna { ComunaId = 127, Nombre = "San Francisco de Mostazal", RegionId = 8 },
        new Comuna { ComunaId = 128, Nombre = "Codegua", RegionId = 8 },
        new Comuna { ComunaId = 129, Nombre = "Doñihue", RegionId = 8 },
        new Comuna { ComunaId = 130, Nombre = "Coltauco", RegionId = 8 },
        new Comuna { ComunaId = 131, Nombre = "Peumo", RegionId = 8 },
        new Comuna { ComunaId = 132, Nombre = "Las Cabras", RegionId = 8 },
        new Comuna { ComunaId = 133, Nombre = "San Vicente", RegionId = 8 },
        new Comuna { ComunaId = 134, Nombre = "Pichidegua", RegionId = 8 },
        new Comuna { ComunaId = 135, Nombre = "Rengo", RegionId = 8 },
        new Comuna { ComunaId = 136, Nombre = "Requínoa", RegionId = 8 },
        new Comuna { ComunaId = 137, Nombre = "Malloa", RegionId = 8 },
        new Comuna { ComunaId = 138, Nombre = "San Fernando", RegionId = 8 },
        new Comuna { ComunaId = 139, Nombre = "Santa Cruz", RegionId = 8 },
        new Comuna { ComunaId = 140, Nombre = "Palmilla", RegionId = 8 },
        new Comuna { ComunaId = 141, Nombre = "Peralillo", RegionId = 8 },
        new Comuna { ComunaId = 142, Nombre = "Placilla", RegionId = 8 },
        new Comuna { ComunaId = 143, Nombre = "Pumanque", RegionId = 8 },
        new Comuna { ComunaId = 144, Nombre = "Chimbarongo", RegionId = 8 },
        new Comuna { ComunaId = 145, Nombre = "Lolol", RegionId = 8 },
        new Comuna { ComunaId = 146, Nombre = "Nancagua", RegionId = 8 },
        new Comuna { ComunaId = 147, Nombre = "Paredones", RegionId = 8 },
        new Comuna { ComunaId = 148, Nombre = "Pichilemu", RegionId = 8 },


        // Región 9 – Maule
        new Comuna { ComunaId = 149, Nombre = "Talca", RegionId = 9 },
        new Comuna { ComunaId = 150, Nombre = "Pencahue", RegionId = 9 },
        new Comuna { ComunaId = 151, Nombre = "Maule", RegionId = 9 },
        new Comuna { ComunaId = 152, Nombre = "San Clemente", RegionId = 9 },
        new Comuna { ComunaId = 153, Nombre = "San Rafael", RegionId = 9 },
        new Comuna { ComunaId = 154, Nombre = "Curepto", RegionId = 9 },
        new Comuna { ComunaId = 155, Nombre = "Constitución", RegionId = 9 },
        new Comuna { ComunaId = 156, Nombre = "Empedrado", RegionId = 9 },
        new Comuna { ComunaId = 157, Nombre = "Linares", RegionId = 9 },
        new Comuna { ComunaId = 158, Nombre = "Longaví", RegionId = 9 },
        new Comuna { ComunaId = 159, Nombre = "Colbún", RegionId = 9 },
        new Comuna { ComunaId = 160, Nombre = "Retiro", RegionId = 9 },
        new Comuna { ComunaId = 161, Nombre = "Villa Alegre", RegionId = 9 },
        new Comuna { ComunaId = 162, Nombre = "Yerbas Buenas", RegionId = 9 },
        new Comuna { ComunaId = 163, Nombre = "Cauquenes", RegionId = 9 },
        new Comuna { ComunaId = 164, Nombre = "Chanco", RegionId = 9 },
        new Comuna { ComunaId = 165, Nombre = "Pelluhue", RegionId = 9 },
        new Comuna { ComunaId = 166, Nombre = "Curicó", RegionId = 9 },
        new Comuna { ComunaId = 167, Nombre = "Hualañé", RegionId = 9 },
        new Comuna { ComunaId = 168, Nombre = "Licantén", RegionId = 9 },
        new Comuna { ComunaId = 169, Nombre = "Molina", RegionId = 9 },
        new Comuna { ComunaId = 170, Nombre = "Rauco", RegionId = 9 },
        new Comuna { ComunaId = 171, Nombre = "Romeral", RegionId = 9 },
        new Comuna { ComunaId = 172, Nombre = "Sagrada Familia", RegionId = 9 },
        new Comuna { ComunaId = 173, Nombre = "Teno", RegionId = 9 },
        new Comuna { ComunaId = 174, Nombre = "Vichuquén", RegionId = 9 },

        // Región 10 – Ñuble
        new Comuna { ComunaId = 175, Nombre = "Chillán", RegionId = 10 },
        new Comuna { ComunaId = 176, Nombre = "Chillán Viejo", RegionId = 10 },
        new Comuna { ComunaId = 177, Nombre = "Quillón", RegionId = 10 },
        new Comuna { ComunaId = 178, Nombre = "Bulnes", RegionId = 10 },
        new Comuna { ComunaId = 179, Nombre = "San Ignacio", RegionId = 10 },
        new Comuna { ComunaId = 180, Nombre = "Pemuco", RegionId = 10 },
        new Comuna { ComunaId = 181, Nombre = "El Carmen", RegionId = 10 },
        new Comuna { ComunaId = 182, Nombre = "Yungay", RegionId = 10 },
        new Comuna { ComunaId = 183, Nombre = "Pinto", RegionId = 10 },
        new Comuna { ComunaId = 184, Nombre = "Coihueco", RegionId = 10 },
        new Comuna { ComunaId = 185, Nombre = "San Carlos", RegionId = 10 },
        new Comuna { ComunaId = 186, Nombre = "Ninhue", RegionId = 10 },
        new Comuna { ComunaId = 187, Nombre = "San Nicolás", RegionId = 10 },
        new Comuna { ComunaId = 188, Nombre = "Ñiquén", RegionId = 10 },
        new Comuna { ComunaId = 189, Nombre = "San Fabián", RegionId = 10 },
        new Comuna { ComunaId = 190, Nombre = "Treguaco", RegionId = 10 },
        new Comuna { ComunaId = 191, Nombre = "Cobquecura", RegionId = 10 },
        new Comuna { ComunaId = 192, Nombre = "Portezuelo", RegionId = 10 },
        new Comuna { ComunaId = 193, Nombre = "Ránquil", RegionId = 10 },
        new Comuna { ComunaId = 194, Nombre = "Coelemu", RegionId = 10 },
        new Comuna { ComunaId = 195, Nombre = "Quirihue", RegionId = 10 },
        new Comuna { ComunaId = 196, Nombre = "Chillán Viejo", RegionId = 10 },


        // Región 11 – Biobío
        new Comuna { ComunaId = 197, Nombre = "Concepción", RegionId = 11 },
        new Comuna { ComunaId = 198, Nombre = "Coronel", RegionId = 11 },
        new Comuna { ComunaId = 199, Nombre = "Chiguayante", RegionId = 11 },
        new Comuna { ComunaId = 200, Nombre = "Florida", RegionId = 11 },
        new Comuna { ComunaId = 201, Nombre = "Hualpén", RegionId = 11 },
        new Comuna { ComunaId = 202, Nombre = "Hualqui", RegionId = 11 },
        new Comuna { ComunaId = 203, Nombre = "Lota", RegionId = 11 },
        new Comuna { ComunaId = 204, Nombre = "Penco", RegionId = 11 },
        new Comuna { ComunaId = 205, Nombre = "San Pedro de la Paz", RegionId = 11 },
        new Comuna { ComunaId = 206, Nombre = "Santa Juana", RegionId = 11 },
        new Comuna { ComunaId = 207, Nombre = "Talcahuano", RegionId = 11 },
        new Comuna { ComunaId = 208, Nombre = "Tomé", RegionId = 11 },
        new Comuna { ComunaId = 209, Nombre = "Los Ángeles", RegionId = 11 },
        new Comuna { ComunaId = 210, Nombre = "Antuco", RegionId = 11 },
        new Comuna { ComunaId = 211, Nombre = "Cabrero", RegionId = 11 },
        new Comuna { ComunaId = 212, Nombre = "Laja", RegionId = 11 },
        new Comuna { ComunaId = 213, Nombre = "Mulchén", RegionId = 11 },
        new Comuna { ComunaId = 214, Nombre = "Nacimiento", RegionId = 11 },
        new Comuna { ComunaId = 215, Nombre = "Negrete", RegionId = 11 },
        new Comuna { ComunaId = 216, Nombre = "Quilaco", RegionId = 11 },
        new Comuna { ComunaId = 217, Nombre = "Quilleco", RegionId = 11 },
        new Comuna { ComunaId = 218, Nombre = "San Rosendo", RegionId = 11 },
        new Comuna { ComunaId = 219, Nombre = "Santa Bárbara", RegionId = 11 },
        new Comuna { ComunaId = 220, Nombre = "Tucapel", RegionId = 11 },
        new Comuna { ComunaId = 221, Nombre = "Yumbel", RegionId = 11 },
        new Comuna { ComunaId = 222, Nombre = "Alto Biobío", RegionId = 11 },

        // Región 12 – La Araucanía
        new Comuna { ComunaId = 223, Nombre = "Temuco", RegionId = 12 },
        new Comuna { ComunaId = 224, Nombre = "Padre Las Casas", RegionId = 12 },
        new Comuna { ComunaId = 225, Nombre = "Cunco", RegionId = 12 },
        new Comuna { ComunaId = 226, Nombre = "Melipeuco", RegionId = 12 },
        new Comuna { ComunaId = 227, Nombre = "Vilcún", RegionId = 12 },
        new Comuna { ComunaId = 228, Nombre = "Curarrehue", RegionId = 12 },
        new Comuna { ComunaId = 229, Nombre = "Pucón", RegionId = 12 },
        new Comuna { ComunaId = 230, Nombre = "Villarrica", RegionId = 12 },
        new Comuna { ComunaId = 231, Nombre = "Lautaro", RegionId = 12 },
        new Comuna { ComunaId = 232, Nombre = "Perquenco", RegionId = 12 },
        new Comuna { ComunaId = 233, Nombre = "Galvarino", RegionId = 12 },
        new Comuna { ComunaId = 234, Nombre = "Cholchol", RegionId = 12 },
        new Comuna { ComunaId = 235, Nombre = "Nueva Imperial", RegionId = 12 },
        new Comuna { ComunaId = 236, Nombre = "Carahue", RegionId = 12 },
        new Comuna { ComunaId = 237, Nombre = "Saavedra", RegionId = 12 },
        new Comuna { ComunaId = 238, Nombre = "Teodoro Schmidt", RegionId = 12 },
        new Comuna { ComunaId = 239, Nombre = "Toltén", RegionId = 12 },
        new Comuna { ComunaId = 240, Nombre = "Gorbea", RegionId = 12 },
        new Comuna { ComunaId = 241, Nombre = "Loncoche", RegionId = 12 },
        new Comuna { ComunaId = 242, Nombre = "Pitrufquén", RegionId = 12 },
        new Comuna { ComunaId = 243, Nombre = "Freire", RegionId = 12 },
        new Comuna { ComunaId = 244, Nombre = "Renaico", RegionId = 12 },
        new Comuna { ComunaId = 245, Nombre = "Angol", RegionId = 12 },
        new Comuna { ComunaId = 246, Nombre = "Collipulli", RegionId = 12 },
        new Comuna { ComunaId = 247, Nombre = "Curacautín", RegionId = 12 },
        new Comuna { ComunaId = 248, Nombre = "Lonquimay", RegionId = 12 },
        new Comuna { ComunaId = 249, Nombre = "Ercilla", RegionId = 12 },
        new Comuna { ComunaId = 250, Nombre = "Purén", RegionId = 12 },
        new Comuna { ComunaId = 251, Nombre = "Los Sauces", RegionId = 12 },
        new Comuna { ComunaId = 252, Nombre = "Lumaco", RegionId = 12 },
        new Comuna { ComunaId = 253, Nombre = "Traiguén", RegionId = 12 },
        new Comuna { ComunaId = 254, Nombre = "Victoria", RegionId = 12 },

        // Región 13 – Los Ríos
        new Comuna { ComunaId = 255, Nombre = "Valdivia", RegionId = 13 },
        new Comuna { ComunaId = 256, Nombre = "Corral", RegionId = 13 },
        new Comuna { ComunaId = 257, Nombre = "Lanco", RegionId = 13 },
        new Comuna { ComunaId = 258, Nombre = "Los Lagos", RegionId = 13 },
        new Comuna { ComunaId = 259, Nombre = "Máfil", RegionId = 13 },
        new Comuna { ComunaId = 260, Nombre = "Mariquina", RegionId = 13 },
        new Comuna { ComunaId = 261, Nombre = "Paillaco", RegionId = 13 },
        new Comuna { ComunaId = 262, Nombre = "Panguipulli", RegionId = 13 },
        new Comuna { ComunaId = 263, Nombre = "La Unión", RegionId = 13 },
        new Comuna { ComunaId = 264, Nombre = "Río Bueno", RegionId = 13 },
        new Comuna { ComunaId = 265, Nombre = "Futrono", RegionId = 13 },
        new Comuna { ComunaId = 266, Nombre = "Lago Ranco", RegionId = 13 },

        // Región 14 – Los Lagos
        new Comuna { ComunaId = 269, Nombre = "Puerto Montt", RegionId = 14 },
        new Comuna { ComunaId = 270, Nombre = "Puerto Varas", RegionId = 14 },
        new Comuna { ComunaId = 271, Nombre = "Llanquihue", RegionId = 14 },
        new Comuna { ComunaId = 272, Nombre = "Frutillar", RegionId = 14 },
        new Comuna { ComunaId = 273, Nombre = "Los Muermos", RegionId = 14 },
        new Comuna { ComunaId = 274, Nombre = "Maullín", RegionId = 14 },
        new Comuna { ComunaId = 275, Nombre = "Calbuco", RegionId = 14 },
        new Comuna { ComunaId = 276, Nombre = "Cochamó", RegionId = 14 },
        new Comuna { ComunaId = 277, Nombre = "Osorno", RegionId = 14 },
        new Comuna { ComunaId = 278, Nombre = "Río Negro", RegionId = 14 },
        new Comuna { ComunaId = 279, Nombre = "Purranque", RegionId = 14 },
        new Comuna { ComunaId = 280, Nombre = "San Pablo", RegionId = 14 },
        new Comuna { ComunaId = 281, Nombre = "Puqueldón", RegionId = 14 },
        new Comuna { ComunaId = 282, Nombre = "Castro", RegionId = 14 },
        new Comuna { ComunaId = 283, Nombre = "Chonchi", RegionId = 14 },
        new Comuna { ComunaId = 284, Nombre = "Curaco de Vélez", RegionId = 14 },
        new Comuna { ComunaId = 285, Nombre = "Dalcahue", RegionId = 14 },
        new Comuna { ComunaId = 286, Nombre = "Puqueldón", RegionId = 14 },
        new Comuna { ComunaId = 287, Nombre = "Queilén", RegionId = 14 },
        new Comuna { ComunaId = 288, Nombre = "Quellón", RegionId = 14 },
        new Comuna { ComunaId = 289, Nombre = "Quemchi", RegionId = 14 },
        new Comuna { ComunaId = 290, Nombre = "Quinchao", RegionId = 14 },

        // Región 15 – Aysén
        new Comuna { ComunaId = 299, Nombre = "Coyhaique", RegionId = 15 },
        new Comuna { ComunaId = 300, Nombre = "Lago Verde", RegionId = 15 },
        new Comuna { ComunaId = 301, Nombre = "Aysén", RegionId = 15 },
        new Comuna { ComunaId = 302, Nombre = "Cisnes", RegionId = 15 },
        new Comuna { ComunaId = 303, Nombre = "Guaitecas", RegionId = 15 },
        new Comuna { ComunaId = 304, Nombre = "Cochrane", RegionId = 15 },
        new Comuna { ComunaId = 305, Nombre = "O’Higgins", RegionId = 15 },
        new Comuna { ComunaId = 306, Nombre = "Tortel", RegionId = 15 },
        new Comuna { ComunaId = 307, Nombre = "Chile Chico", RegionId = 15 },
        new Comuna { ComunaId = 308, Nombre = "Río Ibáñez", RegionId = 15 },

        // Región 16 – Magallanes y de la Antártica Chilena
        new Comuna { ComunaId = 313, Nombre = "Punta Arenas", RegionId = 16 },
        new Comuna { ComunaId = 314, Nombre = "Río Verde", RegionId = 16 },
        new Comuna { ComunaId = 315, Nombre = "Laguna Blanca", RegionId = 16 },
        new Comuna { ComunaId = 316, Nombre = "San Gregorio", RegionId = 16 },
        new Comuna { ComunaId = 317, Nombre = "Puerto Natales", RegionId = 16 },
        new Comuna { ComunaId = 318, Nombre = "Torres del Paine", RegionId = 16 },
        new Comuna { ComunaId = 319, Nombre = "Porvenir", RegionId = 16 },
        new Comuna { ComunaId = 320, Nombre = "Primavera", RegionId = 16 },
        new Comuna { ComunaId = 321, Nombre = "Timaukel", RegionId = 16 },
        new Comuna { ComunaId = 322, Nombre = "Cabo de Hornos", RegionId = 16 },
        new Comuna { ComunaId = 323, Nombre = "Antártica", RegionId = 16 }
        );

    }
}

