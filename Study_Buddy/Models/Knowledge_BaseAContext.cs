using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Study_Buddy.Models
{
    public partial class Knowledge_BaseAContext : DbContext
    {
        public Knowledge_BaseAContext()
        {
        }

        public Knowledge_BaseAContext(DbContextOptions<Knowledge_BaseAContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<QuestionType> QuestionType { get; set; }
        public virtual DbSet<Quiz> Quiz { get; set; }
        public virtual DbSet<Subtopic> Subtopic { get; set; }
        public virtual DbSet<Topic> Topic { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=study-buddy.database.windows.net;Initial Catalog=Knowledge_BaseA;Persist Security Info=True;User ID=SBAdmin;Password=asAS1!qwe");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.QuestionId).HasColumnName("questionID");

                entity.Property(e => e.Answer)
                    .IsRequired()
                    .HasColumnName("answer");

                entity.Property(e => e.Body)
                    .IsRequired()
                    .HasColumnName("body");

                entity.Property(e => e.QuestiontypeId).HasColumnName("questiontypeID");

                entity.Property(e => e.SubTopicId).HasColumnName("subTopicID");

                entity.HasOne(d => d.Questiontype)
                    .WithMany(p => p.Question)
                    .HasForeignKey(d => d.QuestiontypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Question__questi__76969D2E");

                entity.HasOne(d => d.SubTopic)
                    .WithMany(p => p.Question)
                    .HasForeignKey(d => d.SubTopicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Question__subTop__778AC167");
            });

            modelBuilder.Entity<QuestionType>(entity =>
            {
                entity.Property(e => e.QuestiontypeId).HasColumnName("questiontypeID");

                entity.Property(e => e.TypeDesc).HasColumnName("typeDesc");
            });

            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.Property(e => e.QuizId).HasColumnName("quizID");

                entity.Property(e => e.DateCreated)
                    .HasColumnName("dateCreated")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NumberOfQuestions).HasColumnName("numberOfQuestions");

                entity.Property(e => e.Score)
                    .HasColumnName("score")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TopicId).HasColumnName("topicID");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.Quiz)
                    .HasForeignKey(d => d.TopicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Quiz__topicID__7B5B524B");
            });

            modelBuilder.Entity<Subtopic>(entity =>
            {
                entity.Property(e => e.SubtopicId).HasColumnName("subtopicID");

                entity.Property(e => e.SubtopicDesc).HasColumnName("subtopicDesc");

                entity.Property(e => e.TopicId).HasColumnName("topicID");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.Subtopic)
                    .HasForeignKey(d => d.TopicId)
                    .HasConstraintName("FK__Subtopic__topicI__71D1E811");
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.Property(e => e.TopicId).HasColumnName("topicID");

                entity.Property(e => e.TopicDesc).HasColumnName("topicDesc");
            });
        }
    }
}
