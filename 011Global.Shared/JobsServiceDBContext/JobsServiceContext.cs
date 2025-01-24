using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using _011Global.Shared.JobsServiceDBContext.Entities;

namespace _011Global.Shared.JobsServiceDBContext;

public partial class JobsServiceContext : DbContext
{
    public JobsServiceContext(DbContextOptions<JobsServiceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<GlobalJob> GlobalJobs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GlobalJob>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Global_J__3214EC27AF7B5666");

            entity.ToTable("Global_Jobs");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Assembly)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.LastStartDate).HasColumnType("datetime");
            entity.Property(e => e.LastStopDate).HasColumnType("datetime");
            entity.Property(e => e.MachineNameList)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.TypeName)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
