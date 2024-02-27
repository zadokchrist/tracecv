﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TraceCV.Data;

#nullable disable

namespace TraceCV.Migrations
{
    [DbContext(typeof(DatabaseHandler))]
    [Migration("20240222135738_additionofaffiliation")]
    partial class additionofaffiliation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TraceCV.Models.Affiliation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ExpertId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ExpertId");

                    b.ToTable("Affiliations");
                });

            modelBuilder.Entity("TraceCV.Models.Certificate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ExpertId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ExpertId");

                    b.ToTable("Certificates");
                });

            modelBuilder.Entity("TraceCV.Models.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ExpertId")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ExpertId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("TraceCV.Models.Education", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ExpertId")
                        .HasColumnType("int");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ExpertId");

                    b.ToTable("Educations");
                });

            modelBuilder.Entity("TraceCV.Models.Expert", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CurrentEmployer")
                        .HasColumnType("longtext");

                    b.Property<string>("CvFilePath")
                        .HasColumnType("longtext");

                    b.Property<string>("DOB")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("EmploymentStatus")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Sector")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Speciality")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("WorkedWith2ML")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("experience")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("lastedit")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Experts");
                });

            modelBuilder.Entity("TraceCV.Models.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ExpertId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ExpertId");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("TraceCV.Models.OtherKeyExpertise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ExpertId")
                        .HasColumnType("int");

                    b.Property<string>("Expertise")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ExpertId");

                    b.ToTable("OtherKeyExpertises");
                });

            modelBuilder.Entity("TraceCV.Models.Affiliation", b =>
                {
                    b.HasOne("TraceCV.Models.Expert", null)
                        .WithMany("Affiliations")
                        .HasForeignKey("ExpertId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TraceCV.Models.Certificate", b =>
                {
                    b.HasOne("TraceCV.Models.Expert", null)
                        .WithMany("Certificates")
                        .HasForeignKey("ExpertId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TraceCV.Models.Contact", b =>
                {
                    b.HasOne("TraceCV.Models.Expert", null)
                        .WithMany("Contacts")
                        .HasForeignKey("ExpertId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TraceCV.Models.Education", b =>
                {
                    b.HasOne("TraceCV.Models.Expert", null)
                        .WithMany("Educations")
                        .HasForeignKey("ExpertId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TraceCV.Models.Language", b =>
                {
                    b.HasOne("TraceCV.Models.Expert", null)
                        .WithMany("Languages")
                        .HasForeignKey("ExpertId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TraceCV.Models.OtherKeyExpertise", b =>
                {
                    b.HasOne("TraceCV.Models.Expert", null)
                        .WithMany("OtherKeyExpertises")
                        .HasForeignKey("ExpertId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TraceCV.Models.Expert", b =>
                {
                    b.Navigation("Affiliations");

                    b.Navigation("Certificates");

                    b.Navigation("Contacts");

                    b.Navigation("Educations");

                    b.Navigation("Languages");

                    b.Navigation("OtherKeyExpertises");
                });
#pragma warning restore 612, 618
        }
    }
}
