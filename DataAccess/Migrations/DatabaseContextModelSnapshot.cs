﻿// <auto-generated />
using System;
using DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Application.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City_code")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("County")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Zip")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.ToTable("City", (string)null);
                });

            modelBuilder.Entity("Core.Entiteti.Food", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("Category_ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal?>("Price")
                        .IsRequired()
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("Category_ID");

                    b.ToTable("Food", (string)null);
                });

            modelBuilder.Entity("Core.Entiteti.Food_category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Food_category", (string)null);
                });

            modelBuilder.Entity("Core.Entiteti.Restaurant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Restaurant", (string)null);
                });

            modelBuilder.Entity("Core.Entiteti.Restaurant_Food", b =>
                {
                    b.Property<Guid?>("Food_ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("Restaurant_ID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Food_ID", "Restaurant_ID");

                    b.HasIndex("Restaurant_ID");

                    b.ToTable("Restaurant_Food", (string)null);
                });

            modelBuilder.Entity("Core.Entiteti.Food", b =>
                {
                    b.HasOne("Core.Entiteti.Food_category", "Category")
                        .WithMany()
                        .HasForeignKey("Category_ID");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Core.Entiteti.Restaurant", b =>
                {
                    b.HasOne("City", null)
                        .WithMany("Restaurants")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("Core.Entiteti.Restaurant_Food", b =>
                {
                    b.HasOne("Core.Entiteti.Food", "Food")
                        .WithMany()
                        .HasForeignKey("Food_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entiteti.Restaurant", "Restaurant")
                        .WithMany()
                        .HasForeignKey("Restaurant_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Food");

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("City", b =>
                {
                    b.Navigation("Restaurants");
                });
#pragma warning restore 612, 618
        }
    }
}
