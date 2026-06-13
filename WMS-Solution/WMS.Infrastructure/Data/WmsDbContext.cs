using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WMS.Domain.Entities;

namespace WMS.Infrastructure.Data
{
    public class WmsDbContext : DbContext
    {
        public WmsDbContext(DbContextOptions<WmsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Leave>()
                .HasOne(l => l.Employee)
                .WithMany(e => e.Leaves)
                .HasForeignKey(l => l.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Leave>()
                .HasOne(l => l.ApprovedByEmployee)
                .WithMany()
                .HasForeignKey(l => l.ApprovedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserLogin>()
                .HasOne(ul => ul.Employee)
                .WithOne(e => e.UserLogin)
                .HasForeignKey<UserLogin>(ul => ul.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserLogin>()
                .HasOne(ul => ul.Role)
                .WithMany()
                .HasForeignKey(ul => ul.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Attendance> Attendances { get; set; }

        public DbSet<Leave> Leaves { get; set; }

        public DbSet<Announcement> Announcements { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<EmployeeProjectAllocation> EmployeeProjectAllocations { get; set; }

        public DbSet<UserLogin> UserLogins { get; set; }

        public DbSet<AuditLog> AuditLogs { get; set; }
    }
}
