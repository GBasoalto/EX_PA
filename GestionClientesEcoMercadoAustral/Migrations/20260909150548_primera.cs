using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GestionClientesEcoMercadoAustral.Migrations
{
    /// <inheritdoc />
    public partial class primera : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Regiones",
                columns: table => new
                {
                    RegionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regiones", x => x.RegionId);
                });

            migrationBuilder.CreateTable(
                name: "SegmentosCliente",
                columns: table => new
                {
                    SegmentoClienteId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreSegmento = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SegmentosCliente", x => x.SegmentoClienteId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Apellido1 = table.Column<string>(type: "text", nullable: false),
                    Apellido2 = table.Column<string>(type: "text", nullable: false),
                    Rut = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Rol = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Comunas",
                columns: table => new
                {
                    ComunaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    RegionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comunas", x => x.ComunaId);
                    table.ForeignKey(
                        name: "FK_Comunas_Regiones_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regiones",
                        principalColumn: "RegionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ClienteId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Rut = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    TipoCliente = table.Column<string>(type: "text", nullable: false),
                    Apellido1 = table.Column<string>(type: "text", nullable: true),
                    Apellido2 = table.Column<string>(type: "text", nullable: true),
                    Direccion = table.Column<string>(type: "text", nullable: false),
                    ComunaId = table.Column<int>(type: "integer", nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    SegmentoClienteId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ClienteId);
                    table.ForeignKey(
                        name: "FK_Clientes_Comunas_ComunaId",
                        column: x => x.ComunaId,
                        principalTable: "Comunas",
                        principalColumn: "ComunaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clientes_SegmentosCliente_SegmentoClienteId",
                        column: x => x.SegmentoClienteId,
                        principalTable: "SegmentosCliente",
                        principalColumn: "SegmentoClienteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clientes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Regiones",
                columns: new[] { "RegionId", "Codigo", "Nombre" },
                values: new object[,]
                {
                    { 1, "15", "Región de Arica y Parinacota" },
                    { 2, "01", "Región de Tarapacá" },
                    { 3, "02", "Región de Antofagasta" },
                    { 4, "03", "Región de Atacama" },
                    { 5, "04", "Región de Coquimbo" },
                    { 6, "05", "Región de Valparaíso" },
                    { 7, "13", "Región Metropolitana de Santiago" },
                    { 8, "06", "Región del Libertador General Bernardo O’Higgins" },
                    { 9, "07", "Región del Maule" },
                    { 10, "16", "Región de Ñuble" },
                    { 11, "08", "Región del Biobío" },
                    { 12, "09", "Región de La Araucanía" },
                    { 13, "14", "Región de Los Ríos" },
                    { 14, "10", "Región de Los Lagos" },
                    { 15, "11", "Región de Aysén del General Carlos Ibáñez del Campo" },
                    { 16, "12", "Región de Magallanes y de la Antártica Chilena" }
                });

            migrationBuilder.InsertData(
                table: "SegmentosCliente",
                columns: new[] { "SegmentoClienteId", "Descripcion", "Estado", "NombreSegmento" },
                values: new object[,]
                {
                    { 1, "Clientes mayoristas", "Vigente", "Mayorista" },
                    { 2, "Clientes minoristas", "Vigente", "Minorista" },
                    { 3, "Clientes restaurantes", "Vigente", "Restaurante" },
                    { 4, "Clientes institucionales", "Vigente", "Instituciones" },
                    { 5, "Otros tipos de clientes", "Vigente", "Otro" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "UsuarioId", "Apellido1", "Apellido2", "Nombre", "Password", "Rol", "Rut", "Username" },
                values: new object[,]
                {
                    { 1, "Basoalto", "Gallegos", "Gonzalo", "admin123", "Administrador", "15.907.638-5", "admin" },
                    { 2, "Perez", "Lopez", "Juan", "user123", "Vendedor", "12.345.678-9", "user" },
                    { 3, "Morales", "Gajardo", "Johanna", "user123", "Vendedor", "9.876.543-1", "JMorales" }
                });

            migrationBuilder.InsertData(
                table: "Comunas",
                columns: new[] { "ComunaId", "Nombre", "RegionId" },
                values: new object[,]
                {
                    { 1, "Arica", 1 },
                    { 2, "Camarones", 1 },
                    { 3, "Putre", 1 },
                    { 4, "General Lagos", 1 },
                    { 5, "Iquique", 2 },
                    { 6, "Alto Hospicio", 2 },
                    { 7, "Pozo Almonte", 2 },
                    { 8, "Camiña", 2 },
                    { 9, "Colchane", 2 },
                    { 10, "Huara", 2 },
                    { 11, "Pica", 2 },
                    { 12, "Antofagasta", 3 },
                    { 13, "Mejillones", 3 },
                    { 14, "Sierra Gorda", 3 },
                    { 15, "Taltal", 3 },
                    { 16, "Calama", 3 },
                    { 17, "San Pedro de Atacama", 3 },
                    { 18, "Ollagüe", 3 },
                    { 19, "Tocopilla", 3 },
                    { 20, "María Elena", 3 },
                    { 21, "Copiapó", 4 },
                    { 22, "Caldera", 4 },
                    { 23, "Tierra Amarilla", 4 },
                    { 24, "Chañaral", 4 },
                    { 25, "Diego de Almagro", 4 },
                    { 26, "Vallenar", 4 },
                    { 27, "Huasco", 4 },
                    { 28, "Freirina", 4 },
                    { 29, "Alto del Carmen", 4 },
                    { 30, "La Serena", 5 },
                    { 31, "Coquimbo", 5 },
                    { 32, "Andacollo", 5 },
                    { 33, "La Higuera", 5 },
                    { 34, "Paihuano", 5 },
                    { 35, "Vicuña", 5 },
                    { 36, "Illapel", 5 },
                    { 37, "Los Vilos", 5 },
                    { 38, "Salamanca", 5 },
                    { 39, "Ovalle", 5 },
                    { 40, "Combarbalá", 5 },
                    { 41, "Monte Patria", 5 },
                    { 42, "Punitaqui", 5 },
                    { 43, "Río Hurtado", 5 },
                    { 44, "Valparaíso", 6 },
                    { 45, "Viña del Mar", 6 },
                    { 46, "Concón", 6 },
                    { 47, "Quintero", 6 },
                    { 48, "Puchuncaví", 6 },
                    { 49, "Casablanca", 6 },
                    { 50, "Juan Fernández", 6 },
                    { 51, "Quilpué", 6 },
                    { 52, "Villa Alemana", 6 },
                    { 53, "Limache", 6 },
                    { 54, "Olmué", 6 },
                    { 55, "Quillota", 6 },
                    { 56, "La Calera", 6 },
                    { 57, "La Cruz", 6 },
                    { 58, "Nogales", 6 },
                    { 59, "Hijuelas", 6 },
                    { 60, "San Antonio", 6 },
                    { 61, "Cartagena", 6 },
                    { 62, "El Tabo", 6 },
                    { 63, "El Quisco", 6 },
                    { 64, "Algarrobo", 6 },
                    { 65, "Santo Domingo", 6 },
                    { 66, "San Felipe", 6 },
                    { 67, "Llaillay", 6 },
                    { 68, "Catemu", 6 },
                    { 69, "Putaendo", 6 },
                    { 70, "Santa María", 6 },
                    { 71, "Panquehue", 6 },
                    { 72, "Santiago", 7 },
                    { 73, "Cerrillos", 7 },
                    { 74, "Cerro Navia", 7 },
                    { 75, "Conchalí", 7 },
                    { 76, "El Bosque", 7 },
                    { 77, "Estación Central", 7 },
                    { 78, "Huechuraba", 7 },
                    { 79, "Independencia", 7 },
                    { 80, "La Cisterna", 7 },
                    { 81, "La Florida", 7 },
                    { 82, "La Granja", 7 },
                    { 83, "La Pintana", 7 },
                    { 84, "La Reina", 7 },
                    { 85, "Las Condes", 7 },
                    { 86, "Lo Barnechea", 7 },
                    { 87, "Lo Espejo", 7 },
                    { 88, "Lo Prado", 7 },
                    { 89, "Macul", 7 },
                    { 90, "Maipú", 7 },
                    { 91, "Ñuñoa", 7 },
                    { 92, "Pedro Aguirre Cerda", 7 },
                    { 93, "Peñalolén", 7 },
                    { 94, "Providencia", 7 },
                    { 95, "Pudahuel", 7 },
                    { 96, "Quilicura", 7 },
                    { 97, "Quinta Normal", 7 },
                    { 98, "Recoleta", 7 },
                    { 99, "Renca", 7 },
                    { 100, "San Joaquín", 7 },
                    { 101, "San Miguel", 7 },
                    { 102, "San Ramón", 7 },
                    { 103, "Vitacura", 7 },
                    { 104, "Puente Alto", 7 },
                    { 105, "Pirque", 7 },
                    { 106, "San José de Maipo", 7 },
                    { 107, "Colina", 7 },
                    { 108, "Lampa", 7 },
                    { 109, "Tiltil", 7 },
                    { 110, "Talagante", 7 },
                    { 111, "El Monte", 7 },
                    { 112, "Isla de Maipo", 7 },
                    { 113, "Padre Hurtado", 7 },
                    { 114, "Peñaflor", 7 },
                    { 115, "Melipilla", 7 },
                    { 116, "Curacaví", 7 },
                    { 117, "María Pinto", 7 },
                    { 118, "San Pedro", 7 },
                    { 119, "Buin", 7 },
                    { 120, "Calera de Tango", 7 },
                    { 121, "Paine", 7 },
                    { 122, "San Bernardo", 7 },
                    { 123, "Rancagua", 8 },
                    { 124, "Machalí", 8 },
                    { 125, "Graneros", 8 },
                    { 126, "Mostazal", 8 },
                    { 127, "San Francisco de Mostazal", 8 },
                    { 128, "Codegua", 8 },
                    { 129, "Doñihue", 8 },
                    { 130, "Coltauco", 8 },
                    { 131, "Peumo", 8 },
                    { 132, "Las Cabras", 8 },
                    { 133, "San Vicente", 8 },
                    { 134, "Pichidegua", 8 },
                    { 135, "Rengo", 8 },
                    { 136, "Requínoa", 8 },
                    { 137, "Malloa", 8 },
                    { 138, "San Fernando", 8 },
                    { 139, "Santa Cruz", 8 },
                    { 140, "Palmilla", 8 },
                    { 141, "Peralillo", 8 },
                    { 142, "Placilla", 8 },
                    { 143, "Pumanque", 8 },
                    { 144, "Chimbarongo", 8 },
                    { 145, "Lolol", 8 },
                    { 146, "Nancagua", 8 },
                    { 147, "Paredones", 8 },
                    { 148, "Pichilemu", 8 },
                    { 149, "Talca", 9 },
                    { 150, "Pencahue", 9 },
                    { 151, "Maule", 9 },
                    { 152, "San Clemente", 9 },
                    { 153, "San Rafael", 9 },
                    { 154, "Curepto", 9 },
                    { 155, "Constitución", 9 },
                    { 156, "Empedrado", 9 },
                    { 157, "Linares", 9 },
                    { 158, "Longaví", 9 },
                    { 159, "Colbún", 9 },
                    { 160, "Retiro", 9 },
                    { 161, "Villa Alegre", 9 },
                    { 162, "Yerbas Buenas", 9 },
                    { 163, "Cauquenes", 9 },
                    { 164, "Chanco", 9 },
                    { 165, "Pelluhue", 9 },
                    { 166, "Curicó", 9 },
                    { 167, "Hualañé", 9 },
                    { 168, "Licantén", 9 },
                    { 169, "Molina", 9 },
                    { 170, "Rauco", 9 },
                    { 171, "Romeral", 9 },
                    { 172, "Sagrada Familia", 9 },
                    { 173, "Teno", 9 },
                    { 174, "Vichuquén", 9 },
                    { 175, "Chillán", 10 },
                    { 176, "Chillán Viejo", 10 },
                    { 177, "Quillón", 10 },
                    { 178, "Bulnes", 10 },
                    { 179, "San Ignacio", 10 },
                    { 180, "Pemuco", 10 },
                    { 181, "El Carmen", 10 },
                    { 182, "Yungay", 10 },
                    { 183, "Pinto", 10 },
                    { 184, "Coihueco", 10 },
                    { 185, "San Carlos", 10 },
                    { 186, "Ninhue", 10 },
                    { 187, "San Nicolás", 10 },
                    { 188, "Ñiquén", 10 },
                    { 189, "San Fabián", 10 },
                    { 190, "Treguaco", 10 },
                    { 191, "Cobquecura", 10 },
                    { 192, "Portezuelo", 10 },
                    { 193, "Ránquil", 10 },
                    { 194, "Coelemu", 10 },
                    { 195, "Quirihue", 10 },
                    { 196, "Chillán Viejo", 10 },
                    { 197, "Concepción", 11 },
                    { 198, "Coronel", 11 },
                    { 199, "Chiguayante", 11 },
                    { 200, "Florida", 11 },
                    { 201, "Hualpén", 11 },
                    { 202, "Hualqui", 11 },
                    { 203, "Lota", 11 },
                    { 204, "Penco", 11 },
                    { 205, "San Pedro de la Paz", 11 },
                    { 206, "Santa Juana", 11 },
                    { 207, "Talcahuano", 11 },
                    { 208, "Tomé", 11 },
                    { 209, "Los Ángeles", 11 },
                    { 210, "Antuco", 11 },
                    { 211, "Cabrero", 11 },
                    { 212, "Laja", 11 },
                    { 213, "Mulchén", 11 },
                    { 214, "Nacimiento", 11 },
                    { 215, "Negrete", 11 },
                    { 216, "Quilaco", 11 },
                    { 217, "Quilleco", 11 },
                    { 218, "San Rosendo", 11 },
                    { 219, "Santa Bárbara", 11 },
                    { 220, "Tucapel", 11 },
                    { 221, "Yumbel", 11 },
                    { 222, "Alto Biobío", 11 },
                    { 223, "Temuco", 12 },
                    { 224, "Padre Las Casas", 12 },
                    { 225, "Cunco", 12 },
                    { 226, "Melipeuco", 12 },
                    { 227, "Vilcún", 12 },
                    { 228, "Curarrehue", 12 },
                    { 229, "Pucón", 12 },
                    { 230, "Villarrica", 12 },
                    { 231, "Lautaro", 12 },
                    { 232, "Perquenco", 12 },
                    { 233, "Galvarino", 12 },
                    { 234, "Cholchol", 12 },
                    { 235, "Nueva Imperial", 12 },
                    { 236, "Carahue", 12 },
                    { 237, "Saavedra", 12 },
                    { 238, "Teodoro Schmidt", 12 },
                    { 239, "Toltén", 12 },
                    { 240, "Gorbea", 12 },
                    { 241, "Loncoche", 12 },
                    { 242, "Pitrufquén", 12 },
                    { 243, "Freire", 12 },
                    { 244, "Renaico", 12 },
                    { 245, "Angol", 12 },
                    { 246, "Collipulli", 12 },
                    { 247, "Curacautín", 12 },
                    { 248, "Lonquimay", 12 },
                    { 249, "Ercilla", 12 },
                    { 250, "Purén", 12 },
                    { 251, "Los Sauces", 12 },
                    { 252, "Lumaco", 12 },
                    { 253, "Traiguén", 12 },
                    { 254, "Victoria", 12 },
                    { 255, "Valdivia", 13 },
                    { 256, "Corral", 13 },
                    { 257, "Lanco", 13 },
                    { 258, "Los Lagos", 13 },
                    { 259, "Máfil", 13 },
                    { 260, "Mariquina", 13 },
                    { 261, "Paillaco", 13 },
                    { 262, "Panguipulli", 13 },
                    { 263, "La Unión", 13 },
                    { 264, "Río Bueno", 13 },
                    { 265, "Futrono", 13 },
                    { 266, "Lago Ranco", 13 },
                    { 269, "Puerto Montt", 14 },
                    { 270, "Puerto Varas", 14 },
                    { 271, "Llanquihue", 14 },
                    { 272, "Frutillar", 14 },
                    { 273, "Los Muermos", 14 },
                    { 274, "Maullín", 14 },
                    { 275, "Calbuco", 14 },
                    { 276, "Cochamó", 14 },
                    { 277, "Osorno", 14 },
                    { 278, "Río Negro", 14 },
                    { 279, "Purranque", 14 },
                    { 280, "San Pablo", 14 },
                    { 281, "Puqueldón", 14 },
                    { 282, "Castro", 14 },
                    { 283, "Chonchi", 14 },
                    { 284, "Curaco de Vélez", 14 },
                    { 285, "Dalcahue", 14 },
                    { 286, "Puqueldón", 14 },
                    { 287, "Queilén", 14 },
                    { 288, "Quellón", 14 },
                    { 289, "Quemchi", 14 },
                    { 290, "Quinchao", 14 },
                    { 299, "Coyhaique", 15 },
                    { 300, "Lago Verde", 15 },
                    { 301, "Aysén", 15 },
                    { 302, "Cisnes", 15 },
                    { 303, "Guaitecas", 15 },
                    { 304, "Cochrane", 15 },
                    { 305, "O’Higgins", 15 },
                    { 306, "Tortel", 15 },
                    { 307, "Chile Chico", 15 },
                    { 308, "Río Ibáñez", 15 },
                    { 313, "Punta Arenas", 16 },
                    { 314, "Río Verde", 16 },
                    { 315, "Laguna Blanca", 16 },
                    { 316, "San Gregorio", 16 },
                    { 317, "Puerto Natales", 16 },
                    { 318, "Torres del Paine", 16 },
                    { 319, "Porvenir", 16 },
                    { 320, "Primavera", 16 },
                    { 321, "Timaukel", 16 },
                    { 322, "Cabo de Hornos", 16 },
                    { 323, "Antártica", 16 }
                });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "ClienteId", "Apellido1", "Apellido2", "ComunaId", "Direccion", "Nombre", "Rut", "SegmentoClienteId", "TipoCliente", "UsuarioId" },
                values: new object[,]
                {
                    { 1, "Pérez", "González", 1, "Calle Falsa 123", "Juan", "12345678-9", 1, "Persona Natural", 1 },
                    { 2, null, null, 2, "Avenida Siempre Viva 456", "Empresa XYZ", "98765432-1", 2, "Empresa", 2 },
                    { 3, null, null, 2, "Cumpeo", "El pollo farsante", "1245789-4", 3, "Empresa", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_ComunaId",
                table: "Clientes",
                column: "ComunaId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_Rut",
                table: "Clientes",
                column: "Rut",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_SegmentoClienteId",
                table: "Clientes",
                column: "SegmentoClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_UsuarioId",
                table: "Clientes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Comunas_RegionId",
                table: "Comunas",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Username",
                table: "Usuarios",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Comunas");

            migrationBuilder.DropTable(
                name: "SegmentosCliente");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Regiones");
        }
    }
}
