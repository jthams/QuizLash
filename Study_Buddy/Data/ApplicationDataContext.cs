using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Study_Buddy.Models;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Study_Buddy.Data
{
    public partial class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext()
        {
        }

        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answers> Answers { get; set; }
        public virtual DbSet<Choices> Choices { get; set; }
        public virtual DbSet<Questions> Questions { get; set; }
        public virtual DbSet<Quiz> Quiz { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=study-buddy.database.windows.net;Initial Catalog=ApplicationData;Persist Security Info=True;User ID=SBAdmin;Password=asAS1!qwe");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity<Answers>(entity =>
            {
                entity.HasKey(e => e.AnswerId);

                entity.Property(e => e.AnswerId).HasColumnName("AnswerID");

                entity.Property(e => e.AnswerText).IsUnicode(false);

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK_Answers_Questions");
            });

            modelBuilder.Entity<Choices>(entity =>
            {
                entity.HasKey(e => e.ChoiceId);

                entity.Property(e => e.ChoiceId).HasColumnName("ChoiceID");

                entity.Property(e => e.ChoiceText).IsUnicode(false);

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Choices)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK_Choices_Questions");
            });

            modelBuilder.Entity<Questions>(entity =>
            {
                entity.HasKey(e => e.QuestionId);

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.QuestionText).IsUnicode(false);

                entity.Property(e => e.QuizId).HasColumnName("QuizID");

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.QuizId)
                    .HasConstraintName("FK_Questions_Quiz");
            });

            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.Property(e => e.QuizId).HasColumnName("QuizID");

                entity.Property(e => e.QuizName)
                    .HasMaxLength(80)
                    .IsUnicode(false);
            });
        }
    }
}
