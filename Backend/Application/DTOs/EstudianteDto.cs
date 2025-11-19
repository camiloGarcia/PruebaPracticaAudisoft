namespace PruebaPracticaAudisoft.Application.DTOs;

public class EstudianteDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
}

public class CreateEstudianteDto
{
    public string Nombre { get; set; } = string.Empty;
}

public class UpdateEstudianteDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
}
