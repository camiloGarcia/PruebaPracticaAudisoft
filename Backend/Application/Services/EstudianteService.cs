using Microsoft.EntityFrameworkCore;
using PruebaPracticaAudisoft.Application.DTOs;
using PruebaPracticaAudisoft.Application.Interfaces;
using PruebaPracticaAudisoft.Domain.Entities;
using PruebaPracticaAudisoft.Infrastructure.Data;
using System.Linq.Dynamic.Core;

namespace PruebaPracticaAudisoft.Application.Services;

public class EstudianteService : IEstudianteService
{
    private readonly ApplicationDbContext _context;

    public EstudianteService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EstudianteDto>> GetAllAsync(string? orderBy = null, string? filterBy = null, string? filterValue = null)
    {
        var query = _context.Estudiantes.AsQueryable();

        // Filtrado
        if (!string.IsNullOrEmpty(filterBy) && !string.IsNullOrEmpty(filterValue))
        {
            query = filterBy.ToLower() switch
            {
                "nombre" => query.Where(e => e.Nombre.Contains(filterValue)),
                "id" => int.TryParse(filterValue, out var id) ? query.Where(e => e.Id == id) : query,
                _ => query
            };
        }

        // Ordenamiento
        if (!string.IsNullOrEmpty(orderBy))
        {
            try
            {
                query = query.OrderBy(orderBy);
            }
            catch
            {
                query = query.OrderBy(e => e.Id);
            }
        }
        else
        {
            query = query.OrderBy(e => e.Id);
        }

        var estudiantes = await query.ToListAsync();
        return estudiantes.Select(e => new EstudianteDto
        {
            Id = e.Id,
            Nombre = e.Nombre
        });
    }

    public async Task<EstudianteDto?> GetByIdAsync(int id)
    {
        var estudiante = await _context.Estudiantes.FindAsync(id);
        if (estudiante == null) return null;

        return new EstudianteDto
        {
            Id = estudiante.Id,
            Nombre = estudiante.Nombre
        };
    }

    public async Task<EstudianteDto> CreateAsync(CreateEstudianteDto dto)
    {
        var estudiante = new Estudiante
        {
            Nombre = dto.Nombre
        };

        _context.Estudiantes.Add(estudiante);
        await _context.SaveChangesAsync();

        return new EstudianteDto
        {
            Id = estudiante.Id,
            Nombre = estudiante.Nombre
        };
    }

    public async Task<bool> UpdateAsync(UpdateEstudianteDto dto)
    {
        var estudiante = await _context.Estudiantes.FindAsync(dto.Id);
        if (estudiante == null) return false;

        estudiante.Nombre = dto.Nombre;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var estudiante = await _context.Estudiantes.FindAsync(id);
        if (estudiante == null) return false;

        _context.Estudiantes.Remove(estudiante);
        await _context.SaveChangesAsync();

        return true;
    }
}
