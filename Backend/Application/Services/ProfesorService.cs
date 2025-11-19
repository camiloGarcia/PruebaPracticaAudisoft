using Microsoft.EntityFrameworkCore;
using PruebaPracticaAudisoft.Application.DTOs;
using PruebaPracticaAudisoft.Application.Interfaces;
using PruebaPracticaAudisoft.Domain.Entities;
using PruebaPracticaAudisoft.Infrastructure.Data;
using System.Linq.Dynamic.Core;

namespace PruebaPracticaAudisoft.Application.Services;

public class ProfesorService : IProfesorService
{
    private readonly ApplicationDbContext _context;

    public ProfesorService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProfesorDto>> GetAllAsync(string? orderBy = null, string? filterBy = null, string? filterValue = null)
    {
        var query = _context.Profesores.AsQueryable();

        // Filtrado
        if (!string.IsNullOrEmpty(filterBy) && !string.IsNullOrEmpty(filterValue))
        {
            query = filterBy.ToLower() switch
            {
                "nombre" => query.Where(p => p.Nombre.Contains(filterValue)),
                "id" => int.TryParse(filterValue, out var id) ? query.Where(p => p.Id == id) : query,
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
                query = query.OrderBy(p => p.Id);
            }
        }
        else
        {
            query = query.OrderBy(p => p.Id);
        }

        var profesores = await query.ToListAsync();
        return profesores.Select(p => new ProfesorDto
        {
            Id = p.Id,
            Nombre = p.Nombre
        });
    }

    public async Task<ProfesorDto?> GetByIdAsync(int id)
    {
        var profesor = await _context.Profesores.FindAsync(id);
        if (profesor == null) return null;

        return new ProfesorDto
        {
            Id = profesor.Id,
            Nombre = profesor.Nombre
        };
    }

    public async Task<ProfesorDto> CreateAsync(CreateProfesorDto dto)
    {
        var profesor = new Profesor
        {
            Nombre = dto.Nombre
        };

        _context.Profesores.Add(profesor);
        await _context.SaveChangesAsync();

        return new ProfesorDto
        {
            Id = profesor.Id,
            Nombre = profesor.Nombre
        };
    }

    public async Task<bool> UpdateAsync(UpdateProfesorDto dto)
    {
        var profesor = await _context.Profesores.FindAsync(dto.Id);
        if (profesor == null) return false;

        profesor.Nombre = dto.Nombre;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var profesor = await _context.Profesores.FindAsync(id);
        if (profesor == null) return false;

        _context.Profesores.Remove(profesor);
        await _context.SaveChangesAsync();

        return true;
    }
}
