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

        public DbSet<Penjualan> Penjualan { get; set; }
        public DbSet<Produk> Produk { get; set; }
        public DbSet<PenjualanProduk> PenjualanProduk { get; set; }
        public DbSet<Register> Register { get; set; }
        public DbSet<Toko> Toko { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Toko -> Register
            modelBuilder.Entity<Toko>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<Register>()
                .HasKey(r => r.Id);
            modelBuilder.Entity<Toko>()
                .HasOne(t => t.Register)
                .WithOne(r => r.Toko)
                .HasForeignKey<Register>(r => r.Id)
                .IsRequired();

            // Register -> Penjualan
            modelBuilder.Entity<Penjualan>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<Register>()
                .HasOne(r => r.Penjualan)
                .WithOne(p => p.Register)
                .HasForeignKey<Penjualan>(p => p.Id)
                .IsRequired();

            // Penjualan -> Produk m-m
            modelBuilder.Entity<Produk>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<PenjualanProduk>()
                .HasKey(pp => new { pp.PenjualanId, pp.ProdukId });
            modelBuilder.Entity<PenjualanProduk>()
                .HasOne(pp => pp.Penjualan)
                .WithMany(pj => pj.PenjualanProduk)
                .HasForeignKey(pp => pp.PenjualanId);
            modelBuilder.Entity<PenjualanProduk>()
                .HasOne(pp => pp.Produk)
                .WithMany(p => p.PenjualanProduk)
                .HasForeignKey(pp => pp.ProdukId);
        }
    }
}
