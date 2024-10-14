﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DiabetWebSite.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240902134122_AddFindRiscResultsTable")]
    partial class AddFindRiscResultsTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BloodPressure", b =>
                {
                    b.Property<int>("BloodPressureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BloodPressureId"));

                    b.Property<int>("Diastolic")
                        .HasColumnType("int");

                    b.Property<DateTime>("MeasurementTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Systolic")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("BloodPressureId");

                    b.HasIndex("UserId");

                    b.ToTable("BloodPressures");
                });

            modelBuilder.Entity("BloodSugar", b =>
                {
                    b.Property<int>("BloodSugarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BloodSugarId"));

                    b.Property<DateTime>("MeasurementTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("MeasurementValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("BloodSugarId");

                    b.HasIndex("UserId");

                    b.ToTable("BloodSugars");
                });

            modelBuilder.Entity("DiabetWebSite.Models.BodyMassIndex", b =>
                {
                    b.Property<int>("BodyMassIndexId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BodyMassIndexId"));

                    b.Property<decimal>("BMICalculated")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("HeightCm")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("MeasurementTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<decimal>("WeightKg")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("BodyMassIndexId");

                    b.HasIndex("UserId");

                    b.ToTable("BodyMassIndexes", (string)null);
                });

            modelBuilder.Entity("DiabetWebSite.Models.UserAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnswerId")
                        .HasColumnType("int");

                    b.Property<int?>("FindRiscResultId")
                        .HasColumnType("int");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FindRiscResultId");

                    b.ToTable("UserAnswer");
                });

            modelBuilder.Entity("DiabetesControl", b =>
                {
                    b.Property<int>("DiabetesControlId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DiabetesControlId"));

                    b.Property<decimal>("Hba1c")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("MeasurementTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("DiabetesControlId");

                    b.HasIndex("UserId");

                    b.ToTable("DiabetesControls");
                });

            modelBuilder.Entity("FindRiscResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DegreeOfRisk")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoutineScreening")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenYearRiskRating")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TestDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TotalRiskPoints")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("FindRiscResults");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BloodPressure", b =>
                {
                    b.HasOne("User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BloodSugar", b =>
                {
                    b.HasOne("User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DiabetWebSite.Models.BodyMassIndex", b =>
                {
                    b.HasOne("User", "User")
                        .WithMany("BodyMassIndexes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DiabetWebSite.Models.UserAnswer", b =>
                {
                    b.HasOne("FindRiscResult", null)
                        .WithMany("UserAnswers")
                        .HasForeignKey("FindRiscResultId");
                });

            modelBuilder.Entity("DiabetesControl", b =>
                {
                    b.HasOne("User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FindRiscResult", b =>
                {
                    b.Navigation("UserAnswers");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Navigation("BodyMassIndexes");
                });
#pragma warning restore 612, 618
        }
    }
}
