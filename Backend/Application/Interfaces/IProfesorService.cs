using PruebaPracticaAudisoft.Application.DTOs;

namespace PruebaPracticaAudisoft.Application.Interfaces;

public interface IProfesorService
{
    Task<IEnumerable<ProfesorDto>> GetAllAsync(string? orderBy = null, string? filterBy = null, string? filterValue = null);
    Task<ProfesorDto?> GetByIdAsync(int id);
    Task<ProfesorDto> CreateAsync(CreateProfesorDto dto);
    Task<bool> UpdateAsync(UpdateProfesorDto dto);
    Task<bool> DeleteAsync(int id);
}
