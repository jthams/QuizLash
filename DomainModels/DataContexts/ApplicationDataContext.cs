using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Domain.DataContexts
{
    public class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options)
        {
        }

        public DbSet<Choice> Choices { get; set; }
        public DbSet<Quiz> Quizs { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuizQuestionRelation> QuizQuestionRelation { get; set; }
        public DbSet<Topic> Topics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<QuizQuestionRelation>().HasKey(qqr => new { qqr.QuizID, qqr.QuestionID });

            modelBuilder.Entity<QuizQuestionRelation>()
                .HasOne(qqr => qqr.Question)
                .WithMany(q => q.QuizQuestionRelation)
                .HasForeignKey(qqr => qqr.QuestionID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuizQuestionRelation>()
                .HasOne(qqr => qqr.Quiz)
                .WithMany(q => q.QuizQuestionRelation)
                .HasForeignKey(qqr => qqr.QuizID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Choice>().ToTable("Choices");
            modelBuilder.Entity<Topic>().ToTable("Topics");
            modelBuilder.Entity<Quiz>().ToTable("Quizs");
            modelBuilder.Entity<Question>().ToTable("Questions");
            modelBuilder.Entity<QuizQuestionRelation>().ToTable("QuizQuestionRelation");
            modelBuilder.Entity<Topic>().HasIndex(t => t.Description).IsUnique();

        }

    }  

}

