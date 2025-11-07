using System;
using System.Collections.Generic;
using AppDocentes.Models;
using Microsoft.EntityFrameworkCore;

namespace AppDocentes.Data;

public partial class DocentesDbContext : DbContext
{
    public DocentesDbContext()
    {
    }

    public DocentesDbContext(DbContextOptions<DocentesDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Carrera> Carreras { get; set; }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Dato> Datos { get; set; }

    public virtual DbSet<Docente> Docentes { get; set; }

    public virtual DbSet<Escuela> Escuelas { get; set; }

    public virtual DbSet<Grado> Grados { get; set; }

    public virtual DbSet<Horario> Horarios { get; set; }

    public virtual DbSet<Modulo> Modulos { get; set; }

    public virtual DbSet<MovDocente> MovDocentes { get; set; }

    public virtual DbSet<Sede> Sedes { get; set; }

    public virtual DbSet<Semestre> Semestres { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("admincft");

        modelBuilder.Entity<Carrera>(entity =>
        {
            entity.HasKey(e => e.IdCarrera);

            entity.ToTable("Carrera", "dbo");

            entity.Property(e => e.IdCarrera).ValueGeneratedNever();
            entity.Property(e => e.NomCarrera)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria);

            entity.ToTable("Categoria", "dbo");

            entity.Property(e => e.IdCategoria).ValueGeneratedNever();
            entity.Property(e => e.NomCategoria)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Dato>(entity =>
        {
            entity.HasKey(e => e.IdDatos);

            entity.ToTable("Datos", "dbo");

            entity.Property(e => e.IdDatos).ValueGeneratedNever();
            entity.Property(e => e.DatosAcademicos)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDocentesNavigation).WithMany(p => p.Datos)
                .HasForeignKey(d => d.IdDocentes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Datos_Docente");

            entity.HasOne(d => d.IdGradosNavigation).WithMany(p => p.Datos)
                .HasForeignKey(d => d.IdGrados)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Datos_Grado");
        });

        modelBuilder.Entity<Docente>(entity =>
        {
            entity.HasKey(e => e.IdDocente);

            entity.ToTable("Docente", "dbo");

            entity.Property(e => e.IdDocente).ValueGeneratedNever();
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EstadoCivil)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MatDocente)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nacionalidad)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NomDocente)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PatDocente)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Rut)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Docentes)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Docente_Categoria");
        });

        modelBuilder.Entity<Escuela>(entity =>
        {
            entity.HasKey(e => e.IdEscuela);

            entity.ToTable("Escuela", "dbo");

            entity.Property(e => e.IdEscuela).ValueGeneratedNever();
            entity.Property(e => e.NomEscuela)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Grado>(entity =>
        {
            entity.HasKey(e => e.IdGrado);

            entity.ToTable("Grado", "dbo");

            entity.Property(e => e.IdGrado).ValueGeneratedNever();
            entity.Property(e => e.NomGrado)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Horario>(entity =>
        {
            entity.HasKey(e => e.IdHorario).HasName("PK_Horario_1");

            entity.ToTable("Horario", "dbo");

            entity.Property(e => e.IdHorario).ValueGeneratedNever();
            entity.Property(e => e.NomHorario)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Modulo>(entity =>
        {
            entity.HasKey(e => e.IdModulo);

            entity.ToTable("Modulo", "dbo");

            // 🔹 AUTOINCREMENTAL
            entity.Property(e => e.IdModulo)
                .ValueGeneratedOnAdd(); // <-- esta es la clave

            entity.Property(e => e.Horas)
                .HasColumnName("horas");

            entity.Property(e => e.NomModulo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCarreraNavigation)
                .WithMany(p => p.Modulos)
                .HasForeignKey(d => d.IdCarrera)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Modulo_Carrera");
        });


        modelBuilder.Entity<MovDocente>(entity =>
        {
            entity.ToTable("MovDocente", "dbo", tb =>
                {
                    tb.HasTrigger("trg_EvitarHorasNegativas");
                    tb.HasTrigger("trg_RegistroEliminaciones");
                    tb.HasTrigger("trg_RegistroInserciones");
                });

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IdCarrera).HasColumnName("idCarrera");

            entity.HasOne(d => d.IdDocenteNavigation).WithMany(p => p.MovDocentes)
                .HasForeignKey(d => d.IdDocente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MovDocente_Docente");

            entity.HasOne(d => d.IdHorarioNavigation).WithMany(p => p.MovDocentes)
                .HasForeignKey(d => d.IdHorario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MovDocente_Horario1");

            entity.HasOne(d => d.IdModuloNavigation).WithMany(p => p.MovDocentes)
                .HasForeignKey(d => d.IdModulo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MovDocente_Modulo");

            entity.HasOne(d => d.IdSedeNavigation).WithMany(p => p.MovDocentes)
                .HasForeignKey(d => d.IdSede)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MovDocente_Escuela");

            entity.HasOne(d => d.IdSede1).WithMany(p => p.MovDocentes)
                .HasForeignKey(d => d.IdSede)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MovDocente_Sede");

            entity.HasOne(d => d.IdSemestreNavigation).WithMany(p => p.MovDocentes)
                .HasForeignKey(d => d.IdSemestre)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MovDocente_Semestre");
        });

        modelBuilder.Entity<Sede>(entity =>
        {
            entity.HasKey(e => e.IdSede);

            entity.ToTable("Sede", "dbo");

            entity.Property(e => e.IdSede).ValueGeneratedNever();
            entity.Property(e => e.Comuna)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NomCiudad)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NomSede)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Semestre>(entity =>
        {
            entity.HasKey(e => e.IdSemestre);

            entity.ToTable("Semestre", "dbo");

            entity.Property(e => e.IdSemestre).ValueGeneratedNever();
            entity.Property(e => e.NomSemestre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK_usuario");

            entity.ToTable("Usuario", "dbo");

            entity.Property(e => e.CorrUsuario)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NomUsuario)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PassUsuario)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(u => u.IdUsuario)
                .UseIdentityColumn(); // or .ValueGeneratedOnAdd();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
