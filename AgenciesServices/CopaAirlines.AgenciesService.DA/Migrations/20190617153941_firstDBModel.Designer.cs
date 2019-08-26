﻿// <auto-generated />
using System;
using CopaAirlines.AgenciesService.DA;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CopaAirlines.AgenciesService.DA.Migrations
{
    [DbContext(typeof(DbContextAgenciesServices))]
    [Migration("20190617153941_firstDBModel")]
    partial class firstDBModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CopaAirlines.AgenciesService.DA.DBModels.Agencies", b =>
                {
                    b.Property<int>("AgencyID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Email")
                        .HasMaxLength(100);

                    b.Property<string>("IATACode")
                        .HasMaxLength(8);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20);

                    b.HasKey("AgencyID");

                    b.ToTable("Agencies");
                });

            modelBuilder.Entity("CopaAirlines.AgenciesService.DA.DBModels.AgencieUsers", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AgencyID");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("UserID");

                    b.HasIndex("AgencyID");

                    b.ToTable("AgencyUsers");
                });

            modelBuilder.Entity("CopaAirlines.AgenciesService.DA.DBModels.AgencieUsers", b =>
                {
                    b.HasOne("CopaAirlines.AgenciesService.DA.DBModels.Agencies", "Agency")
                        .WithMany()
                        .HasForeignKey("AgencyID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
