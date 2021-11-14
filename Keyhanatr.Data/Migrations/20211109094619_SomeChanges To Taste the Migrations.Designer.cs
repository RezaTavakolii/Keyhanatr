﻿// <auto-generated />
using System;
using Keyhanatr.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Keyhanatr.Data.Migrations
{
    [DbContext(typeof(KeyhanatrContext))]
<<<<<<< HEAD
<<<<<<< HEAD:Keyhanatr.Data/Migrations/20211108182313_InitDB.Designer.cs
<<<<<<< HEAD:Keyhanatr.Data/Migrations/20211108182313_InitDB.Designer.cs
    [Migration("20211108182313_InitDB")]
    partial class InitDB
=======
=======
    [Migration("20211109094619_SomeChanges To Taste the Migrations")]
    partial class SomeChangesToTastetheMigrations
>>>>>>> 22c6bc1d321c9e903ea9ef20618be9c12255727f:Keyhanatr.Data/Migrations/20211109094619_SomeChanges To Taste the Migrations.Designer.cs
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Keyhanatr.Data.Domain.Products.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(600)
                        .HasColumnType("nvarchar(600)");

                    b.Property<string>("ImageName")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductGroupId")
                        .HasColumnType("int");

                    b.Property<int>("ProductSubGroupId")
                        .HasColumnType("int");

                    b.Property<string>("ProductTitle")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Tags")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("ProductId");

                    b.HasIndex("ProductSubGroupId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Keyhanatr.Data.Domain.Products.ProductGroup", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GroupTitle")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("GroupId");

                    b.ToTable("ProductGroups");
                });

            modelBuilder.Entity("Keyhanatr.Data.Domain.Products.ProductSubGroup", b =>
                {
                    b.Property<int>("SubGroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

<<<<<<< HEAD
=======
<<<<<<< HEAD:Keyhanatr.Data/Migrations/20211109094226_Add_New_Mig.Designer.cs
                    b.Property<int?>("DecimalPrice")
                        .HasColumnType("int");

=======
>>>>>>> 22c6bc1d321c9e903ea9ef20618be9c12255727f:Keyhanatr.Data/Migrations/20211109094619_SomeChanges To Taste the Migrations.Designer.cs
>>>>>>> 22c6bc1d321c9e903ea9ef20618be9c12255727f
                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("SubGroupTitle")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("SubGroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("ProductSubGroups");
                });

            modelBuilder.Entity("Keyhanatr.Data.Domain.User.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("RoleTitle")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Keyhanatr.Data.Domain.User.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActiveCode")
                        .HasColumnType("nvarchar(max)");

<<<<<<< HEAD
=======
                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

>>>>>>> 22c6bc1d321c9e903ea9ef20618be9c12255727f
                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

<<<<<<< HEAD
                    b.Property<int?>("Rate")
                        .HasColumnType("int");

=======
>>>>>>> 22c6bc1d321c9e903ea9ef20618be9c12255727f
                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Keyhanatr.Data.Domain.Products.Product", b =>
                {
                    b.HasOne("Keyhanatr.Data.Domain.Products.ProductSubGroup", "ProductSubGroup")
                        .WithMany("Products")
                        .HasForeignKey("ProductSubGroupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ProductSubGroup");
                });

            modelBuilder.Entity("Keyhanatr.Data.Domain.Products.ProductSubGroup", b =>
                {
                    b.HasOne("Keyhanatr.Data.Domain.Products.ProductGroup", "ProductGroup")
                        .WithMany("ProductSubGroups")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ProductGroup");
                });

            modelBuilder.Entity("Keyhanatr.Data.Domain.User.User", b =>
                {
                    b.HasOne("Keyhanatr.Data.Domain.User.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Keyhanatr.Data.Domain.Products.ProductGroup", b =>
                {
                    b.Navigation("ProductSubGroups");
                });

            modelBuilder.Entity("Keyhanatr.Data.Domain.Products.ProductSubGroup", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Keyhanatr.Data.Domain.User.Role", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
