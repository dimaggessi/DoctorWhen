using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorWhen.Persistence.Migrations
{
    public partial class NormalizedNameRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "ff74cc6c-7d9a-40f5-9ce6-d028a229d79b", "ADMIN" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "b90b8633-8936-4438-93ac-1853cf6a8df0", "ATENDENTE" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "bb1ae961-96e4-41a2-a62b-0729ba3cfe85", "AQAAAAEAACcQAAAAEObGqHIrsXsdExsZPWrQ3slR/xAivMsxE1gMyDXHDQnlNjz2AXWlGjy/y9zkMU2m6Q==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "35daacfa-5b07-4d4c-81d7-a31689cdfdb5", null });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "c152eb8c-bdb2-43dd-82ee-7132c4c630b4", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e2226fa7-b901-4860-9609-31e23373ad9d", "AQAAAAEAACcQAAAAEPUnT4NB5kINs4nBRxfLxLaPB4ed5ZLjrIp9iQBaLqWPTLrH7aazYqDhvfd0QUz2OA==" });
        }
    }
}
