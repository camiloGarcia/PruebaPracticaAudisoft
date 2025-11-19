namespace PruebaPracticaAudisoft.Domain.Entities;

public class Nota
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int IdProfesor { get; set; }
    public int IdEstudiante { get; set; }
    public decimal Valor { get; set; }

    // Navegación
    public virtual Profesor Profesor { get; set; } = null!;
    public virtual Estudiante Estudiante { get; set; } = null!;
}
