using Microsoft.EntityFrameworkCore.Migrations;

namespace JMNH_05012024.Migrations
{
    public partial class lockandenableuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
