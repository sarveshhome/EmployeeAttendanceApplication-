using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Employee configuration
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.EmployeeId).IsUnique();
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Department).HasMaxLength(100);
            });

            // Attendance configuration
            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => new { e.EmployeeId, e.Date }).IsUnique();
                
                entity.Property(e => e.CheckInTime).IsRequired();
                entity.Property(e => e.Date).IsRequired();
                
                entity.HasOne(a => a.Employee)
                    .WithMany(e => e.Attendances)
                    .HasForeignKey(a => a.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}