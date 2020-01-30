﻿// <auto-generated />
using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductAPI.DataAccess;

namespace ProductAPI.Migrations
{
    [ExcludeFromCodeCoverage]
    [DbContext(typeof(ProductDBContext))]
    [Migration("20200123194340_SeedProductData")]
    partial class SeedProductData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProductAPI.Entities.ProductInfo", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("description")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("expiryDate");

                    b.Property<string>("manufacturer")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<DateTime>("manufacturingDate");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("productType")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.HasKey("id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            id = "0001",
                            description = "Android SmartPhone",
                            expiryDate = new DateTime(2019, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            manufacturer = "Oneplus",
                            manufacturingDate = new DateTime(2019, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            name = "Oneplus",
                            productType = "mobile"
                        },
                        new
                        {
                            id = "0002",
                            description = "HP Thinkpad",
                            expiryDate = new DateTime(2030, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            manufacturer = "HP",
                            manufacturingDate = new DateTime(2015, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            name = "Hp Thinkpad",
                            productType = "Laptop"
                        },
                        new
                        {
                            id = "0003",
                            description = "Nestle Cofee Classic 200g",
                            expiryDate = new DateTime(2019, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            manufacturer = "Nestle",
                            manufacturingDate = new DateTime(2019, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            name = "Nestle Cofee",
                            productType = "Cofee"
                        },
                        new
                        {
                            id = "0004",
                            description = "Brother Printer L23251",
                            expiryDate = new DateTime(2030, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            manufacturer = "Brother",
                            manufacturingDate = new DateTime(2015, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            name = "Brother Printer",
                            productType = "Printer"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}