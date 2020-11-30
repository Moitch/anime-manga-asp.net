using System;
using System.Collections.Generic;
using System.Text;
using COMP2084_Assignment1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace COMP2084_Assignment1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        // Define model classes

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Manga> Mangas { get; set; }
        public DbSet<MangaList> MangaLists { get; set; }
        public DbSet<Anime> Animes { get; set; }
        public DbSet<AnimeList> AnimeLists { get; set; }

        // Define relationships between tables
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Anime>()
                .HasOne(p => p.Genres)
                .WithMany(c => c.Animes)
                .HasForeignKey(p => p.GenreID)
                .HasConstraintName("FK_Animes_GenreID");

            builder.Entity<AnimeList>()
                .HasOne(p => p.Animes)
                .WithMany(c => c.AnimeLists)
                .HasForeignKey(p => p.AnimeID)
                .HasConstraintName("FK_AnimeLists_AnimeID");

            builder.Entity<Manga>()
                .HasOne(p => p.Genres)
                .WithMany(c => c.Mangas)
                .HasForeignKey(p => p.GenreID)
                .HasConstraintName("FK_Mangas_GenreID");

            builder.Entity<MangaList>()
                .HasOne(p => p.Mangas)
                .WithMany(c => c.MangaLists)
                .HasForeignKey(p => p.MangaID)
                .HasConstraintName("FK_OrderDetails_OrderId");
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
