using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TokoSayaAPI.Models;

namespace TokoSayaAPI.Data
{
    public class TokoSayaContext : DbContext
    {
        public TokoSayaContext(DbContextOptions<TokoSayaContext> options) 
            : base(options) 
        {
        
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Cashier> Cashiers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<TransactionItem> TransactionItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User -> Cashier
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
            modelBuilder.Entity<Cashier>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<User>()
                .HasOne(u => u.Cashier)
                .WithOne(c => c.User)
                .HasForeignKey<Cashier>(c => c.Id)
                .IsRequired();

            // Cashier -> Transaction 1-m
            modelBuilder.Entity<Transaction>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Cashier)
                .WithMany(c => c.Transactions)
                .HasForeignKey(c => c.CashierId);

            // Transaction -> Item m-m
            modelBuilder.Entity<Item>()
                .HasKey(i => i.Id);
            modelBuilder.Entity<TransactionItem>()
                .HasKey(ti => new {ti.TransactionId, ti.ItemId});
            modelBuilder.Entity<TransactionItem>()
                .HasOne(ti => ti.Transaction)
                .WithMany(t => t.TransactionItems)
                .HasForeignKey(ti => ti.TransactionId);
            modelBuilder.Entity<TransactionItem>()
                .HasOne(ti => ti.Item)
                .WithMany(i => i.TransactionItems)
                .HasForeignKey(ti => ti.ItemId);
        }
    }
}
