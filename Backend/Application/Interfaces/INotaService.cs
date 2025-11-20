using PruebaPracticaAudisoft.Application.DTOs;

namespace PruebaPracticaAudisoft.Application.Interfaces;

public interface INotaService
{
    Task<PagedResult<NotaDto>> GetAllAsync(int page = 1, int pageSize = 10, string? orderBy = null, string? filterBy = null, string? filterValue = null);
    Task<NotaDto?> GetByIdAsync(int id);
    Task<NotaDto> CreateAsync(CreateNotaDto dto);
    Task<bool> UpdateAsync(UpdateNotaDto dto);
    Task<bool> DeleteAsync(int id);
}
