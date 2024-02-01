using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TraceCV.Models;

public partial class TraceCvDbContext : DbContext
{
    public TraceCvDbContext()
    {
    }

    public TraceCvDbContext(DbContextOptions<TraceCvDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=localhost;database=trace_cv_db;user=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.32-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
