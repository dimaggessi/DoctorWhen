﻿// <auto-generated />
using System;
using DoctorWhen.Persistence.Repository.RepositoryAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DoctorWhen.Persistence.Migrations
{
    [DbContext(typeof(DoctorWhenContext))]
    [Migration("20231123192836_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AtendentePaciente", b =>
                {
                    b.Property<long>("AtendentesId")
                        .HasColumnType("bigint");

                    b.Property<long>("PacientesAtendidosId")
                        .HasColumnType("bigint");

                    b.HasKey("AtendentesId", "PacientesAtendidosId");

                    b.HasIndex("PacientesAtendidosId");

                    b.ToTable("AtendentePaciente");
                });

            modelBuilder.Entity("DoctorWhen.Domain.Entities.Atendente", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTimeOffset>("DataRegistro")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Atendentes");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Atendente");
                });

            modelBuilder.Entity("DoctorWhen.Domain.Entities.Consulta", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long?>("AtendenteId")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("DataConsulta")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DataRegistro")
                        .HasColumnType("datetimeoffset");

                    b.Property<long?>("MedicoId")
                        .HasColumnType("bigint");

                    b.Property<long?>("PacienteId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AtendenteId");

                    b.HasIndex("MedicoId");

                    b.HasIndex("PacienteId");

                    b.ToTable("Consultas");
                });

            modelBuilder.Entity("DoctorWhen.Domain.Entities.Medico", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTimeOffset>("DataRegistro")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Especialidade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Medicos");
                });

            modelBuilder.Entity("DoctorWhen.Domain.Entities.Paciente", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<DateTimeOffset>("DataRegistro")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Endereco")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Idade")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Pacientes");
                });

            modelBuilder.Entity("DoctorWhen.Domain.Entities.Prescricao", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long?>("ConsultaId")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("DataRegistro")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Receita")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ConsultaId");

                    b.ToTable("Prescricoes");
                });

            modelBuilder.Entity("MedicoPaciente", b =>
                {
                    b.Property<long>("MedicosId")
                        .HasColumnType("bigint");

                    b.Property<long>("PacientesId")
                        .HasColumnType("bigint");

                    b.HasKey("MedicosId", "PacientesId");

                    b.HasIndex("PacientesId");

                    b.ToTable("MedicoPaciente");
                });

            modelBuilder.Entity("DoctorWhen.Domain.Entities.Administrador", b =>
                {
                    b.HasBaseType("DoctorWhen.Domain.Entities.Atendente");

                    b.HasDiscriminator().HasValue("Administrador");
                });

            modelBuilder.Entity("AtendentePaciente", b =>
                {
                    b.HasOne("DoctorWhen.Domain.Entities.Atendente", null)
                        .WithMany()
                        .HasForeignKey("AtendentesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DoctorWhen.Domain.Entities.Paciente", null)
                        .WithMany()
                        .HasForeignKey("PacientesAtendidosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DoctorWhen.Domain.Entities.Consulta", b =>
                {
                    b.HasOne("DoctorWhen.Domain.Entities.Atendente", "Atendente")
                        .WithMany()
                        .HasForeignKey("AtendenteId");

                    b.HasOne("DoctorWhen.Domain.Entities.Medico", "Medico")
                        .WithMany("Consultas")
                        .HasForeignKey("MedicoId");

                    b.HasOne("DoctorWhen.Domain.Entities.Paciente", "Paciente")
                        .WithMany("Consultas")
                        .HasForeignKey("PacienteId");

                    b.Navigation("Atendente");

                    b.Navigation("Medico");

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("DoctorWhen.Domain.Entities.Prescricao", b =>
                {
                    b.HasOne("DoctorWhen.Domain.Entities.Consulta", "Consulta")
                        .WithMany()
                        .HasForeignKey("ConsultaId");

                    b.Navigation("Consulta");
                });

            modelBuilder.Entity("MedicoPaciente", b =>
                {
                    b.HasOne("DoctorWhen.Domain.Entities.Medico", null)
                        .WithMany()
                        .HasForeignKey("MedicosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DoctorWhen.Domain.Entities.Paciente", null)
                        .WithMany()
                        .HasForeignKey("PacientesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DoctorWhen.Domain.Entities.Medico", b =>
                {
                    b.Navigation("Consultas");
                });

            modelBuilder.Entity("DoctorWhen.Domain.Entities.Paciente", b =>
                {
                    b.Navigation("Consultas");
                });
#pragma warning restore 612, 618
        }
    }
}
