﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WendingMachineDAL.EF;

namespace WendingMachineDAL.Migrations
{
    [DbContext(typeof(WendingDbContext))]
    partial class WendingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WendingMachineDAL.Entities.Coin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountCoins")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.Property<int?>("WendingMachineId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WendingMachineId");

                    b.ToTable("Coins");
                });

            modelBuilder.Entity("WendingMachineDAL.Entities.Drink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("WendingMachineId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WendingMachineId");

                    b.ToTable("Drinks");
                });

            modelBuilder.Entity("WendingMachineDAL.Entities.WendingMachine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.HasKey("Id");

                    b.ToTable("WendingMachine");
                });

            modelBuilder.Entity("WendingMachineDAL.Entities.Coin", b =>
                {
                    b.HasOne("WendingMachineDAL.Entities.WendingMachine", "WendingMachine")
                        .WithMany("Coins")
                        .HasForeignKey("WendingMachineId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("WendingMachine");
                });

            modelBuilder.Entity("WendingMachineDAL.Entities.Drink", b =>
                {
                    b.HasOne("WendingMachineDAL.Entities.WendingMachine", "WendingMachine")
                        .WithMany("Drinks")
                        .HasForeignKey("WendingMachineId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("WendingMachine");
                });

            modelBuilder.Entity("WendingMachineDAL.Entities.WendingMachine", b =>
                {
                    b.Navigation("Coins");

                    b.Navigation("Drinks");
                });
#pragma warning restore 612, 618
        }
    }
}
