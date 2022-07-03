﻿// <auto-generated />
using System;
using Intranet.Entities.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Intranet.Entities.Migrations
{
    [DbContext(typeof(IntranetContext))]
    [Migration("20220619063948_Initial-Migration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-preview.5.22302.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Intranet.Entities.Entities.Attendance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Attend")
                        .HasColumnType("datetime2");

                    b.Property<int>("AttendanceInfoId")
                        .HasColumnType("int");

                    b.Property<decimal>("ContributeAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Leave")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MeetingScheduleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AttendanceInfoId");

                    b.HasIndex("MeetingScheduleId");

                    b.ToTable("Attendances");
                });

            modelBuilder.Entity("Intranet.Entities.Entities.ChatMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ConversationId")
                        .HasColumnType("int");

                    b.Property<string>("MessageContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SentTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ConversationId");

                    b.HasIndex("UserId");

                    b.ToTable("ChatMessages");
                });

            modelBuilder.Entity("Intranet.Entities.Entities.Contribution", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ContributorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DonateOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<int>("PaymentType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContributorId");

                    b.ToTable("Contributions");
                });

            modelBuilder.Entity("Intranet.Entities.Entities.Conversation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastInteractionTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastMessageContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Conversations");
                });

            modelBuilder.Entity("Intranet.Entities.Entities.Food", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FoodEnglishName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FoodName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsUnavailable")
                        .HasColumnType("bit");

                    b.Property<int>("MainIcon")
                        .HasColumnType("int");

                    b.Property<decimal>("Percentage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("SecondaryIcon")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Foods");
                });

            modelBuilder.Entity("Intranet.Entities.Entities.MeetingInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ImportanceLevel")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MeetingInfos");
                });

            modelBuilder.Entity("Intranet.Entities.Entities.MeetingSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("MeetingDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MeetingInfoId")
                        .HasColumnType("int");

                    b.Property<int>("PlannerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MeetingInfoId");

                    b.HasIndex("PlannerId");

                    b.ToTable("MeetingSchedules");
                });

            modelBuilder.Entity("Intranet.Entities.Entities.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("About")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AppStoreLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Clients")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Deadline")
                        .HasColumnType("datetime2");

                    b.Property<string>("FigmaLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GithubLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GooglePlayLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MicrosoftStoreLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProjectBackground")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProjectLogo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("TechLead")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Intranet.Entities.Entities.TodoTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageTaskUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MeetingScheduleId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("MeetingScheduleId");

                    b.ToTable("TodoTasks");
                });

            modelBuilder.Entity("Intranet.Entities.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardPic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Former")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Hobby")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDisable")
                        .HasColumnType("bit");

                    b.Property<string>("ProfilePic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Relationship")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SignalRConnectionId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Skills")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SpecialAward")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Intranet.Entities.Entities.UserConversation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ConversationId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsMute")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ConversationId");

                    b.HasIndex("UserId");

                    b.ToTable("UserConversations", (string)null);
                });

            modelBuilder.Entity("Intranet.Entities.Entities.UserFood", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("FoodId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FoodId");

                    b.HasIndex("UserId");

                    b.ToTable("UserFoods", (string)null);
                });

            modelBuilder.Entity("Intranet.Entities.Entities.UserProject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("UserProjects", (string)null);
                });

            modelBuilder.Entity("Intranet.Entities.Entities.Attendance", b =>
                {
                    b.HasOne("Intranet.Entities.Entities.User", "AttendanceInfo")
                        .WithMany()
                        .HasForeignKey("AttendanceInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Intranet.Entities.Entities.MeetingSchedule", null)
                        .WithMany("Attendances")
                        .HasForeignKey("MeetingScheduleId");

                    b.Navigation("AttendanceInfo");
                });

            modelBuilder.Entity("Intranet.Entities.Entities.ChatMessage", b =>
                {
                    b.HasOne("Intranet.Entities.Entities.Conversation", "Conversation")
                        .WithMany("ChatMessages")
                        .HasForeignKey("ConversationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Intranet.Entities.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Conversation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Intranet.Entities.Entities.Contribution", b =>
                {
                    b.HasOne("Intranet.Entities.Entities.User", "Contributor")
                        .WithMany()
                        .HasForeignKey("ContributorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contributor");
                });

            modelBuilder.Entity("Intranet.Entities.Entities.MeetingSchedule", b =>
                {
                    b.HasOne("Intranet.Entities.Entities.MeetingInfo", "MeetingInfo")
                        .WithMany()
                        .HasForeignKey("MeetingInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Intranet.Entities.Entities.User", "Planner")
                        .WithMany()
                        .HasForeignKey("PlannerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MeetingInfo");

                    b.Navigation("Planner");
                });

            modelBuilder.Entity("Intranet.Entities.Entities.TodoTask", b =>
                {
                    b.HasOne("Intranet.Entities.Entities.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Intranet.Entities.Entities.MeetingSchedule", null)
                        .WithMany("MustDoneTask")
                        .HasForeignKey("MeetingScheduleId");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Intranet.Entities.Entities.UserConversation", b =>
                {
                    b.HasOne("Intranet.Entities.Entities.Conversation", "Conversation")
                        .WithMany()
                        .HasForeignKey("ConversationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Intranet.Entities.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Conversation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Intranet.Entities.Entities.UserFood", b =>
                {
                    b.HasOne("Intranet.Entities.Entities.Food", "Food")
                        .WithMany()
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Intranet.Entities.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Food");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Intranet.Entities.Entities.UserProject", b =>
                {
                    b.HasOne("Intranet.Entities.Entities.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Intranet.Entities.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Intranet.Entities.Entities.Conversation", b =>
                {
                    b.Navigation("ChatMessages");
                });

            modelBuilder.Entity("Intranet.Entities.Entities.MeetingSchedule", b =>
                {
                    b.Navigation("Attendances");

                    b.Navigation("MustDoneTask");
                });
#pragma warning restore 612, 618
        }
    }
}