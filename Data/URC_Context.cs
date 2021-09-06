using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using URC.Models;
using System.Threading;

namespace WebApplication1.Data
{
    /*
     * This is the URC_Context that builds the databases
     */
    public class URC_Context : DbContext
    {
        public URC_Context(DbContextOptions<URC_Context> options)
            : base(options)
        {
        }

        public DbSet<Opportunity> Opportunity { get; set; }
        public DbSet<Application> Application { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Opportunity>().ToTable("Opportunity");

            // For Assignment 6
            modelBuilder.Entity<Application>().ToTable("Application")
                .Property(p => p.DateCreated)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Application>().ToTable("Application")
                .Property(p => p.TimeModified)
                .ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
        }
        // For Assignment 6
        
        /// <summary>
        /// Special code to handle the date created aspect of the application table
        /// </summary>
        /// <returns></returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellation = default)
        {
            var now = DateTime.UtcNow.ToLocalTime();

            // Handle Initial creation time
            foreach (var item in ChangeTracker.Entries<Application>()
                .Where(e => e.State == EntityState.Added))
            {
                item.Property("DateCreated").CurrentValue = now;
            }
            // Hangle modification time
            foreach (var item in ChangeTracker.Entries<Application>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                item.Property("TimeModified").CurrentValue = now;
            }
            return base.SaveChangesAsync();
        }
    }
}
