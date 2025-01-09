﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ResponseCachingDemo.Models;

#nullable disable

namespace ResponseCachingDemo.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250109180133_Mg1")]
    partial class Mg1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ResponseCachingDemo.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(8,2)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Product A Description",
                            Name = "Product A",
                            Price = 10.0m
                        },
                        new
                        {
                            Id = 2,
                            Description = "Product B Description",
                            Name = "Product B",
                            Price = 20.0m
                        },
                        new
                        {
                            Id = 3,
                            Description = "Product C Description",
                            Name = "Product C",
                            Price = 30.0m
                        },
                        new
                        {
                            Id = 4,
                            Description = "Product D Description",
                            Name = "Product D",
                            Price = 40.0m
                        },
                        new
                        {
                            Id = 5,
                            Description = "Product E Description",
                            Name = "Product E",
                            Price = 50.0m
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
