using Insurance.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Insurance.DataAccessLayer
{
    public class InsuranceContext : DbContext
    {
        public InsuranceContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        { }
        public DbSet<Advisor> Advisor { get; set; }
        public DbSet<Carrier> Carrier { get; set; }
        public DbSet<Contract> Contract { get; set; }
        public DbSet<MGA> MGA { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contract>()
                .HasOne(d => d.Carrier)
                .WithMany(p => p.Contracts)
                .HasForeignKey(d => d.CarrierId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Contract>()
                .HasOne(d => d.Advisor)
                .WithMany(p => p.Contracts)
                .HasForeignKey(d => d.AdvisorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Contract>()
                .HasOne(d => d.MGA)
                .WithMany(p => p.Contracts)
                .HasForeignKey(d => d.MGAId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Carrier>();
            modelBuilder.Entity<Advisor>();
            modelBuilder.Entity<MGA>();
        }
    }
}
