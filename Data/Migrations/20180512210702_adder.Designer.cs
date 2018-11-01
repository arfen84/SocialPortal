﻿// <auto-generated />
using SocialPortal.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace SocialPortal.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180512210702_adder")]
    partial class adder
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SocialPortal.Models.Album", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddDate");

                    b.Property<string>("Adder");

                    b.Property<string>("Author");

                    b.Property<string>("Description");

                    b.Property<string>("Genre");

                    b.Property<string>("Name");

                    b.Property<string>("Photos");

                    b.Property<int>("Popularity");

                    b.Property<DateTime>("PublicDate");

                    b.Property<string>("Tracks");

                    b.Property<string>("Type");

                    b.Property<bool>("verified");

                    b.HasKey("ID");

                    b.ToTable("Albumy");
                });

            modelBuilder.Entity("SocialPortal.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("SocialPortal.Models.Autor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddDate");

                    b.Property<string>("Adder");

                    b.Property<string>("Awards");

                    b.Property<string>("Description");

                    b.Property<string>("Discs");

                    b.Property<string>("Link");

                    b.Property<string>("Name");

                    b.Property<string>("Photos");

                    b.Property<int>("Popularity");

                    b.Property<DateTime>("Year");

                    b.Property<bool>("verified");

                    b.HasKey("ID");

                    b.ToTable("Autorzy");
                });

            modelBuilder.Entity("SocialPortal.Models.Event", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Adder");

                    b.Property<string>("Artist");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<DateTime>("Hour");

                    b.Property<string>("Name");

                    b.Property<string>("Photos");

                    b.Property<string>("Place");

                    b.Property<int>("Price");

                    b.Property<string>("URL");

                    b.Property<string>("Youtube");

                    b.Property<bool>("verified");

                    b.HasKey("ID");

                    b.ToTable("Eventy");
                });

            modelBuilder.Entity("SocialPortal.Models.Relacja", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Adder");

                    b.Property<string>("Artist");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Name");

                    b.Property<string>("Photos");

                    b.Property<string>("Place");

                    b.Property<string>("URL");

                    b.Property<string>("Youtube");

                    b.Property<string>("relation");

                    b.Property<bool>("verified");

                    b.HasKey("ID");

                    b.ToTable("Relacje");
                });

            modelBuilder.Entity("SocialPortal.Models.Tekst", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddDate");

                    b.Property<string>("Adder");

                    b.Property<string>("Author");

                    b.Property<string>("Files");

                    b.Property<string>("Genre");

                    b.Property<string>("Name");

                    b.Property<int>("Popularity");

                    b.Property<string>("Special");

                    b.Property<string>("Text");

                    b.Property<string>("TextAuthor");

                    b.Property<string>("Translate");

                    b.Property<string>("Type");

                    b.Property<DateTime>("Year");

                    b.Property<string>("Youtube");

                    b.Property<bool>("verified");

                    b.HasKey("ID");

                    b.ToTable("Teksty");
                });

            modelBuilder.Entity("SocialPortal.Models.Utwor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddDate");

                    b.Property<string>("Adder");

                    b.Property<string>("Author");

                    b.Property<string>("Compositor");

                    b.Property<string>("Description");

                    b.Property<string>("DiscName");

                    b.Property<string>("MusicType");

                    b.Property<string>("Name");

                    b.Property<int>("Popularity");

                    b.Property<string>("Text");

                    b.Property<string>("TextAuthor");

                    b.Property<string>("Type");

                    b.Property<string>("URL");

                    b.Property<DateTime>("Year");

                    b.Property<int>("rank");

                    b.Property<bool>("verified");

                    b.HasKey("ID");

                    b.ToTable("Utwory");
                });

            modelBuilder.Entity("SocialPortal.Models.Zespol", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddDate");

                    b.Property<string>("Adder");

                    b.Property<string>("Awards");

                    b.Property<string>("Description");

                    b.Property<string>("Discs");

                    b.Property<string>("Link");

                    b.Property<string>("Name");

                    b.Property<string>("Photos");

                    b.Property<int>("Popularity");

                    b.Property<string>("Team");

                    b.Property<DateTime>("Year");

                    b.Property<bool>("verified");

                    b.HasKey("ID");

                    b.ToTable("Zespoly");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("SocialPortal.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SocialPortal.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SocialPortal.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("SocialPortal.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
