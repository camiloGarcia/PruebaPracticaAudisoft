using PruebaPracticaAudisoft.Application.DTOs;

namespace PruebaPracticaAudisoft.Application.Interfaces;

public interface INotaService
{
    Task<IEnumerable<NotaDto>> GetAllAsync(string? orderBy = null, string? filterBy = null, string? filterValue = null);
    Task<NotaDto?> GetByIdAsync(int id);
    Task<NotaDto> CreateAsync(CreateNotaDto dto);
    Task<bool> UpdateAsync(UpdateNotaDto dto);
    Task<bool> DeleteAsync(int id);
}
