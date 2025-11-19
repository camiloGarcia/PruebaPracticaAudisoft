using Microsoft.EntityFrameworkCore;
using PruebaPracticaAudisoft.Domain.Entities;

namespace PruebaPracticaAudisoft.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Estudiante> Estudiantes { get; set; }
    public DbSet<Profesor> Profesores { get; set; }
    public DbSet<Nota> Notas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de Estudiante
        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.ToTable("Estudiantes");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);
            entity.HasMany(e => e.Notas)
                  .WithOne(n => n.Estudiante)
                  .HasForeignKey(n => n.IdEstudiante)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Configuración de Profesor
        modelBuilder.Entity<Profesor>(entity =>
        {
            entity.ToTable("Profesores");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).ValueGeneratedOnAdd();
            entity.Property(p => p.Nombre).IsRequired().HasMaxLength(200);
            entity.HasMany(p => p.Notas)
                  .WithOne(n => n.Profesor)
                  .HasForeignKey(n => n.IdProfesor)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Configuración de Nota
        modelBuilder.Entity<Nota>(entity =>
        {
            entity.ToTable("Notas");
            entity.HasKey(n => n.Id);
            entity.Property(n => n.Id).ValueGeneratedOnAdd();
            entity.Property(n => n.Nombre).IsRequired().HasMaxLength(200);
            entity.Property(n => n.Valor).HasColumnType("decimal(5,2)").IsRequired();
            entity.Property(n => n.IdProfesor).IsRequired();
            entity.Property(n => n.IdEstudiante).IsRequired();
        });
    }
}
