using Microsoft.EntityFrameworkCore.Migrations;

namespace JMNH_05012024.Migrations
{
    public partial class UserNameAlumno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Alumnos",
                type: "nvarchar(max)",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Alumnos");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e52c169c-cd4a-473a-becc-0dba41c78423",
                column: "ConcurrencyStamp",
                value: "65210c51-21f6-4727-9a91-3a8c9ebf20aa");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e52c169c-cd4a-473a-becc-0dba41c78424",
                column: "ConcurrencyStamp",
                value: "e6c40764-cf86-4243-bc91-162e2fd70c48");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "72c7e21c-ac55-46b4-a2ee-df24af6b45e4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e73915b8-91f3-40d1-9a98-0e45a61b2228", "AQAAAAEAACcQAAAAEKA0oycsHgUwhIXMiSaVpDEA9x0sMHpfi0shtN2+Q0ls2J5noStcmf+hRhgNVvUbdw==", "acab0440-193e-4aeb-bbc9-6a2820aae80a" });
        }
    }
}
