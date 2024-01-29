using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Cine.Models
{
    public partial class CineContext : DbContext
    {
        public CineContext()
        {
        }

        public CineContext(DbContextOptions<CineContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Asiento> Asientos { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Pelicula> Peliculas { get; set; } = null!;
        public virtual DbSet<Sala> Salas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
   }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asiento>(entity =>
            {
                entity.HasKey(e => e.NumeroAsiento)
                    .HasName("PK__Asiento__366D7CD771A23AB7");

                entity.ToTable("Asiento");

                entity.Property(e => e.NumeroAsiento).ValueGeneratedNever();

                entity.Property(e => e.Estado)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.NumeroSalaNavigation)
                    .WithMany(p => p.AsientosNavigation)
                    .HasForeignKey(d => d.NumeroSala)
                    .HasConstraintName("FK__Asiento__NumeroS__4CA06362");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Nit)
                    .HasName("PK__Cliente__C7DEC3C32297DF57");

                entity.ToTable("Cliente");

                entity.Property(e => e.Nit)
                    .ValueGeneratedNever()
                    .HasColumnName("NIT");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pelicula>(entity =>
            {
                entity.ToTable("Pelicula");

                entity.Property(e => e.Actores)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Clasificacion)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.DiaTransmision).HasColumnType("date");

                entity.Property(e => e.Director)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Duracion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Genero)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Idioma)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Sinopsis)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("url");

                entity.HasOne(d => d.NumeroSalaNavigation)
                    .WithMany(p => p.Peliculas)
                    .HasForeignKey(d => d.NumeroSala)
                    .HasConstraintName("FK__Pelicula__Numero__4F7CD00D");
            });

            modelBuilder.Entity<Sala>(entity =>
            {
                entity.HasKey(e => e.NumeroSala)
                    .HasName("PK__Sala__C44387CF3E18A841");

                entity.ToTable("Sala");

                entity.Property(e => e.Estado)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
