﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TaskServices.Persistence.Contexts;

#nullable disable

namespace TaskServices.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230902094847_updateUserModel")]
    partial class updateUserModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TaskServices.Domain.Entities.Attachment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<byte[]>("FileData")
                        .HasMaxLength(1000)
                        .HasColumnType("bytea");

                    b.Property<string>("FileName")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("FileType")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int?>("IssueId")
                        .HasColumnType("integer");

                    b.Property<int?>("SubIssueId")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("IssueId");

                    b.HasIndex("SubIssueId");

                    b.ToTable("Attachments", (string)null);
                });

            modelBuilder.Entity("TaskServices.Domain.Entities.Column", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Color")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int?>("SprintId")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("SprintId");

                    b.ToTable("Columns", (string)null);
                });

            modelBuilder.Entity("TaskServices.Domain.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int?>("IssueId")
                        .HasColumnType("integer");

                    b.Property<int?>("NoteId")
                        .HasColumnType("integer");

                    b.Property<int?>("SubIssueId")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("IssueId");

                    b.HasIndex("NoteId");

                    b.HasIndex("SubIssueId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments", (string)null);
                });

            modelBuilder.Entity("TaskServices.Domain.Entities.EventCalendar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTimeOffset?>("EndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Link")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset?>("StartTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Events", (string)null);
                });

            modelBuilder.Entity("TaskServices.Domain.Entities.Issue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<DateTimeOffset?>("DueDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int?>("Order")
                        .HasColumnType("integer");

                    b.Property<int?>("Priority")
                        .HasColumnType("integer");

                    b.Property<int?>("SprintId")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset?>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("StatusId")
                        .HasColumnType("integer");

                    b.Property<int?>("StoryPoint")
                        .HasColumnType("integer");

                    b.Property<int?>("Type")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SprintId");

                    b.HasIndex("StatusId");

                    b.HasIndex("UserId");

                    b.ToTable("Issues", (string)null);
                });

            modelBuilder.Entity("TaskServices.Domain.Entities.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("ColumnId")
                        .HasColumnType("integer");

                    b.Property<string>("Content")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int?>("Order")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ColumnId");

                    b.HasIndex("UserId");

                    b.ToTable("Notes", (string)null);
                });

            modelBuilder.Entity("TaskServices.Domain.Entities.Page", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int?>("ParentPageId")
                        .HasColumnType("integer");

                    b.Property<int?>("SprintId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SprintId");

                    b.HasIndex("UserId");

                    b.ToTable("Pages", (string)null);
                });

            modelBuilder.Entity("TaskServices.Domain.Entities.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsUpgraded")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Projects", (string)null);
                });

            modelBuilder.Entity("TaskServices.Domain.Entities.Sprint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTimeOffset?>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool?>("IsCompleted")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsStart")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int?>("NumOfIssue")
                        .HasColumnType("integer");

                    b.Property<int?>("NumOfStoryPoint")
                        .HasColumnType("integer");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset?>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Sprints", (string)null);
                });

            modelBuilder.Entity("TaskServices.Domain.Entities.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Color")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int?>("SprintId")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("SprintId");

                    b.ToTable("Statuses", (string)null);
                });

            modelBuilder.Entity("TaskServices.Domain.Entities.SubIssue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<DateTimeOffset?>("DueDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int?>("IssueId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int?>("Order")
                        .HasColumnType("integer");

                    b.Property<int?>("Priority")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset?>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("StatusId")
                        .HasColumnType("integer");

                    b.Property<int?>("StoryPoint")
                        .HasColumnType("integer");

                    b.Property<int?>("Type")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("IssueId");

                    b.HasIndex("StatusId");

                    b.HasIndex("UserId");

                    b.ToTable("SubIssues", (string)null);
                });

            modelBuilder.Entity("TaskServices.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TaskServices.Domain.Entities.UserProject", b =>
                {
                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset?>("JoinDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("ProjectId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserProject", (string)null);
                });

            modelBuilder.Entity("TaskServices.Domain.Entities.Attachment", b =>
                {
                    b.HasOne("TaskServices.Domain.Entities.Issue", "Issue")
                        .WithMany("Attachments")
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("TaskServices.Domain.Entities.SubIssue", "SubIssue")
                        .WithMany("Attachments")
                        .HasForeignKey("SubIssueId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Issue");

                    b.Navigation("SubIssue");
                });

            modelBuilder.Entity("TaskServices.Domain.Entities.Column", b =>
                {
                    b.HasOne("TaskServices.Domain.Entities.Sprint", "Sprint")
                        .WithMany("Columns")
                        .HasForeignKey("SprintId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Sprint");
                });

            modelBuilder.Entity("TaskServices.Domain.Entities.Comment", b =>
                {
                    b.HasOne("TaskServices.Domain.Entities.Issue", "Issue")
                        .WithMany("Comments")
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("TaskServices.Domain.Entities.Note", "Note")
                        .WithMany("Comments")
                        .HasForeignKey("NoteId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("TaskServices.Domain.Entities.SubIssue", "SubIssue")
                        .WithMany("Comments")
                        .HasForeignKey("SubIssueId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("TaskServices.Domain.Entities.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Issue");

                    b.Navigation("Note");

                    b.Navigation("SubIssue");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaskServices.Domain.Entities.EventCalendar", b =>
                {
                    b.HasOne("TaskServices.Domain.Entities.Project", "Project")
                        .WithMany("Events")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Project");
                });

            modelBuilder.Entity("TaskServices.Domain.Entities.Issue", b =>
                {
                    b.HasOne("TaskServices.Domain.Entities.Sprint", "Sprint")
                        .WithMany("Issues")
                        .HasForeignKey("SprintId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TaskServices.Domain.Entities.Status", "Status")
                        .WithMany("Issues")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("TaskServices.Domain.Entities.User", "User")
                        .WithMany("Issues")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Sprint");

                    b.Navigation("Status");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaskServices.Domain.Entities.Note", b =>
                {
                    b.HasOne("TaskServices.Domain.Entities.Column", "Column")
                        .WithMany("Notes")
                        .HasForeignKey("ColumnId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TaskServices.Domain.Entities.User", "User")
                        .WithMany("Notes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Column");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaskServices.Domain.Entities.Page", b =>
                {
                    b.HasOne("TaskServices.Domain.Entities.Sprint", "Sprint")
                        .WithMany("Pages")
                        .HasForeignKey("SprintId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TaskServices.Domain.Entities.User", "User")
                        .WithMany("Pages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Sprint");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaskServices.Domain.Entities.Sprint", b =>
                {
                    b.HasOne("TaskServices.Domain.Entities.Project", "Project")
                        .WithMany("Sprints")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Project");
                });

            modelBuilder.Entity("TaskServices.Domain.Entities.Status", b =>
                {
                    b.HasOne("TaskServices.Domain.Entities.Sprint", "Sprint")
                        .WithMany("Statuses")
                        .HasForeignKey("SprintId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Sprint");
                });

            modelBuilder.Entity("TaskServices.Domain.Entities.SubIssue", b =>
                {
                    b.HasOne("TaskServices.Domain.Entities.Issue", "Issue")
                        .WithMany("SubIssues")
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TaskServices.Domain.Entities.Status", "Status")
                        .WithMany("SubIssues")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("TaskServices.Domain.Entities.User", "User")
                        .WithMany("SubIssues")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Issue");

                    b.Navigation("Status");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaskServices.Domain.Entities.UserProject", b =>
                {
                    b.HasOne("TaskServices.Domain.Entities.Project", "Project")
                        .WithMany("UserProjects")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskServices.Domain.Entities.User", "User")
                        .WithMany("UserProjects")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaskServices.Domain.Entities.Column", b =>
                {
                    b.Navigation("Notes");
                });

            modelBuilder.Entity("TaskServices.Domain.Entities.Issue", b =>
                {
                    b.Navigation("Attachments");

                    b.Navigation("Comments");

                    b.Navigation("SubIssues");
                });

            modelBuilder.Entity("TaskServices.Domain.Entities.Note", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("TaskServices.Domain.Entities.Project", b =>
                {
                    b.Navigation("Events");

                    b.Navigation("Sprints");

                    b.Navigation("UserProjects");
                });

            modelBuilder.Entity("TaskServices.Domain.Entities.Sprint", b =>
                {
                    b.Navigation("Columns");

                    b.Navigation("Issues");

                    b.Navigation("Pages");

                    b.Navigation("Statuses");
                });

            modelBuilder.Entity("TaskServices.Domain.Entities.Status", b =>
                {
                    b.Navigation("Issues");

                    b.Navigation("SubIssues");
                });

            modelBuilder.Entity("TaskServices.Domain.Entities.SubIssue", b =>
                {
                    b.Navigation("Attachments");

                    b.Navigation("Comments");
                });

            modelBuilder.Entity("TaskServices.Domain.Entities.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Issues");

                    b.Navigation("Notes");

                    b.Navigation("Pages");

                    b.Navigation("SubIssues");

                    b.Navigation("UserProjects");
                });
#pragma warning restore 612, 618
        }
    }
}
