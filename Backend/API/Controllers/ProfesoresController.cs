using Microsoft.AspNetCore.Mvc;
using PruebaPracticaAudisoft.Application.DTOs;
using PruebaPracticaAudisoft.Application.Interfaces;

namespace PruebaPracticaAudisoft.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfesoresController : ControllerBase
{
    private readonly IProfesorService _service;
    private readonly ILogger<ProfesoresController> _logger;

    public ProfesoresController(IProfesorService service, ILogger<ProfesoresController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProfesorDto>>> GetAll(
        [FromQuery] string? orderBy = null,
        [FromQuery] string? filterBy = null,
        [FromQuery] string? filterValue = null)
    {
        try
        {
            var profesores = await _service.GetAllAsync(orderBy, filterBy, filterValue);
            return Ok(profesores);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener profesores");
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProfesorDto>> GetById(int id)
    {
        try
        {
            var profesor = await _service.GetByIdAsync(id);
            if (profesor == null)
                return NotFound($"Profesor con ID {id} no encontrado");

            return Ok(profesor);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener profesor {Id}", id);
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpPost]
    public async Task<ActionResult<ProfesorDto>> Create([FromBody] CreateProfesorDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var profesor = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = profesor.Id }, profesor);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear profesor");
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateProfesorDto dto)
    {
        try
        {
            if (id != dto.Id)
                return BadRequest("El ID no coincide");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.UpdateAsync(dto);
            if (!result)
                return NotFound($"Profesor con ID {id} no encontrado");

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar profesor {Id}", id);
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
                return NotFound($"Profesor con ID {id} no encontrado");

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar profesor {Id}", id);
            return StatusCode(500, "Error interno del servidor");
        }
    }
}
