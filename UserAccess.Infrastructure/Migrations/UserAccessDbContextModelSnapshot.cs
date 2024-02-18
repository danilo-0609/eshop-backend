﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserAccess.Infrastructure;

#nullable disable

namespace UserAccess.Infrastructure.Migrations
{
    [DbContext(typeof(UserAccessDbContext))]
    partial class UserAccessDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("UserAccess.Domain.Permission", b =>
                {
                    b.Property<int>("PermissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PermissionId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("PermissionId");

                    b.HasIndex("RoleId");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("UserAccess.Domain.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("UserAccess.Domain.UserRegistrations.UserRegistration", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UserRegistrationId");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Address");

                    b.Property<DateTime?>("ConfirmedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("ConfirmedDate");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)")
                        .HasColumnName("FirstName");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)")
                        .HasColumnName("LastName");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)")
                        .HasColumnName("Login");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(155)
                        .HasColumnType("nvarchar(155)")
                        .HasColumnName("Name");

                    b.Property<DateTime>("RegisteredDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("RegisteredDate");

                    b.HasKey("Id");

                    b.ToTable("UserRegistrations", "users");
                });

            modelBuilder.Entity("UserAccess.Domain.Users.RolePermission", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.HasKey("RoleId", "PermissionId");

                    b.ToTable("RolePermissions");
                });

            modelBuilder.Entity("UserAccess.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UserId");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Address");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedDateTime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)")
                        .HasColumnName("FirstName");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("IsActive");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)")
                        .HasColumnName("LastName");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)")
                        .HasColumnName("Login");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(155)
                        .HasColumnType("nvarchar(155)")
                        .HasColumnName("Name");

                    b.Property<string>("ProfileImageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ProfileImageName");

                    b.Property<DateTime?>("UpdatedDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("UpdatedDateTime");

                    b.HasKey("Id");

                    b.ToTable("Users", "users");
                });

            modelBuilder.Entity("UserAccess.Domain.Users.UserRole", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RoleId", "UserId")
                        .HasName("Pk_userRoles");

                    b.HasIndex("UsersId");

                    b.HasIndex(new[] { "RoleId" }, "IX_UsersRoles_RoleId");

                    b.HasIndex(new[] { "UserId" }, "IX_UsersRoles_UserId");

                    b.ToTable("UsersRoles", "users");
                });

            modelBuilder.Entity("UserAccess.Infrastructure.Outbox.UserAccessOutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Content");

                    b.Property<string>("Error")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Error");

                    b.Property<DateTime>("OcurredOnUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("OcurredOnUtc");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("ProcessedOnUtc");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Type");

                    b.HasKey("Id");

                    b.ToTable("UserAccessOutboxMessages", "users");
                });

            modelBuilder.Entity("UserAccess.Domain.Permission", b =>
                {
                    b.HasOne("UserAccess.Domain.Role", null)
                        .WithMany("Permissions")
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("UserAccess.Domain.UserRegistrations.UserRegistration", b =>
                {
                    b.OwnsOne("UserAccess.Domain.Common.Password", "Password", b1 =>
                        {
                            b1.Property<Guid>("UserRegistrationId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(150)
                                .HasColumnType("nvarchar(150)")
                                .HasColumnName("Password");

                            b1.HasKey("UserRegistrationId");

                            b1.ToTable("UserRegistrations", "users");

                            b1.WithOwner()
                                .HasForeignKey("UserRegistrationId");
                        });

                    b.OwnsOne("UserAccess.Domain.UserRegistrations.UserRegistrationStatus", "Status", b1 =>
                        {
                            b1.Property<Guid>("UserRegistrationId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("StatusCode");

                            b1.HasKey("UserRegistrationId");

                            b1.ToTable("UserRegistrations", "users");

                            b1.WithOwner()
                                .HasForeignKey("UserRegistrationId");
                        });

                    b.Navigation("Password")
                        .IsRequired();

                    b.Navigation("Status")
                        .IsRequired();
                });

            modelBuilder.Entity("UserAccess.Domain.Users.User", b =>
                {
                    b.OwnsOne("UserAccess.Domain.Common.Password", "Password", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(150)
                                .HasColumnType("nvarchar(150)")
                                .HasColumnName("Password");

                            b1.HasKey("UserId");

                            b1.ToTable("Users", "users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Password")
                        .IsRequired();
                });

            modelBuilder.Entity("UserAccess.Domain.Users.UserRole", b =>
                {
                    b.HasOne("UserAccess.Domain.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UserAccess.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UserAccess.Domain.Role", b =>
                {
                    b.Navigation("Permissions");
                });
#pragma warning restore 612, 618
        }
    }
}
