using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace JobApplication.Models;

public partial class JobapplicationdbContext : DbContext
{
    public JobapplicationdbContext()
    {
    }

    public JobapplicationdbContext(DbContextOptions<JobapplicationdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<Jobapplication> Jobapplications { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("name=JobApplicationDB", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.40-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("companies");

            entity.HasIndex(e => e.City, "idx_companies_city");

            entity.HasIndex(e => e.Industry, "idx_companies_industry");

            entity.HasIndex(e => e.Name, "idx_companies_name");

            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.ContactEmail).HasMaxLength(255);
            entity.Property(e => e.ContactPhone).HasMaxLength(50);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Industry).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValueSql("'1'");
            entity.Property(e => e.LogoPath).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.State).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.Website).HasMaxLength(255);
            entity.Property(e => e.ZipCode).HasMaxLength(20);
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("jobs");

            entity.HasIndex(e => e.IsActive, "idx_jobs_active");

            entity.HasIndex(e => e.CompanyId, "idx_jobs_company");

            entity.HasIndex(e => e.ApplicationDeadline, "idx_jobs_deadline");

            entity.HasIndex(e => e.Experience, "idx_jobs_experience");

            entity.HasIndex(e => e.JobType, "idx_jobs_jobtype");

            entity.HasIndex(e => e.Location, "idx_jobs_location");

            entity.HasIndex(e => e.Title, "idx_jobs_title");

            entity.Property(e => e.ApplicationDeadline).HasColumnType("datetime");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Experience).HasMaxLength(50);
            entity.Property(e => e.IsActive).HasDefaultValueSql("'1'");
            entity.Property(e => e.JobType).HasMaxLength(50);
            entity.Property(e => e.Location).HasMaxLength(200);
            entity.Property(e => e.MaxSalary).HasPrecision(10, 2);
            entity.Property(e => e.MinSalary).HasPrecision(10, 2);
            entity.Property(e => e.Requirements).HasColumnType("text");
            entity.Property(e => e.Skills).HasColumnType("text");
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Company).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("jobs_ibfk_1");
        });

        modelBuilder.Entity<Jobapplication>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("jobapplications");

            entity.HasIndex(e => e.AppliedAt, "idx_applications_applied_date");

            entity.HasIndex(e => e.JobId, "idx_applications_job");

            entity.HasIndex(e => e.Status, "idx_applications_status");

            entity.HasIndex(e => e.UserId, "idx_applications_user");

            entity.HasIndex(e => new { e.JobId, e.UserId }, "unique_user_job").IsUnique();

            entity.Property(e => e.AdditionalDocuments).HasColumnType("json");
            entity.Property(e => e.AppliedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.CoverLetter).HasColumnType("text");
            entity.Property(e => e.CustomResumeFilePath).HasMaxLength(255);
            entity.Property(e => e.InterviewDate).HasColumnType("datetime");
            entity.Property(e => e.InterviewFeedback).HasColumnType("text");
            entity.Property(e => e.Notes).HasColumnType("text");
            entity.Property(e => e.Status).HasDefaultValueSql("'1'");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Job).WithMany(p => p.Jobapplications)
                .HasForeignKey(d => d.JobId)
                .HasConstraintName("jobapplications_ibfk_1");

            entity.HasOne(d => d.User).WithMany(p => p.Jobapplications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("jobapplications_ibfk_2");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "Email").IsUnique();

            entity.HasIndex(e => new { e.FirstName, e.LastName }, "idx_users_name");

            entity.HasIndex(e => e.Skills, "idx_users_skills").HasAnnotation("MySql:IndexPrefixLength", new[] { 100 });

            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Education).HasColumnType("text");
            entity.Property(e => e.Experience).HasColumnType("text");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.GitHubProfile).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValueSql("'1'");
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.LinkedInProfile).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.Portfolio).HasMaxLength(255);
            entity.Property(e => e.ProfilePicturePath).HasMaxLength(255);
            entity.Property(e => e.ResumeFilePath).HasMaxLength(255);
            entity.Property(e => e.Skills).HasColumnType("text");
            entity.Property(e => e.State).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.ZipCode).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
