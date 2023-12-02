using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorWhen.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Atendentes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataRegistro = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atendentes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medicos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Especialidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataRegistro = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Idade = table.Column<int>(type: "int", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataRegistro = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AtendentePaciente",
                columns: table => new
                {
                    AtendentesId = table.Column<long>(type: "bigint", nullable: false),
                    PacientesAtendidosId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtendentePaciente", x => new { x.AtendentesId, x.PacientesAtendidosId });
                    table.ForeignKey(
                        name: "FK_AtendentePaciente_Atendentes_AtendentesId",
                        column: x => x.AtendentesId,
                        principalTable: "Atendentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AtendentePaciente_Pacientes_PacientesAtendidosId",
                        column: x => x.PacientesAtendidosId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Consultas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataConsulta = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    MedicoId = table.Column<long>(type: "bigint", nullable: true),
                    PacienteId = table.Column<long>(type: "bigint", nullable: true),
                    AtendenteId = table.Column<long>(type: "bigint", nullable: true),
                    DataRegistro = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consultas_Atendentes_AtendenteId",
                        column: x => x.AtendenteId,
                        principalTable: "Atendentes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Consultas_Medicos_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Medicos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Consultas_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MedicoPaciente",
                columns: table => new
                {
                    MedicosId = table.Column<long>(type: "bigint", nullable: false),
                    PacientesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicoPaciente", x => new { x.MedicosId, x.PacientesId });
                    table.ForeignKey(
                        name: "FK_MedicoPaciente_Medicos_MedicosId",
                        column: x => x.MedicosId,
                        principalTable: "Medicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicoPaciente_Pacientes_PacientesId",
                        column: x => x.PacientesId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prescricoes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsultaId = table.Column<long>(type: "bigint", nullable: true),
                    Receita = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataRegistro = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescricoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prescricoes_Consultas_ConsultaId",
                        column: x => x.ConsultaId,
                        principalTable: "Consultas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AtendentePaciente_PacientesAtendidosId",
                table: "AtendentePaciente",
                column: "PacientesAtendidosId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_AtendenteId",
                table: "Consultas",
                column: "AtendenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_MedicoId",
                table: "Consultas",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_PacienteId",
                table: "Consultas",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicoPaciente_PacientesId",
                table: "MedicoPaciente",
                column: "PacientesId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescricoes_ConsultaId",
                table: "Prescricoes",
                column: "ConsultaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AtendentePaciente");

            migrationBuilder.DropTable(
                name: "MedicoPaciente");

            migrationBuilder.DropTable(
                name: "Prescricoes");

            migrationBuilder.DropTable(
                name: "Consultas");

            migrationBuilder.DropTable(
                name: "Atendentes");

            migrationBuilder.DropTable(
                name: "Medicos");

            migrationBuilder.DropTable(
                name: "Pacientes");
        }
    }
}
