﻿// <auto-generated />
using System;
using Keyhanatr.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Keyhanatr.Data.Migrations
{
    [DbContext(typeof(KeyhanatrContext))]
    partial class KeyhanatrContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ExistId")
                        .HasColumnType("int");

                    b.Property<string>("ImageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int?>("ProductExist")
                        .HasColumnType("int");

                    b.Property<int>("ProductGroupId")
                        .HasColumnType("int");

                    b.Property<int>("ProductSubGroupId")
                        .HasColumnType("int");

                    b.Property<string>("ProductTitle")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int?>("SalesCount")
                        .HasColumnType("int");

                    b.Property<int?>("SumSalesUntilNow")
                        .HasColumnType("int");

                    b.Property<string>("Tags")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ProductId");

                    b.HasIndex("ProductGroupId");

                    b.HasIndex("ProductSubGroupId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Keyhanatr.Data.Domain.Products.ProductColor", b =>
                {
                    b.Property<int>("ColorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ColorCode")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ColorName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ImageName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("ProductExist")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("ColorId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductColors");
                });

            modelBuilder.Entity("Keyhanatr.Data.Domain.Products.ProductComment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsAccepted")
                        .HasColumnType("bit");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CommentId");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("ProductComments");
                });

            modelBuilder.Entity("Keyhanatr.Data.Domain.Products.ProductFeature", b =>
                {
                    b.Property<int>("FeatureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FeatureTitle")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("FeatureId");

                    b.ToTable("ProductFeatures");
                });

            modelBuilder.Entity("Keyhanatr.Data.Domain.Products.ProductGallery", b =>
                {
                    b.Property<int>("GalleryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImageName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("GalleryId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductGalleries");
                });

            modelBuilder.Entity("Keyhanatr.Data.Domain.Products.ProductGroup", b =>
                {
                    b.Property<int>("ProductGroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GroupTitle")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ProductGroupId");

                    b.ToTable("ProductGroups");
                });

            modelBuilder.Entity("Keyhanatr.Data.Domain.Products.ProductSelectedFeature", b =>
                {
                    b.Property<int>("SelectedFeatureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FeatureId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("SelectedFeatureId");

                    b.HasIndex("FeatureId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductSelectedFeatures");
                });

            modelBuilder.Entity("Keyhanatr.Data.Domain.Products.ProductSubGroup", b =>
                {
                    b.Property<int>("SubGroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProductGroupId")
                        .HasColumnType("int");

                    b.Property<string>("SubGroupTitle")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("SubGroupId");

                    b.HasIndex("ProductGroupId");

                    b.ToTable("ProductSubGroups");
                });

            modelBuilder.Entity("Keyhanatr.Data.Domain.Slider.Slider", b =>
                {
                    b.Property<int>("SlideID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SlideID");

                    b.ToTable("Sliders");
                });

            modelBuilder.Entity("Keyhanatr.Data.Domain.User.Address", b =>
                {
                    b.Property<int>("AddressID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CodePosti")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Company")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Family")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ostan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Shahr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("AddressID");

                    b.HasIndex("UserId");

                    b.ToTable("Addresses");
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

                    b.Property<int?>("Rate")
                        .HasColumnType("int");

                    b.Property<string>("RegisterDate")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

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

            modelBuilder.Entity("Keyhanatr.Data.Domain.User.UserInfo", b =>
                {
                    b.Property<int>("UserInfoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Family")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageName")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneDaftar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserInfoID");

                    b.HasIndex("UserId");

                    b.ToTable("UserInfos");
                });

            modelBuilder.Entity("Keyhanatr.Data.Domain.Products.Product", b =>
                {
                    b.HasOne("Keyhanatr.Data.Domain.Products.ProductGroup", "ProductGroup")
                        .WithMany("Products")
                        .HasForeignKey("ProductGroupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Keyhanatr.Data.Domain.Products.ProductSubGroup", "ProductSubGroup")
                        .WithMany("Products")
                        .HasForeignKey("ProductSubGroupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ProductGroup");

                    b.Navigation("ProductSubGroup");
                });

            modelBuilder.Entity("Keyhanatr.Data.Domain.Products.ProductColor", b =>
                {
                    b.HasOne("Keyhanatr.Data.Domain.Products.Product", "Product")
                        .WithMany("ProductColors")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Keyhanatr.Data.Domain.Products.ProductComment", b =>
                {
                    b.HasOne("Keyhanatr.Data.Domain.Products.Product", "Product")
                        .WithMany("ProductComments")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Keyhanatr.Data.Domain.User.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Keyhanatr.Data.Domain.Products.ProductGallery", b =>
                {
                    b.HasOne("Keyhanatr.Data.Domain.Products.Product", "Product")
                        .WithMany("ProductGalleries")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Keyhanatr.Data.Domain.Products.ProductSelectedFeature", b =>
                {
                    b.HasOne("Keyhanatr.Data.Domain.Products.ProductFeature", "ProductFeature")
                        .WithMany("ProductSelectedFeatures")
                        .HasForeignKey("FeatureId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Keyhanatr.Data.Domain.Products.Product", "Product")
                        .WithMany("ProductSelectedFeatures")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("ProductFeature");
                });

            modelBuilder.Entity("Keyhanatr.Data.Domain.Products.ProductSubGroup", b =>
                {
                    b.HasOne("Keyhanatr.Data.Domain.Products.ProductGroup", "ProductGroup")
                        .WithMany("ProductSubGroups")
                        .HasForeignKey("ProductGroupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ProductGroup");
                });

            modelBuilder.Entity("Keyhanatr.Data.Domain.User.Address", b =>
                {
                    b.HasOne("Keyhanatr.Data.Domain.User.User", "User")
                        .WithMany("Addresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
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

            modelBuilder.Entity("Keyhanatr.Data.Domain.User.UserInfo", b =>
                {
                    b.HasOne("Keyhanatr.Data.Domain.User.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Keyhanatr.Data.Domain.Products.Product", b =>
                {
                    b.Navigation("ProductColors");

                    b.Navigation("ProductComments");

                    b.Navigation("ProductGalleries");

                    b.Navigation("ProductSelectedFeatures");
                });

            modelBuilder.Entity("Keyhanatr.Data.Domain.Products.ProductFeature", b =>
                {
                    b.Navigation("ProductSelectedFeatures");
                });

            modelBuilder.Entity("Keyhanatr.Data.Domain.Products.ProductGroup", b =>
                {
                    b.Navigation("Products");

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

            modelBuilder.Entity("Keyhanatr.Data.Domain.User.User", b =>
                {
                    b.Navigation("Addresses");
                });
#pragma warning restore 612, 618
        }
    }
}
