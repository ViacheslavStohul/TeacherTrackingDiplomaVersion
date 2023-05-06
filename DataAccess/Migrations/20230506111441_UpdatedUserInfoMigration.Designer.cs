﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230506111441_UpdatedUserInfoMigration")]
    partial class UpdatedUserInfoMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DataAccess.Entities.AccessLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Chair")
                        .HasColumnType("bit");

                    b.Property<bool>("Comission")
                        .HasColumnType("bit");

                    b.Property<bool>("Departament")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("AccessLevels");
                });

            modelBuilder.Entity("DataAccess.Entities.BanLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("BanEnded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("BanStarter")
                        .HasColumnType("datetime2");

                    b.Property<string>("Ip")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("BanLogs");
                });

            modelBuilder.Entity("DataAccess.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("DataAccess.Entities.Chair", b =>
                {
                    b.Property<int>("ChairId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ChairId"), 1L, 1);

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ChairId");

                    b.ToTable("Chairs");
                });

            modelBuilder.Entity("DataAccess.Entities.ChairHead", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ChairId")
                        .HasColumnType("int");

                    b.Property<int>("HeadIdUserInfo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChairId");

                    b.HasIndex("HeadIdUserInfo");

                    b.ToTable("ChairHeads");
                });

            modelBuilder.Entity("DataAccess.Entities.Comission", b =>
                {
                    b.Property<int>("ComissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ComissionId"), 1L, 1);

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("DepartamentId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ComissionId");

                    b.HasIndex("DepartamentId");

                    b.ToTable("Comissions");
                });

            modelBuilder.Entity("DataAccess.Entities.ComissionHead", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ComissionId")
                        .HasColumnType("int");

                    b.Property<int>("HeadIdUserInfo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ComissionId");

                    b.HasIndex("HeadIdUserInfo");

                    b.ToTable("ComissionHeads");
                });

            modelBuilder.Entity("DataAccess.Entities.CompleteMigration", b =>
                {
                    b.Property<string>("CompleteMigrationId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CompleteMigrationId");

                    b.ToTable("_CompleteMigrations");
                });

            modelBuilder.Entity("DataAccess.Entities.Departament", b =>
                {
                    b.Property<int>("DepartamentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepartamentId"), 1L, 1);

                    b.Property<string>("Abbreviatoin")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("ChairId")
                        .HasColumnType("int");

                    b.Property<int>("HeadIdUserInfo")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("DepartamentId");

                    b.HasIndex("ChairId");

                    b.HasIndex("HeadIdUserInfo");

                    b.ToTable("Departaments");
                });

            modelBuilder.Entity("DataAccess.Entities.Log", b =>
                {
                    b.Property<int>("IdLog")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdLog"), 1L, 1);

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Ip")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("ObjectTabke")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<int>("Result")
                        .HasColumnType("int");

                    b.Property<string>("Target")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("Time")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<int>("UserIdUserInfo")
                        .HasColumnType("int");

                    b.HasKey("IdLog");

                    b.HasIndex("UserIdUserInfo");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("DataAccess.Entities.Methodical", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("UserInfoIdUserInfo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserInfoIdUserInfo");

                    b.ToTable("Methodicals");
                });

            modelBuilder.Entity("DataAccess.Entities.OrganizationalWork", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("OrganizationType")
                        .HasColumnType("int");

                    b.Property<int?>("UserInfoIdUserInfo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserInfoIdUserInfo");

                    b.ToTable("OrganizationalWorks");
                });

            modelBuilder.Entity("DataAccess.Entities.Qualification", b =>
                {
                    b.Property<int>("QualificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QualificationId"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserInfoIdUserInfo")
                        .HasColumnType("int");

                    b.HasKey("QualificationId");

                    b.HasIndex("UserInfoIdUserInfo");

                    b.ToTable("Qualifications");
                });

            modelBuilder.Entity("DataAccess.Entities.Rank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Ranks");
                });

            modelBuilder.Entity("DataAccess.Entities.User", b =>
                {
                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("IdUser");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DataAccess.Entities.UserInfo", b =>
                {
                    b.Property<int>("IdUserInfo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUserInfo"), 1L, 1);

                    b.Property<int>("AccessLevelId")
                        .HasColumnType("int");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int?>("ChairId")
                        .HasColumnType("int");

                    b.Property<int?>("ComissionId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DeletionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("RankId")
                        .HasColumnType("int");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("WorkTypeId")
                        .HasColumnType("int");

                    b.HasKey("IdUserInfo");

                    b.HasIndex("AccessLevelId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ChairId");

                    b.HasIndex("ComissionId");

                    b.HasIndex("RankId");

                    b.HasIndex("WorkTypeId");

                    b.ToTable("UserInfos");
                });

            modelBuilder.Entity("DataAccess.Entities.WorkType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WorkTypes");
                });

            modelBuilder.Entity("DataAccess.Entities.ChairHead", b =>
                {
                    b.HasOne("DataAccess.Entities.Chair", "Chair")
                        .WithMany()
                        .HasForeignKey("ChairId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataAccess.Entities.UserInfo", "Head")
                        .WithMany()
                        .HasForeignKey("HeadIdUserInfo")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Chair");

                    b.Navigation("Head");
                });

            modelBuilder.Entity("DataAccess.Entities.Comission", b =>
                {
                    b.HasOne("DataAccess.Entities.Departament", "Departament")
                        .WithMany("Comissions")
                        .HasForeignKey("DepartamentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Departament");
                });

            modelBuilder.Entity("DataAccess.Entities.ComissionHead", b =>
                {
                    b.HasOne("DataAccess.Entities.Comission", "Comission")
                        .WithMany()
                        .HasForeignKey("ComissionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataAccess.Entities.UserInfo", "Head")
                        .WithMany()
                        .HasForeignKey("HeadIdUserInfo")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Comission");

                    b.Navigation("Head");
                });

            modelBuilder.Entity("DataAccess.Entities.Departament", b =>
                {
                    b.HasOne("DataAccess.Entities.Chair", "Chair")
                        .WithMany()
                        .HasForeignKey("ChairId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataAccess.Entities.UserInfo", "Head")
                        .WithMany()
                        .HasForeignKey("HeadIdUserInfo")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Chair");

                    b.Navigation("Head");
                });

            modelBuilder.Entity("DataAccess.Entities.Log", b =>
                {
                    b.HasOne("DataAccess.Entities.UserInfo", "User")
                        .WithMany()
                        .HasForeignKey("UserIdUserInfo")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccess.Entities.Methodical", b =>
                {
                    b.HasOne("DataAccess.Entities.UserInfo", null)
                        .WithMany("MethodicalWorks")
                        .HasForeignKey("UserInfoIdUserInfo")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DataAccess.Entities.OrganizationalWork", b =>
                {
                    b.HasOne("DataAccess.Entities.UserInfo", null)
                        .WithMany("OrganizationalWorks")
                        .HasForeignKey("UserInfoIdUserInfo")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DataAccess.Entities.Qualification", b =>
                {
                    b.HasOne("DataAccess.Entities.UserInfo", null)
                        .WithMany("Qualifications")
                        .HasForeignKey("UserInfoIdUserInfo")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DataAccess.Entities.User", b =>
                {
                    b.HasOne("DataAccess.Entities.UserInfo", "UserInfo")
                        .WithOne("User")
                        .HasForeignKey("DataAccess.Entities.User", "IdUser")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("UserInfo");
                });

            modelBuilder.Entity("DataAccess.Entities.UserInfo", b =>
                {
                    b.HasOne("DataAccess.Entities.AccessLevel", "AccessLevel")
                        .WithMany()
                        .HasForeignKey("AccessLevelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataAccess.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DataAccess.Entities.Chair", "Chair")
                        .WithMany()
                        .HasForeignKey("ChairId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DataAccess.Entities.Comission", "Comission")
                        .WithMany()
                        .HasForeignKey("ComissionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DataAccess.Entities.Rank", "Rank")
                        .WithMany()
                        .HasForeignKey("RankId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DataAccess.Entities.WorkType", "WorkType")
                        .WithMany()
                        .HasForeignKey("WorkTypeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("AccessLevel");

                    b.Navigation("Category");

                    b.Navigation("Chair");

                    b.Navigation("Comission");

                    b.Navigation("Rank");

                    b.Navigation("WorkType");
                });

            modelBuilder.Entity("DataAccess.Entities.Departament", b =>
                {
                    b.Navigation("Comissions");
                });

            modelBuilder.Entity("DataAccess.Entities.UserInfo", b =>
                {
                    b.Navigation("MethodicalWorks");

                    b.Navigation("OrganizationalWorks");

                    b.Navigation("Qualifications");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}