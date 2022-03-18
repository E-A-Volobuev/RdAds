﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RnAds.DAL;

namespace RnAds.DAL.Migrations
{
    [DbContext(typeof(RnAdsContext))]
    [Migration("20220314100158_EditMessageTable")]
    partial class EditMessageTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RnAds.Model.Model.Ad", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameAd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("TypeAd")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Ads");
                });

            modelBuilder.Entity("RnAds.Model.Model.AvatarUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId1")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId1");

                    b.ToTable("Avatars");
                });

            modelBuilder.Entity("RnAds.Model.Model.Dialog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CurrentUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ToUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Dialogs");
                });

            modelBuilder.Entity("RnAds.Model.Model.FavoriteUserId", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AdId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AdId");

                    b.ToTable("FavoriteUserIdes");
                });

            modelBuilder.Entity("RnAds.Model.Model.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DialogId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ReceiverUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SenderUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Time")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DialogId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("RnAds.Model.Model.Picture", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AdId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("MessageId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AdId");

                    b.HasIndex("MessageId");

                    b.ToTable("Pictures");
                });

            modelBuilder.Entity("RnAds.Model.Model.ProductAttribute", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AdId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Area")
                        .HasColumnType("int");

                    b.Property<string>("Availability")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryItem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Citizenship")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Condition")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CountRooms")
                        .HasColumnType("int");

                    b.Property<double>("EngineVolume")
                        .HasColumnType("float");

                    b.Property<string>("FrequencyOfPayments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HorsePower")
                        .HasColumnType("int");

                    b.Property<int>("Kilometers")
                        .HasColumnType("int");

                    b.Property<int>("NumberFloor")
                        .HasColumnType("int");

                    b.Property<int>("OwnerCount")
                        .HasColumnType("int");

                    b.Property<string>("Sex")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SphereOfActivity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeBody")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeDrive")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeEngine")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeGearBox")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeHelm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeItem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeSparePartsAndAcsessories")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkExperience")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkingSchedule")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("YearReliase")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AdId")
                        .IsUnique();

                    b.ToTable("ProductAttributes");
                });

            modelBuilder.Entity("RnAds.Model.Model.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RnAds.Model.Model.Ad", b =>
                {
                    b.HasOne("RnAds.Model.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RnAds.Model.Model.AvatarUser", b =>
                {
                    b.HasOne("RnAds.Model.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("RnAds.Model.Model.FavoriteUserId", b =>
                {
                    b.HasOne("RnAds.Model.Model.Ad", "Ad")
                        .WithMany("ListUserId")
                        .HasForeignKey("AdId");
                });

            modelBuilder.Entity("RnAds.Model.Model.Message", b =>
                {
                    b.HasOne("RnAds.Model.Model.Dialog", "Dialog")
                        .WithMany("Messages")
                        .HasForeignKey("DialogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RnAds.Model.Model.Picture", b =>
                {
                    b.HasOne("RnAds.Model.Model.Ad", "Ad")
                        .WithMany("Pictures")
                        .HasForeignKey("AdId");

                    b.HasOne("RnAds.Model.Model.Message", "Message")
                        .WithMany("Pictures")
                        .HasForeignKey("MessageId");
                });

            modelBuilder.Entity("RnAds.Model.Model.ProductAttribute", b =>
                {
                    b.HasOne("RnAds.Model.Model.Ad", "Ad")
                        .WithOne("ProductAttribute")
                        .HasForeignKey("RnAds.Model.Model.ProductAttribute", "AdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}