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

    public async Task<DTOs.PagedResult<EstudianteDto>> GetAllAsync(int page = 1, int pageSize = 10, string? orderBy = null, string? filterBy = null, string? filterValue = null)
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

        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        var estudiantes = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new DTOs.PagedResult<EstudianteDto>
        {
            Data = estudiantes.Select(e => new EstudianteDto
            {
                Id = e.Id,
                Nombre = e.Nombre
            }),
            CurrentPage = page,
            PageSize = pageSize,
            TotalItems = totalItems,
            TotalPages = totalPages
        };
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

    public async Task<OperationResult> DeleteAsync(int id)
    {
        var estudiante = await _context.Estudiantes
            .Include(e => e.Notas)
            .FirstOrDefaultAsync(e => e.Id == id);
        
        if (estudiante == null)
        {
            return new OperationResult
            {
                Success = false,
                Message = "El estudiante no existe"
            };
        }

        if (estudiante.Notas.Any())
        {
            return new OperationResult
            {
                Success = false,
                Message = "No se puede eliminar el estudiante porque tiene notas asociadas"
            };
        }

        _context.Estudiantes.Remove(estudiante);
        await _context.SaveChangesAsync();

        return new OperationResult
        {
            Success = true,
            Message = "Estudiante eliminado exitosamente"
        };
    }
}
