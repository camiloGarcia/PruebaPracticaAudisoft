using PruebaPracticaAudisoft.Application.DTOs;

namespace PruebaPracticaAudisoft.Application.Interfaces;

public interface IEstudianteService
{
    Task<PagedResult<EstudianteDto>> GetAllAsync(int page = 1, int pageSize = 10, string? orderBy = null, string? filterBy = null, string? filterValue = null);
    Task<EstudianteDto?> GetByIdAsync(int id);
    Task<EstudianteDto> CreateAsync(CreateEstudianteDto dto);
    Task<bool> UpdateAsync(UpdateEstudianteDto dto);
    Task<OperationResult> DeleteAsync(int id);
}
