using Microsoft.EntityFrameworkCore.Migrations;

namespace JMNH_05012024.Migrations
{
    public partial class roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e52c169c-cd4a-473a-becc-0dba41c78423",
                column: "ConcurrencyStamp",
                value: "65210c51-21f6-4727-9a91-3a8c9ebf20aa");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e52c169c-cd4a-473a-becc-0dba41c78424", "e6c40764-cf86-4243-bc91-162e2fd70c48", "Alumno", "ALUMNO" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "e52c169c-cd4a-473a-becc-0dba41c78423", "72c7e21c-ac55-46b4-a2ee-df24af6b45e4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "72c7e21c-ac55-46b4-a2ee-df24af6b45e4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e73915b8-91f3-40d1-9a98-0e45a61b2228", "AQAAAAEAACcQAAAAEKA0oycsHgUwhIXMiSaVpDEA9x0sMHpfi0shtN2+Q0ls2J5noStcmf+hRhgNVvUbdw==", "acab0440-193e-4aeb-bbc9-6a2820aae80a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e52c169c-cd4a-473a-becc-0dba41c78424");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e52c169c-cd4a-473a-becc-0dba41c78423", "72c7e21c-ac55-46b4-a2ee-df24af6b45e4" });

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
        }
    }
}
