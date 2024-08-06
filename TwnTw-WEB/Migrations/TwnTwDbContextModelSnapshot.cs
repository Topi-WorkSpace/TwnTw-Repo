﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TwnTw_WEB.Data;

#nullable disable

namespace TwnTw_WEB.Migrations
{
    [DbContext(typeof(TwnTwDbContext))]
    partial class TwnTwDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TwnTw_WEB.Models.MemberDetail", b =>
                {
                    b.Property<Guid>("MemberDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WorkSpaceId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("MemberDetailId");

                    b.HasIndex("UserId");

                    b.HasIndex("WorkSpaceId");

                    b.ToTable("MemberDetails", (string)null);
                });

            modelBuilder.Entity("TwnTw_WEB.Models.TaskDetail", b =>
                {
                    b.Property<Guid>("TaskDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TaskDetailId");

                    b.HasIndex("UserId");

                    b.ToTable("TaskDetails", (string)null);
                });

            modelBuilder.Entity("TwnTw_WEB.Models.User", b =>
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

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("TwnTw_WEB.Models.Workspace", b =>
                {
                    b.Property<Guid>("WSId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WSName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WSId");

                    b.ToTable("Workspaces", (string)null);
                });

            modelBuilder.Entity("TwnTw_WEB.Models.MemberDetail", b =>
                {
                    b.HasOne("TwnTw_WEB.Models.User", "Users")
                        .WithMany("MemberDetails")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TwnTw_WEB.Models.Workspace", "Workspaces")
                        .WithMany("MemberDetails")
                        .HasForeignKey("WorkSpaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");

                    b.Navigation("Workspaces");
                });

            modelBuilder.Entity("TwnTw_WEB.Models.TaskDetail", b =>
                {
                    b.HasOne("TwnTw_WEB.Models.User", "Users")
                        .WithMany("TaskDetails")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("TwnTw_WEB.Models.User", b =>
                {
                    b.Navigation("MemberDetails");

                    b.Navigation("TaskDetails");
                });

            modelBuilder.Entity("TwnTw_WEB.Models.Workspace", b =>
                {
                    b.Navigation("MemberDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
