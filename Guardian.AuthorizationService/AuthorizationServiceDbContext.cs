using Microsoft.EntityFrameworkCore;
using Guardian.AuthorizationService.Models;
using System;

namespace Guardian.AuthorizationService
{
    public class AuthorizationServiceDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<UserPool> UserPool { get; set; }

        public DbSet<PoolUser> PoolUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLExpress;Database=Guardian;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configures one-to-many relationship
            modelBuilder.Entity<PoolUser>()
                .HasKey(x => new { x.UserPoolId, x.UserId });
        }
    }
}
