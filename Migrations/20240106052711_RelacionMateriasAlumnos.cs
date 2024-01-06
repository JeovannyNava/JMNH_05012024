using Microsoft.EntityFrameworkCore.Migrations;

namespace JMNH_05012024.Migrations
{
    public partial class RelacionMateriasAlumnos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MateriasAlumnos",
                columns: table => new
                {
                    IdMateriaAlumno = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAlumno = table.Column<int>(type: "int", nullable: true),
                    IdMateria = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MateriasAlumnos", x => x.IdMateriaAlumno);
                    table.ForeignKey(
                        name: "FK_MateriasAlumnos_Alumnos_IdAlumno",
                        column: x => x.IdAlumno,
                        principalTable: "Alumnos",
                        principalColumn: "IdAlumno",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MateriasAlumnos_Materias_IdMateria",
                        column: x => x.IdMateria,
                        principalTable: "Materias",
                        principalColumn: "IdMateria",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e52c169c-cd4a-473a-becc-0dba41c78423",
                column: "ConcurrencyStamp",
                value: "04c81038-eae6-4764-a4a6-9296590af48b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "72c7e21c-ac55-46b4-a2ee-df24af6b45e4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "905ca6f9-5516-4e1e-bc9f-4c2ec7427a21", "AQAAAAEAACcQAAAAEH7juiTS00N36hUoV9Xrg1lNqHfaPw+z9erPrR3WFwyqvO3Xm9KEzefTR8Mx68X5rw==", "95915327-25ff-47f3-85a8-a1947d58abd4" });

            migrationBuilder.CreateIndex(
                name: "IX_MateriasAlumnos_IdAlumno",
                table: "MateriasAlumnos",
                column: "IdAlumno");

            migrationBuilder.CreateIndex(
                name: "IX_MateriasAlumnos_IdMateria",
                table: "MateriasAlumnos",
                column: "IdMateria");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MateriasAlumnos");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e52c169c-cd4a-473a-becc-0dba41c78423",
                column: "ConcurrencyStamp",
                value: "01285cc8-caa8-49dc-b010-37aefe3824ae");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "72c7e21c-ac55-46b4-a2ee-df24af6b45e4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9ca6a99a-33d4-471c-89de-49f2cc743519", "AQAAAAEAACcQAAAAELYB2oKuSaIEsGPf1tWS7kteSASQng19n7FDZ3WeAmcfBjiWh/y49dYFEKRYdM92tw==", "9711e3a6-dd49-4868-b39d-07107194deef" });
        }
    }
}
