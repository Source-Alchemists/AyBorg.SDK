﻿// <auto-generated />
using System;
using AyBorg.Database.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AyBorg.Database.Migrations.SqlLite.Migrations.Project
{
    [DbContext(typeof(ProjectContext))]
    [Migration("20221124202754_ProjectSettings")]
    partial class ProjectSettings
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("AyBorg.SDK.Data.DAL.LinkRecord", b =>
                {
                    b.Property<Guid>("DbId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ProjectRecordId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SourceId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TargetId")
                        .HasColumnType("TEXT");

                    b.HasKey("DbId");

                    b.HasIndex("ProjectRecordId");

                    b.ToTable("AyBorgLinks");
                });

            modelBuilder.Entity("AyBorg.SDK.Data.DAL.PluginMetaInfoRecord", b =>
                {
                    b.Property<Guid>("DbId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("AssemblyName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("AssemblyVersion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("DbId");

                    b.ToTable("PluginMetaInfoRecord");
                });

            modelBuilder.Entity("AyBorg.SDK.Data.DAL.PortRecord", b =>
                {
                    b.Property<Guid>("DbId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Brand")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Direction")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("StepRecordId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("DbId");

                    b.HasIndex("StepRecordId");

                    b.ToTable("AyBorgPorts");
                });

            modelBuilder.Entity("AyBorg.SDK.Data.DAL.ProjectMetaRecord", b =>
                {
                    b.Property<Guid>("DbId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("ApprovedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ProjectRecordId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ServiceUniqueName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("State")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.Property<long>("VersionIteration")
                        .HasColumnType("INTEGER");

                    b.Property<string>("VersionName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("DbId");

                    b.HasIndex("ProjectRecordId")
                        .IsUnique();

                    b.ToTable("AyBorgProjectMetas");
                });

            modelBuilder.Entity("AyBorg.SDK.Data.DAL.ProjectRecord", b =>
                {
                    b.Property<Guid>("DbId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SettingsDbId")
                        .HasColumnType("TEXT");

                    b.HasKey("DbId");

                    b.HasIndex("SettingsDbId");

                    b.ToTable("AyBorgProjects");
                });

            modelBuilder.Entity("AyBorg.SDK.Data.DAL.ProjectSettingsRecord", b =>
                {
                    b.Property<Guid>("DbId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsForceResultCommunicationEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsForceWebUiCommunicationEnabled")
                        .HasColumnType("INTEGER");

                    b.HasKey("DbId");

                    b.ToTable("ProjectSettingsRecord");
                });

            modelBuilder.Entity("AyBorg.SDK.Data.DAL.StepRecord", b =>
                {
                    b.Property<Guid>("DbId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("MetaInfoDbId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ProjectRecordId")
                        .HasColumnType("TEXT");

                    b.Property<int>("X")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Y")
                        .HasColumnType("INTEGER");

                    b.HasKey("DbId");

                    b.HasIndex("MetaInfoDbId");

                    b.HasIndex("ProjectRecordId");

                    b.ToTable("AyBorgSteps");
                });

            modelBuilder.Entity("AyBorg.SDK.Data.DAL.LinkRecord", b =>
                {
                    b.HasOne("AyBorg.SDK.Data.DAL.ProjectRecord", "ProjectRecord")
                        .WithMany("Links")
                        .HasForeignKey("ProjectRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProjectRecord");
                });

            modelBuilder.Entity("AyBorg.SDK.Data.DAL.PortRecord", b =>
                {
                    b.HasOne("AyBorg.SDK.Data.DAL.StepRecord", "StepRecord")
                        .WithMany("Ports")
                        .HasForeignKey("StepRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StepRecord");
                });

            modelBuilder.Entity("AyBorg.SDK.Data.DAL.ProjectMetaRecord", b =>
                {
                    b.HasOne("AyBorg.SDK.Data.DAL.ProjectRecord", null)
                        .WithOne("Meta")
                        .HasForeignKey("AyBorg.SDK.Data.DAL.ProjectMetaRecord", "ProjectRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AyBorg.SDK.Data.DAL.ProjectRecord", b =>
                {
                    b.HasOne("AyBorg.SDK.Data.DAL.ProjectSettingsRecord", "Settings")
                        .WithMany()
                        .HasForeignKey("SettingsDbId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Settings");
                });

            modelBuilder.Entity("AyBorg.SDK.Data.DAL.StepRecord", b =>
                {
                    b.HasOne("AyBorg.SDK.Data.DAL.PluginMetaInfoRecord", "MetaInfo")
                        .WithMany()
                        .HasForeignKey("MetaInfoDbId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AyBorg.SDK.Data.DAL.ProjectRecord", "ProjectRecord")
                        .WithMany("Steps")
                        .HasForeignKey("ProjectRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MetaInfo");

                    b.Navigation("ProjectRecord");
                });

            modelBuilder.Entity("AyBorg.SDK.Data.DAL.ProjectRecord", b =>
                {
                    b.Navigation("Links");

                    b.Navigation("Meta")
                        .IsRequired();

                    b.Navigation("Steps");
                });

            modelBuilder.Entity("AyBorg.SDK.Data.DAL.StepRecord", b =>
                {
                    b.Navigation("Ports");
                });
#pragma warning restore 612, 618
        }
    }
}
