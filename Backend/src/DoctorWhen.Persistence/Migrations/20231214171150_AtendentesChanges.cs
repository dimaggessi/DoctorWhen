using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorWhen.Persistence.Migrations
{
    public partial class AtendentesChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Atendentes");

            migrationBuilder.DropColumn(
                name: "Senha",
                table: "Atendentes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Atendentes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "Atendentes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
