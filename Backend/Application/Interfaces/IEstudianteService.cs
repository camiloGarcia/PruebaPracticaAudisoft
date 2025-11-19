using PruebaPracticaAudisoft.Application.DTOs;

namespace PruebaPracticaAudisoft.Application.Interfaces;

public interface IEstudianteService
{
    Task<IEnumerable<EstudianteDto>> GetAllAsync(string? orderBy = null, string? filterBy = null, string? filterValue = null);
    Task<EstudianteDto?> GetByIdAsync(int id);
    Task<EstudianteDto> CreateAsync(CreateEstudianteDto dto);
    Task<bool> UpdateAsync(UpdateEstudianteDto dto);
    Task<bool> DeleteAsync(int id);
}
