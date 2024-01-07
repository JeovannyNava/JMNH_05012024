using Microsoft.EntityFrameworkCore.Migrations;

namespace JMNH_05012024.Migrations
{
    public partial class camposEliminado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "Materias",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "Alumnos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "AlumnoMateria",
                columns: table => new
                {
                    AlumnosIdAlumno = table.Column<int>(type: "int", nullable: false),
                    MateriasIdMateria = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlumnoMateria", x => new { x.AlumnosIdAlumno, x.MateriasIdMateria });
                    table.ForeignKey(
                        name: "FK_AlumnoMateria_Alumnos_AlumnosIdAlumno",
                        column: x => x.AlumnosIdAlumno,
                        principalTable: "Alumnos",
                        principalColumn: "IdAlumno",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlumnoMateria_Materias_MateriasIdMateria",
                        column: x => x.MateriasIdMateria,
                        principalTable: "Materias",
                        principalColumn: "IdMateria",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e52c169c-cd4a-473a-becc-0dba41c78423",
                column: "ConcurrencyStamp",
                value: "9fcb9296-1114-48f6-b5cb-260d14f87c2e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e52c169c-cd4a-473a-becc-0dba41c78424",
                column: "ConcurrencyStamp",
                value: "09274397-dd35-4359-b797-8ebb9262d22d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "72c7e21c-ac55-46b4-a2ee-df24af6b45e4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a7cc0b3c-5f1d-418b-8227-43112b59f106", "AQAAAAEAACcQAAAAEHwyUC0qWv43UJcESApHUQlcbE9ACaglLuJdzSoWCsM/cajB8ibfDB5JVKm0kPbMhw==", "58552188-3cf1-43dc-9ae2-d085e1c5561a" });

            migrationBuilder.CreateIndex(
                name: "IX_AlumnoMateria_MateriasIdMateria",
                table: "AlumnoMateria",
                column: "MateriasIdMateria");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlumnoMateria");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "Materias");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "Alumnos");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e52c169c-cd4a-473a-becc-0dba41c78423",
                column: "ConcurrencyStamp",
                value: "78f50cc4-b673-4766-9869-4f879fa4046c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e52c169c-cd4a-473a-becc-0dba41c78424",
                column: "ConcurrencyStamp",
                value: "bbd7f7e2-37d4-4a6f-83f0-9da896c7edd4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "72c7e21c-ac55-46b4-a2ee-df24af6b45e4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8faf2981-7ae9-4681-ad55-0fe6b8917f38", "AQAAAAEAACcQAAAAEEibjK+JklrY5SFF4jWlUAXLQ5N5LWd2+sbuxlFcHR6Wv2M0OKYuse+DWwLQHkPcAQ==", "183455c6-9f06-4d3a-8a7e-502bb5c5261c" });
        }
    }
}
