using PruebaPracticaAudisoft.Application.DTOs;

namespace PruebaPracticaAudisoft.Application.Interfaces;

public interface IProfesorService
{
    Task<PagedResult<ProfesorDto>> GetAllAsync(int page = 1, int pageSize = 10, string? orderBy = null, string? filterBy = null, string? filterValue = null);
    Task<ProfesorDto?> GetByIdAsync(int id);
    Task<ProfesorDto> CreateAsync(CreateProfesorDto dto);
    Task<bool> UpdateAsync(UpdateProfesorDto dto);
    Task<OperationResult> DeleteAsync(int id);
}
