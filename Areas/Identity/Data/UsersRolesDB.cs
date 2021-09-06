using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using URC.Areas.Identity.Data;
using URC.Models;

namespace URC.Data
{
    /*
     * This is the Users and roles database
     */
    public class UsersRolesDB : IdentityDbContext<URCUser>
    {
        
        public UsersRolesDB(DbContextOptions<UsersRolesDB> options)
            : base(options)
        {
            
        }

        public DbSet<URCUserProfile> UserProfiles { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Skill> Skills { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<URCUserProfile>().ToTable("URCUserProfile");

            builder.Entity<URCUserProfile>()
            .Property(f => f.URCUserProfileID)
            .ValueGeneratedOnAdd();

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<Notification>()
                .Property(n => n.CreatedTime)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}