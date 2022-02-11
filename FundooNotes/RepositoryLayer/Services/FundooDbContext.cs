using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.User;

namespace RepositoryLayer.Services
{
    public class FundooDbContext : DbContext
    {
        public FundooDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<UserModel> Users { get; set; }
        protected override void
            OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>()
            .HasIndex(u => u.email)
            .IsUnique();
        }
    }
}
