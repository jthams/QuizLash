﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Study_Buddy.Data;

namespace Study_Buddy.Migrations.ApplicationData
{
    [DbContext(typeof(ApplicationDataContext))]
    [Migration("20190401204005_CreateSchema")]
    partial class CreateSchema
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Study_Buddy.Models.Choices", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("QuestionRefID");

                    b.Property<string>("Text");

                    b.HasKey("ID");

                    b.HasIndex("QuestionRefID");

                    b.ToTable("Choices");
                });

            modelBuilder.Entity("Study_Buddy.Models.Question", b =>
                {
                    b.Property<int>("QuestionID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Answer");

                    b.Property<string>("Body");

                    b.Property<int>("QuizRefID");

                    b.Property<int>("Type");

                    b.HasKey("QuestionID");

                    b.HasIndex("QuizRefID");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("Study_Buddy.Models.Quiz", b =>
                {
                    b.Property<int>("QuizID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsComplete");

                    b.Property<string>("Owner");

                    b.Property<decimal?>("Score");

                    b.Property<string>("Topic");

                    b.HasKey("QuizID");

                    b.ToTable("Quiz");
                });

            modelBuilder.Entity("Study_Buddy.Models.Choices", b =>
                {
                    b.HasOne("Study_Buddy.Models.Question", "Question")
                        .WithMany("Choices")
                        .HasForeignKey("QuestionRefID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Study_Buddy.Models.Question", b =>
                {
                    b.HasOne("Study_Buddy.Models.Quiz", "Quiz")
                        .WithMany("Questions")
                        .HasForeignKey("QuizRefID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}