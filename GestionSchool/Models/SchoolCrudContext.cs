using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GestionSchool.Models;

public partial class SchoolCrudContext : DbContext
{
    public SchoolCrudContext()
    {
    }

    public SchoolCrudContext(DbContextOptions<SchoolCrudContext> options)
        : base(options)
    {
    }

  
    public virtual DbSet<Login> Logins { get; set; }
   
    public virtual DbSet<Profesore> Profesores { get; set; }
  

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DESKTOP-UVK8L71;Database=SchoolCrud;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      

       

        modelBuilder.Entity<Login>(entity =>
        {
            entity.ToTable("Login");
        });


        modelBuilder.Entity<Profesore>(entity =>
        {
            entity.HasKey(e => e.DocumentoIdentidad).HasName("PK__Profesor__049E81A816F39792");

            entity.Property(e => e.DocumentoIdentidad).ValueGeneratedNever();
            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ImagenUrl)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombres)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Profesion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

