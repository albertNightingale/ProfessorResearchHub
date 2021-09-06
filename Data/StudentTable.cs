using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using URC.Models;

namespace URC.Data
{
    public class StudentTable : DbContext
    {
        public StudentTable(DbContextOptions<StudentTable> options)
            : base(options)
        {
        }

        public DbSet<Student> Opportunity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("Student");
        }
    }
}
