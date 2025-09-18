using System;
using System.Collections.Generic;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public partial class TodoApp2Context : DbContext
{
    public TodoApp2Context()
    {
    }

    public TodoApp2Context(DbContextOptions<TodoApp2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=TIEN10DIEM\\SQLEXPRESS;Database=TodoApp2;User ID=sa;Password=sa;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.JobId).HasName("PK__Jobs__6E21E30DC9D65861");

            entity.Property(e => e.JobId).HasColumnName("job_ID");
            entity.Property(e => e.JobCreateAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("job_Create_At");
            entity.Property(e => e.JobDateEnd)
                .HasColumnType("datetime")
                .HasColumnName("job_Date_End");
            entity.Property(e => e.JobDateStart)
                .HasColumnType("datetime")
                .HasColumnName("job_Date_Start");
            entity.Property(e => e.JobDeleteAt)
                .HasColumnType("datetime")
                .HasColumnName("job_Delete_At");
            entity.Property(e => e.JobFlag).HasColumnName("job_Flag");
            entity.Property(e => e.JobMembers)
                .HasDefaultValue(1)
                .HasColumnName("job_Members");
            entity.Property(e => e.JobName)
                .HasMaxLength(100)
                .HasColumnName("job_name");
            entity.Property(e => e.JobRemainingTime)
                .HasColumnType("datetime")
                .HasColumnName("job_Remaining_Time");
            entity.Property(e => e.JobStatus)
                .HasMaxLength(50)
                .HasColumnName("job_Status");
            entity.Property(e => e.JobUpdateAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("job_Update_At");
            entity.Property(e => e.UserId).HasColumnName("user_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Jobs__user_ID__3E52440B");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BF3307ACBB4A25");

            entity.Property(e => e.UserId).HasColumnName("user_ID");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(100)
                .HasColumnName("user_Email");
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .HasColumnName("user_Name");
            entity.Property(e => e.UserPasswordHash)
                .HasMaxLength(255)
                .HasColumnName("user_PasswordHash");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
