using Microsoft.EntityFrameworkCore;
using PruebaPracticaAudisoft.Application.DTOs;
using PruebaPracticaAudisoft.Application.Interfaces;
using PruebaPracticaAudisoft.Domain.Entities;
using PruebaPracticaAudisoft.Infrastructure.Data;
using System.Linq.Dynamic.Core;

namespace PruebaPracticaAudisoft.Application.Services;

public class NotaService : INotaService
{
    private readonly ApplicationDbContext _context;

    public NotaService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DTOs.PagedResult<NotaDto>> GetAllAsync(int page = 1, int pageSize = 10, string? orderBy = null, string? filterBy = null, string? filterValue = null)
    {
        var query = _context.Notas
            .Include(n => n.Profesor)
            .Include(n => n.Estudiante)
            .AsQueryable();

        // Filtrado
        if (!string.IsNullOrEmpty(filterBy) && !string.IsNullOrEmpty(filterValue))
        {
            query = filterBy.ToLower() switch
            {
                "nombre" => query.Where(n => n.Nombre.Contains(filterValue)),
                "nombreprofesor" => query.Where(n => n.Profesor.Nombre.Contains(filterValue)),
                "nombreestudiante" => query.Where(n => n.Estudiante.Nombre.Contains(filterValue)),
                "id" => int.TryParse(filterValue, out var id) ? query.Where(n => n.Id == id) : query,
                "idprofesor" => int.TryParse(filterValue, out var idProf) ? query.Where(n => n.IdProfesor == idProf) : query,
                "idestudiante" => int.TryParse(filterValue, out var idEst) ? query.Where(n => n.IdEstudiante == idEst) : query,
                "valor" => decimal.TryParse(filterValue, out var valor) ? query.Where(n => n.Valor == valor) : query,
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
                query = query.OrderBy(n => n.Id);
            }
        }
        else
        {
            query = query.OrderBy(n => n.Id);
        }

        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        var notas = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new DTOs.PagedResult<NotaDto>
        {
            Data = notas.Select(n => new NotaDto
            {
                Id = n.Id,
                Nombre = n.Nombre,
                IdProfesor = n.IdProfesor,
                NombreProfesor = n.Profesor.Nombre,
                IdEstudiante = n.IdEstudiante,
                NombreEstudiante = n.Estudiante.Nombre,
                Valor = n.Valor
            }),
            CurrentPage = page,
            PageSize = pageSize,
            TotalItems = totalItems,
            TotalPages = totalPages
        };
    }

    public async Task<NotaDto?> GetByIdAsync(int id)
    {
        var nota = await _context.Notas
            .Include(n => n.Profesor)
            .Include(n => n.Estudiante)
            .FirstOrDefaultAsync(n => n.Id == id);

        if (nota == null) return null;

        return new NotaDto
        {
            Id = nota.Id,
            Nombre = nota.Nombre,
            IdProfesor = nota.IdProfesor,
            NombreProfesor = nota.Profesor.Nombre,
            IdEstudiante = nota.IdEstudiante,
            NombreEstudiante = nota.Estudiante.Nombre,
            Valor = nota.Valor
        };
    }

    public async Task<NotaDto> CreateAsync(CreateNotaDto dto)
    {
        var nota = new Nota
        {
            Nombre = dto.Nombre,
            IdProfesor = dto.IdProfesor,
            IdEstudiante = dto.IdEstudiante,
            Valor = dto.Valor
        };

        _context.Notas.Add(nota);
        await _context.SaveChangesAsync();

        // Recargar con includes
        await _context.Entry(nota).Reference(n => n.Profesor).LoadAsync();
        await _context.Entry(nota).Reference(n => n.Estudiante).LoadAsync();

        return new NotaDto
        {
            Id = nota.Id,
            Nombre = nota.Nombre,
            IdProfesor = nota.IdProfesor,
            NombreProfesor = nota.Profesor.Nombre,
            IdEstudiante = nota.IdEstudiante,
            NombreEstudiante = nota.Estudiante.Nombre,
            Valor = nota.Valor
        };
    }

    public async Task<bool> UpdateAsync(UpdateNotaDto dto)
    {
        var nota = await _context.Notas.FindAsync(dto.Id);
        if (nota == null) return false;

        nota.Nombre = dto.Nombre;
        nota.IdProfesor = dto.IdProfesor;
        nota.IdEstudiante = dto.IdEstudiante;
        nota.Valor = dto.Valor;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var nota = await _context.Notas.FindAsync(id);
        if (nota == null) return false;

        _context.Notas.Remove(nota);
        await _context.SaveChangesAsync();

        return true;
    }
}
