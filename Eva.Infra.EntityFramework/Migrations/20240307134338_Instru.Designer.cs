﻿// <auto-generated />
using System;
using Eva.Infra.EntityFramework.DbContextes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Eva.Infra.EntityFramework.Migrations
{
    [DbContext(typeof(EvaDbContext))]
    [Migration("20240307134338_Instru")]
    partial class Instru
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.26");

            modelBuilder.Entity("Eva.Core.Domain.BaseModels.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Eva.Core.Domain.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Eva.Core.Domain.Models.Authentication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Authentications");
                });

            modelBuilder.Entity("Eva.Core.Domain.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Eva.Core.Domain.Models.Complex", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("FriendlyState")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Imaginary")
                        .HasColumnType("REAL");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("TEXT");

                    b.Property<double>("Real")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("ComplexNumbers");
                });

            modelBuilder.Entity("Eva.Core.Domain.Models.Cryptography.AesCryptography", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AesCryptographies");
                });

            modelBuilder.Entity("Eva.Core.Domain.Models.Cryptography.DesCryptography", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("DesCryptographies");
                });

            modelBuilder.Entity("Eva.Core.Domain.Models.Cryptography.RsaCryptography", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("RsaCryptographies");
                });

            modelBuilder.Entity("Eva.Core.Domain.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CompanyId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Eva.Core.Domain.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Eva.Core.Domain.Models.EvaLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("LogTypeCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Payload")
                        .HasColumnType("TEXT");

                    b.Property<string>("RequestMethod")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RequestUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Response")
                        .HasColumnType("TEXT");

                    b.Property<string>("StatusCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("EvaLogs");
                });

            modelBuilder.Entity("Eva.Core.Domain.Models.Instrument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Instruments");
                });

            modelBuilder.Entity("Eva.Core.Domain.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Eva.Core.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Signature")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Eva.Core.Domain.Models.UserRoleMapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("TEXT");

                    b.Property<int>("RoleId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId", "RoleId")
                        .IsUnique();

                    b.ToTable("UserRoleMappings");
                });

            modelBuilder.Entity("Eva.Core.Domain.Models.Account", b =>
                {
                    b.HasOne("Eva.Core.Domain.Models.Account", null)
                        .WithMany("Accounts")
                        .HasForeignKey("AccountId");
                });

            modelBuilder.Entity("Eva.Core.Domain.Models.Department", b =>
                {
                    b.HasOne("Eva.Core.Domain.Models.Company", null)
                        .WithMany("Departments")
                        .HasForeignKey("CompanyId");
                });

            modelBuilder.Entity("Eva.Core.Domain.Models.Employee", b =>
                {
                    b.HasOne("Eva.Core.Domain.Models.Department", null)
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId");
                });

            modelBuilder.Entity("Eva.Core.Domain.Models.EvaLog", b =>
                {
                    b.HasOne("Eva.Core.Domain.Models.User", "User")
                        .WithMany("EvaLogs")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Eva.Core.Domain.Models.UserRoleMapping", b =>
                {
                    b.HasOne("Eva.Core.Domain.Models.Role", "Role")
                        .WithMany("UserRoleMappings")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Eva.Core.Domain.Models.User", "User")
                        .WithMany("UserRoleMappings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Eva.Core.Domain.Models.Account", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("Eva.Core.Domain.Models.Company", b =>
                {
                    b.Navigation("Departments");
                });

            modelBuilder.Entity("Eva.Core.Domain.Models.Department", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Eva.Core.Domain.Models.Role", b =>
                {
                    b.Navigation("UserRoleMappings");
                });

            modelBuilder.Entity("Eva.Core.Domain.Models.User", b =>
                {
                    b.Navigation("EvaLogs");

                    b.Navigation("UserRoleMappings");
                });
#pragma warning restore 612, 618
        }
    }
}
