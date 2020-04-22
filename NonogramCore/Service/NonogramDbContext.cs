using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using nonogram.Entity;

namespace nonogram.Service
{
    public class NonogramDbContext : DbContext
    {
        public DbSet<Score> Scores { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Nonogram;Trusted_Connection=True;");
        }
    }
}
