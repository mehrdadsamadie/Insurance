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
        public DbSet<FirstContractor> FirstContractor { get; set; }
        public DbSet<SecondContractor> secondContractors { get; set; }
        public DbSet<MGA> MGA { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FirstContractor>()
                .HasOne(d => d.Carrier)
                .WithMany(p => p.FirstContractors)
                .HasForeignKey(d => d.CarrierId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FirstContractor>()
                .HasOne(d => d.Advisor)
                .WithMany(p => p.FirstContractors)
                .HasForeignKey(d => d.AdvisorId)
             .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FirstContractor>()
                .HasOne(d => d.MGA)
                .WithMany(p => p.FirstContractors)
                .HasForeignKey(d => d.MGAId)
             .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SecondContractor>()
                .HasOne(d => d.Carrier)
                .WithMany(p => p.SecondContractors)
                .HasForeignKey(d => d.CarrierId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SecondContractor>()
                .HasOne(d => d.Advisor)
                .WithMany(p => p.SecondContractors)
                .HasForeignKey(d => d.AdvisorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SecondContractor>()
                .HasOne(d => d.MGA)
                .WithMany(p => p.SecondContractors)
                .HasForeignKey(d => d.MGAId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Contract>()
                 .HasOne(d => d.FirstContractor)
                .WithMany(p => p.Contracts)
                .HasForeignKey(d => d.FirstContractorId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Contract>()
                .HasOne(d => d.SecondContractor)
                .WithMany(p => p.Contracts)
                .HasForeignKey(d => d.SecondContractorId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Carrier>();
            modelBuilder.Entity<Advisor>();
            modelBuilder.Entity<MGA>();
        }
    }
}
