﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Planner.Data;

#nullable disable

namespace Planner.Migrations
{
    [DbContext(typeof(PlannerDbContext))]
    [Migration("20230831173002_Initial Migration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PatientWeekDay", b =>
                {
                    b.Property<Guid>("PatientsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WeekDaysId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PatientsId", "WeekDaysId");

                    b.HasIndex("WeekDaysId");

                    b.ToTable("PatientWeekDay");
                });

            modelBuilder.Entity("Planner.Models.Domain.Patient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegistrationDay")
                        .HasColumnType("datetime2");

                    b.Property<string>("Research")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("Planner.Models.Domain.WeekDay", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ActivityDay")
                        .HasColumnType("int");

                    b.Property<DateTime>("Day")
                        .HasColumnType("datetime2");

                    b.Property<double>("QuantityMbK")
                        .HasColumnType("float");

                    b.Property<int>("RegisteredPatients")
                        .HasColumnType("int");

                    b.Property<int>("RemainingPatients")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("WeekDays");
                });

            modelBuilder.Entity("PatientWeekDay", b =>
                {
                    b.HasOne("Planner.Models.Domain.Patient", null)
                        .WithMany()
                        .HasForeignKey("PatientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Planner.Models.Domain.WeekDay", null)
                        .WithMany()
                        .HasForeignKey("WeekDaysId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
