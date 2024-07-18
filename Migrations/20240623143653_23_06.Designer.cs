﻿// <auto-generated />
using System;
using Alarm_Project.Repositories;
using Alarm_Project.Repositories.DbRepo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Alarm_Project.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20240623143653_23_06")]
    partial class _23_06
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Alarm_Project.Models.Alarm", b =>
                {
                    b.Property<Guid>("AlarmId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AlarmMessage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AlarmType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AlarmId");

                    b.ToTable("Alarm");
                });

            modelBuilder.Entity("Alarm_Project.Models.AlarmSettings", b =>
                {
                    b.Property<Guid>("AlarmId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("ReceiveEmail")
                        .HasColumnType("bit");

                    b.Property<bool>("ReceiveReport")
                        .HasColumnType("bit");

                    b.Property<bool>("ReceiveSlack")
                        .HasColumnType("bit");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AlarmId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("AlarmSettings");

                    b.HasData(
                        new
                        {
                            AlarmId = new Guid("22222222-2222-2222-2222-222222222222"),
                            ReceiveEmail = false,
                            ReceiveReport = false,
                            ReceiveSlack = false,
                            UserId = new Guid("11111111-1111-1111-1111-111111111111")
                        });
                });

            modelBuilder.Entity("Alarm_Project.Models.Payment", b =>
                {
                    b.Property<Guid>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CVV")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardOwnerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Currency")
                        .HasColumnType("int");

                    b.Property<string>("ExpireYear")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PaymentId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Payments");

                    b.HasData(
                        new
                        {
                            PaymentId = new Guid("22222222-2222-2222-2222-222222222222"),
                            CVV = "000",
                            CardNumber = "4355084355084358",
                            CardOwnerName = "Test Kart",
                            Currency = 2000,
                            ExpireYear = "2030",
                            UserId = new Guid("11111111-1111-1111-1111-111111111111")
                        });
                });

            modelBuilder.Entity("Alarm_Project.Models.Products", b =>
                {
                    b.Property<Guid>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProductDescripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductPrice")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProductId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = new Guid("22222222-2222-2222-2222-222222222222"),
                            ProductDescripcion = "Ateş seni çağırıyor",
                            ProductName = "Mangal",
                            ProductPrice = 700,
                            UserId = new Guid("11111111-1111-1111-1111-111111111111")
                        });
                });

            modelBuilder.Entity("Alarm_Project.Models.Users", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("11111111-1111-1111-1111-111111111111"),
                            Email = "yigitcizmeci@hotmail.com",
                            Password = "cizmeci",
                            UserName = "yigit"
                        });
                });

            modelBuilder.Entity("Alarm_Project.Models.AlarmSettings", b =>
                {
                    b.HasOne("Alarm_Project.Models.Users", "Users")
                        .WithOne("AlarmSettings")
                        .HasForeignKey("Alarm_Project.Models.AlarmSettings", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Alarm_Project.Models.Payment", b =>
                {
                    b.HasOne("Alarm_Project.Models.Users", "Users")
                        .WithOne("Payment")
                        .HasForeignKey("Alarm_Project.Models.Payment", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Alarm_Project.Models.Products", b =>
                {
                    b.HasOne("Alarm_Project.Models.Users", "Users")
                        .WithOne("Products")
                        .HasForeignKey("Alarm_Project.Models.Products", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Alarm_Project.Models.Users", b =>
                {
                    b.Navigation("AlarmSettings")
                        .IsRequired();

                    b.Navigation("Payment")
                        .IsRequired();

                    b.Navigation("Products")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
