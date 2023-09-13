﻿// <auto-generated />
using System;
using CountersLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CountersLibrary.Migrations
{
    [DbContext(typeof(ApplContext))]
    [Migration("20230120011452_New Entities User and Tarif")]
    partial class NewEntitiesUserandTarif
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CountersLibrary.Cost", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("ColdSpend")
                        .HasColumnType("integer");

                    b.Property<double>("GasCost")
                        .HasColumnType("double precision");

                    b.Property<int>("GasSpend")
                        .HasColumnType("integer");

                    b.Property<int>("HotSpend")
                        .HasColumnType("integer");

                    b.Property<double>("PowerCost")
                        .HasColumnType("double precision");

                    b.Property<int>("PowerSpend")
                        .HasColumnType("integer");

                    b.Property<int>("RRecordID")
                        .HasColumnType("integer");

                    b.Property<int?>("UserID")
                        .HasColumnType("integer");

                    b.Property<double>("WaterCost")
                        .HasColumnType("double precision");

                    b.HasKey("ID");

                    b.HasIndex("RRecordID");

                    b.HasIndex("UserID");

                    b.ToTable("Cost");
                });

            modelBuilder.Entity("CountersLibrary.Record", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("Cold")
                        .HasColumnType("integer");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<int>("Gas")
                        .HasColumnType("integer");

                    b.Property<int>("Hot")
                        .HasColumnType("integer");

                    b.Property<int>("Power")
                        .HasColumnType("integer");

                    b.Property<int?>("UserID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Record");
                });

            modelBuilder.Entity("CountersLibrary.Tarif", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<double>("ColdWater")
                        .HasColumnType("double precision");

                    b.Property<double>("Disposal")
                        .HasColumnType("double precision");

                    b.Property<double>("Gas")
                        .HasColumnType("double precision");

                    b.Property<double>("HotWater")
                        .HasColumnType("double precision");

                    b.Property<double>("Power")
                        .HasColumnType("double precision");

                    b.Property<int>("UserID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Tarif");
                });

            modelBuilder.Entity("CountersLibrary.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("CountersLibrary.Cost", b =>
                {
                    b.HasOne("CountersLibrary.Record", "RRecord")
                        .WithMany()
                        .HasForeignKey("RRecordID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CountersLibrary.User", null)
                        .WithMany("Costs")
                        .HasForeignKey("UserID");

                    b.Navigation("RRecord");
                });

            modelBuilder.Entity("CountersLibrary.Record", b =>
                {
                    b.HasOne("CountersLibrary.User", null)
                        .WithMany("Records")
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("CountersLibrary.Tarif", b =>
                {
                    b.HasOne("CountersLibrary.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CountersLibrary.User", b =>
                {
                    b.Navigation("Costs");

                    b.Navigation("Records");
                });
#pragma warning restore 612, 618
        }
    }
}