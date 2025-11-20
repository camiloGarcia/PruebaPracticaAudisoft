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

    public async Task<DTOs.PagedResult<ProfesorDto>> GetAllAsync(int page = 1, int pageSize = 10, string? orderBy = null, string? filterBy = null, string? filterValue = null)
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

        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        var profesores = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new DTOs.PagedResult<ProfesorDto>
        {
            Data = profesores.Select(p => new ProfesorDto
            {
                Id = p.Id,
                Nombre = p.Nombre
            }),
            CurrentPage = page,
            PageSize = pageSize,
            TotalItems = totalItems,
            TotalPages = totalPages
        };
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

    public async Task<OperationResult> DeleteAsync(int id)
    {
        var profesor = await _context.Profesores
            .Include(p => p.Notas)
            .FirstOrDefaultAsync(p => p.Id == id);
        
        if (profesor == null)
        {
            return new OperationResult
            {
                Success = false,
                Message = "El profesor no existe"
            };
        }

        if (profesor.Notas.Any())
        {
            return new OperationResult
            {
                Success = false,
                Message = "No se puede eliminar el profesor porque tiene notas asociadas"
            };
        }

        _context.Profesores.Remove(profesor);
        await _context.SaveChangesAsync();

        return new OperationResult
        {
            Success = true,
            Message = "Profesor eliminado exitosamente"
        };
    }
}
