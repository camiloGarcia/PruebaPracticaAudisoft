namespace PruebaPracticaAudisoft.Application.DTOs;

public class NotaDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int IdProfesor { get; set; }
    public string NombreProfesor { get; set; } = string.Empty;
    public int IdEstudiante { get; set; }
    public string NombreEstudiante { get; set; } = string.Empty;
    public decimal Valor { get; set; }
}

public class CreateNotaDto
{
    public string Nombre { get; set; } = string.Empty;
    public int IdProfesor { get; set; }
    public int IdEstudiante { get; set; }
    public decimal Valor { get; set; }
}

public class UpdateNotaDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int IdProfesor { get; set; }
    public int IdEstudiante { get; set; }
    public decimal Valor { get; set; }
}
