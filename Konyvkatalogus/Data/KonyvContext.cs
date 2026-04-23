using Konyvkatalogus.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Konyvkatalogus.Data
{
    public class KonyvContext : DbContext
    {
        public KonyvContext(DbContextOptions<KonyvContext> options) : base(options)
        {
        }

        public DbSet<Konyv> Konyv { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Konyv>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Cim).IsRequired().HasMaxLength(300);
                entity.Property(e => e.KiadasIdeje).IsRequired();
                entity.Property(e => e.Szerzo).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Kategoria).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Isbn).IsRequired().HasMaxLength(20);
            });

            // Seed adatok hozzáadása – ezek kerülnek be a migráció során
            modelBuilder.Entity<Konyv>().HasData(
                new Konyv
                {
                    Id = 1,
                    Cim = "A Gyűrűk Ura: A Gyűrű Szövetsége",
                    Szerzo = "J.R.R. Tolkien",
                    KiadasIdeje = new DateTime(1954, 7, 29),
                    Kategoria = "Fantasy",
                    Isbn = "978-0-618-00222-8"
                },
                new Konyv
                {
                    Id = 2,
                    Cim = "Harry Potter és a Bölcsek Köve",
                    Szerzo = "J.K. Rowling",
                    KiadasIdeje = new DateTime(1997, 6, 26),
                    Kategoria = "Fantasy",
                    Isbn = "978-0-7475-3269-9"
                },
                new Konyv
                {
                    Id = 3,
                    Cim = "A kis herceg",
                    Szerzo = "Antoine de Saint-Exupéry",
                    KiadasIdeje = new DateTime(1943, 4, 6),
                    Kategoria = "Mese",
                    Isbn = "978-0-15-601219-5"
                },
                new Konyv
                {
                    Id = 4,
                    Cim = "1984",
                    Szerzo = "George Orwell",
                    KiadasIdeje = new DateTime(1949, 6, 8),
                    Kategoria = "Disztópia",
                    Isbn = "978-0-452-28423-4"
                }
            );
        }
    }
}