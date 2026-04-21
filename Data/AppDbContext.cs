using EInsurance.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EInsurance.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Customer> Customer { get; set; }

        public DbSet<InsuranceAgent> InsuranceAgents { get; set; }

        public DbSet<InsurancePlan> InsurancePlans { get; set; }

        public DbSet<Scheme> Schemes { get; set; }

        public DbSet<Policy> Policy { get; set; }

        public DbSet<Payment> Payment { get; set; }

        public DbSet<Commission> Commissions { get; set; }

        public DbSet<EmployeeScheme> EmployeeSchemes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Admin>()
                .HasIndex(a => a.Email).IsUnique();
            modelBuilder.Entity<Admin>()
                .HasIndex(a => a.Username).IsUnique();

            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.Email).IsUnique();
            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.Username).IsUnique();

            modelBuilder.Entity<InsuranceAgent>()
                .HasIndex(a => a.Email).IsUnique();
            modelBuilder.Entity<InsuranceAgent>()
                .HasIndex(a => a.Username).IsUnique();

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Email).IsUnique();

            modelBuilder.Entity<Policy>()
                .Property(p => p.Preminum)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(10,2");

            modelBuilder.Entity<Commission>()
                .Property(c => c.CommissionAmount)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<Commission>()
                .HasOne(c => c.Policy)
                .WithMany(p => p.Commissions)
                .HasForeignKey(c => c.PolicyID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Commission>()
                .HasOne(c => c.Agent)
                .WithMany(a => a.Commissions)
                .HasForeignKey(c => c.AgentID)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Policy)
                .WithMany(p => p.Payments)
                .HasForeignKey(p => p.PolicyID)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Customer)
                .WithMany(c => c.Payments)
                .HasForeignKey(p => p.CustomerID)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Policy>()
                .HasOne(p => p.Customer)
                .WithMany(c => c.Policies)
                .HasForeignKey(p => p.CustomerID)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Policy>()
                .HasOne(p => p.Scheme)
                .WithMany(s => s.Policies)
                .HasForeignKey(p => p.SchemeID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Admin>().HasData(new Admin
            {
                AdminID = 1,
                Username = "admin",
                Password = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                Email = "admin@einsurance.com",
                FullName = "Super Admin",
                CreatedAt = DateTime.UtcNow
            });
        }

    }
}
