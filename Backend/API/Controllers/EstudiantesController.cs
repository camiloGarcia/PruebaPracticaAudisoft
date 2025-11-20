using Microsoft.AspNetCore.Mvc;
using PruebaPracticaAudisoft.Application.DTOs;
using PruebaPracticaAudisoft.Application.Interfaces;

namespace PruebaPracticaAudisoft.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstudiantesController : ControllerBase
{
    private readonly IEstudianteService _service;
    private readonly ILogger<EstudiantesController> _logger;

    public EstudiantesController(IEstudianteService service, ILogger<EstudiantesController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<EstudianteDto>>> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? orderBy = null,
        [FromQuery] string? filterBy = null,
        [FromQuery] string? filterValue = null)
    {
        try
        {
            var estudiantes = await _service.GetAllAsync(page, pageSize, orderBy, filterBy, filterValue);
            return Ok(estudiantes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener estudiantes");
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EstudianteDto>> GetById(int id)
    {
        try
        {
            var estudiante = await _service.GetByIdAsync(id);
            if (estudiante == null)
                return NotFound($"Estudiante con ID {id} no encontrado");

            return Ok(estudiante);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener estudiante {Id}", id);
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpPost]
    public async Task<ActionResult<EstudianteDto>> Create([FromBody] CreateEstudianteDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var estudiante = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = estudiante.Id }, estudiante);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear estudiante");
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateEstudianteDto dto)
    {
        try
        {
            if (id != dto.Id)
                return BadRequest("El ID no coincide");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.UpdateAsync(dto);
            if (!result)
                return NotFound($"Estudiante con ID {id} no encontrado");

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar estudiante {Id}", id);
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var result = await _service.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(new { message = result.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar estudiante {Id}", id);
            return StatusCode(500, "Error interno del servidor");
        }
    }
}
