using Microsoft.AspNetCore.Mvc;
using PruebaPracticaAudisoft.Application.DTOs;
using PruebaPracticaAudisoft.Application.Interfaces;

namespace PruebaPracticaAudisoft.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotasController : ControllerBase
{
    private readonly INotaService _service;
    private readonly ILogger<NotasController> _logger;

    public NotasController(INotaService service, ILogger<NotasController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<NotaDto>>> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? orderBy = null,
        [FromQuery] string? filterBy = null,
        [FromQuery] string? filterValue = null)
    {
        try
        {
            var notas = await _service.GetAllAsync(page, pageSize, orderBy, filterBy, filterValue);
            return Ok(notas);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener notas");
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<NotaDto>> GetById(int id)
    {
        try
        {
            var nota = await _service.GetByIdAsync(id);
            if (nota == null)
                return NotFound($"Nota con ID {id} no encontrada");

            return Ok(nota);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener nota {Id}", id);
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpPost]
    public async Task<ActionResult<NotaDto>> Create([FromBody] CreateNotaDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var nota = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = nota.Id }, nota);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear nota");
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateNotaDto dto)
    {
        try
        {
            if (id != dto.Id)
                return BadRequest("El ID no coincide");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.UpdateAsync(dto);
            if (!result)
                return NotFound($"Nota con ID {id} no encontrada");

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar nota {Id}", id);
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var result = await _service.DeleteAsync(id);
            if (!result)
                return NotFound($"Nota con ID {id} no encontrada");

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar nota {Id}", id);
            return StatusCode(500, "Error interno del servidor");
        }
    }
}
