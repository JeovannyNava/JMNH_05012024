using Microsoft.EntityFrameworkCore.Migrations;

namespace JMNH_05012024.Migrations
{
    public partial class UsuarioEliminado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e52c169c-cd4a-473a-becc-0dba41c78423",
                column: "ConcurrencyStamp",
                value: "aafc7093-2390-4b80-81e7-f9f55f333667");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e52c169c-cd4a-473a-becc-0dba41c78424",
                column: "ConcurrencyStamp",
                value: "13c54072-3ffc-4a8d-81b8-b93b90ada3d7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "72c7e21c-ac55-46b4-a2ee-df24af6b45e4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bf2cf155-5b1c-4097-b856-0703d6775ad0", "AQAAAAEAACcQAAAAEOACwCOBtyJe0GceT2Q2FOjZFHSdplTE3793YroumXOL5UI4pegU3Q/e9y3JvNDtvw==", "5fd80280-0358-449e-b322-f5c3255cf078" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e52c169c-cd4a-473a-becc-0dba41c78423",
                column: "ConcurrencyStamp",
                value: "eb8a1591-0a91-437d-abf0-c5baba748168");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e52c169c-cd4a-473a-becc-0dba41c78424",
                column: "ConcurrencyStamp",
                value: "c62dac76-5329-4fae-8e11-a1816ce1fc8f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "72c7e21c-ac55-46b4-a2ee-df24af6b45e4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7fe228c8-1b7e-4525-b931-2b71fd2381e3", "AQAAAAEAACcQAAAAEGJNCQpPv7jBiDODZefCY1JiQmY8dNKoml/bj7Fy4r7Jd+uZAMqirMJJUyYWCoi+rQ==", "dbd798e4-f63d-49e0-af69-dedc3bc4e7a8" });
        }
    }
}
