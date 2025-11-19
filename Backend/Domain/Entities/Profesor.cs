namespace PruebaPracticaAudisoft.Domain.Entities;

public class Profesor
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;

    // Navegación
    public virtual ICollection<Nota> Notas { get; set; } = new List<Nota>();
}
