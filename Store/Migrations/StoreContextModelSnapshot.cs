﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Store.Services;
using System;

namespace Store.Migrations
{
    [DbContext(typeof(StoreContext))]
    partial class StoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("Store.Database.Entities.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ImageId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.ToTable("Brand","public");
                });

            modelBuilder.Entity("Store.Database.Entities.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<string>("Name");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("Image","public");
                });

            modelBuilder.Entity("Store.Database.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Guid");

                    b.Property<int>("Number")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("StateId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Order","public");
                });

            modelBuilder.Entity("Store.Database.Entities.OrderState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("OrderState","public");
                });

            modelBuilder.Entity("Store.Database.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BrandId");

                    b.Property<string>("Description");

                    b.Property<int>("ImageId");

                    b.Property<string>("Name");

                    b.Property<double>("OldPrice");

                    b.Property<double>("Price");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("ImageId");

                    b.ToTable("Product","public");
                });

            modelBuilder.Entity("Store.Database.Entities.Product2Order", b =>
                {
                    b.Property<int>("ProductId");

                    b.Property<int>("OrderId");

                    b.HasKey("ProductId", "OrderId");

                    b.HasIndex("OrderId");

                    b.ToTable("Product2Order","public");
                });

            modelBuilder.Entity("Store.Database.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Age");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Login");

                    b.Property<string>("MiddleName");

                    b.Property<string>("PasswordHash");

                    b.HasKey("Id");

                    b.ToTable("User","public");
                });

            modelBuilder.Entity("Store.Database.Entities.UserToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatingDateTime");

                    b.Property<string>("Token");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserToken","public");
                });

            modelBuilder.Entity("Store.Database.Entities.Brand", b =>
                {
                    b.HasOne("Store.Database.Entities.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Store.Database.Entities.Order", b =>
                {
                    b.HasOne("Store.Database.Entities.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Store.Database.Entities.Product", b =>
                {
                    b.HasOne("Store.Database.Entities.Brand", "Brand")
                        .WithMany("Products")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Store.Database.Entities.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Store.Database.Entities.Product2Order", b =>
                {
                    b.HasOne("Store.Database.Entities.Order", "Order")
                        .WithMany("Product2Orders")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Store.Database.Entities.Product", "Product")
                        .WithMany("Product2Orders")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Store.Database.Entities.UserToken", b =>
                {
                    b.HasOne("Store.Database.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
