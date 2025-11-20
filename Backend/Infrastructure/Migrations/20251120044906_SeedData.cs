using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruebaPracticaAudisoft.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insertar 15 estudiantes solo si no existen
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM Estudiantes WHERE Nombre = 'Juan García')
                BEGIN
                    INSERT INTO Estudiantes (Nombre) VALUES
                    ('Juan García'),
                    ('María Rodríguez'),
                    ('Carlos López'),
                    ('Ana Martínez'),
                    ('Pedro González'),
                    ('Laura Sánchez'),
                    ('Diego Ramírez'),
                    ('Sofía Torres'),
                    ('Miguel Flores'),
                    ('Valentina Rivera'),
                    ('Andrés Gómez'),
                    ('Camila Díaz'),
                    ('Santiago Herrera'),
                    ('Isabella Castro'),
                    ('Mateo Vargas')
                END
            ");

            // Insertar 15 profesores solo si no existen
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM Profesores WHERE Nombre = 'Roberto Fernández')
                BEGIN
                    INSERT INTO Profesores (Nombre) VALUES
                    ('Roberto Fernández'),
                    ('Carmen Ruiz'),
                    ('Fernando Jiménez'),
                    ('Patricia Morales'),
                    ('Ricardo Ortiz'),
                    ('Gabriela Silva'),
                    ('Alberto Mendoza'),
                    ('Elena Cruz'),
                    ('Javier Ramos'),
                    ('Mónica Romero'),
                    ('Héctor Medina'),
                    ('Diana Guerrero'),
                    ('Gustavo Navarro'),
                    ('Beatriz Peña'),
                    ('Ernesto Cortés')
                END
            ");

            // Insertar 15 notas solo si no existen
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM Notas WHERE Nombre = 'Cálculo I' AND IdEstudiante = 1)
                BEGIN
                    DECLARE @EstudianteId1 INT = (SELECT TOP 1 Id FROM Estudiantes ORDER BY Id)
                    DECLARE @EstudianteId2 INT = (SELECT Id FROM Estudiantes ORDER BY Id OFFSET 1 ROWS FETCH NEXT 1 ROWS ONLY)
                    DECLARE @EstudianteId3 INT = (SELECT Id FROM Estudiantes ORDER BY Id OFFSET 2 ROWS FETCH NEXT 1 ROWS ONLY)
                    DECLARE @EstudianteId4 INT = (SELECT Id FROM Estudiantes ORDER BY Id OFFSET 3 ROWS FETCH NEXT 1 ROWS ONLY)
                    DECLARE @EstudianteId5 INT = (SELECT Id FROM Estudiantes ORDER BY Id OFFSET 4 ROWS FETCH NEXT 1 ROWS ONLY)
                    DECLARE @EstudianteId6 INT = (SELECT Id FROM Estudiantes ORDER BY Id OFFSET 5 ROWS FETCH NEXT 1 ROWS ONLY)
                    DECLARE @EstudianteId7 INT = (SELECT Id FROM Estudiantes ORDER BY Id OFFSET 6 ROWS FETCH NEXT 1 ROWS ONLY)
                    DECLARE @EstudianteId8 INT = (SELECT Id FROM Estudiantes ORDER BY Id OFFSET 7 ROWS FETCH NEXT 1 ROWS ONLY)
                    DECLARE @EstudianteId9 INT = (SELECT Id FROM Estudiantes ORDER BY Id OFFSET 8 ROWS FETCH NEXT 1 ROWS ONLY)
                    DECLARE @EstudianteId10 INT = (SELECT Id FROM Estudiantes ORDER BY Id OFFSET 9 ROWS FETCH NEXT 1 ROWS ONLY)
                    DECLARE @EstudianteId11 INT = (SELECT Id FROM Estudiantes ORDER BY Id OFFSET 10 ROWS FETCH NEXT 1 ROWS ONLY)
                    DECLARE @EstudianteId12 INT = (SELECT Id FROM Estudiantes ORDER BY Id OFFSET 11 ROWS FETCH NEXT 1 ROWS ONLY)
                    DECLARE @EstudianteId13 INT = (SELECT Id FROM Estudiantes ORDER BY Id OFFSET 12 ROWS FETCH NEXT 1 ROWS ONLY)
                    DECLARE @EstudianteId14 INT = (SELECT Id FROM Estudiantes ORDER BY Id OFFSET 13 ROWS FETCH NEXT 1 ROWS ONLY)
                    DECLARE @EstudianteId15 INT = (SELECT Id FROM Estudiantes ORDER BY Id OFFSET 14 ROWS FETCH NEXT 1 ROWS ONLY)
                    
                    DECLARE @ProfesorId1 INT = (SELECT TOP 1 Id FROM Profesores ORDER BY Id)
                    DECLARE @ProfesorId2 INT = (SELECT Id FROM Profesores ORDER BY Id OFFSET 1 ROWS FETCH NEXT 1 ROWS ONLY)
                    DECLARE @ProfesorId3 INT = (SELECT Id FROM Profesores ORDER BY Id OFFSET 2 ROWS FETCH NEXT 1 ROWS ONLY)
                    DECLARE @ProfesorId4 INT = (SELECT Id FROM Profesores ORDER BY Id OFFSET 3 ROWS FETCH NEXT 1 ROWS ONLY)
                    DECLARE @ProfesorId5 INT = (SELECT Id FROM Profesores ORDER BY Id OFFSET 4 ROWS FETCH NEXT 1 ROWS ONLY)
                    DECLARE @ProfesorId6 INT = (SELECT Id FROM Profesores ORDER BY Id OFFSET 5 ROWS FETCH NEXT 1 ROWS ONLY)
                    DECLARE @ProfesorId7 INT = (SELECT Id FROM Profesores ORDER BY Id OFFSET 6 ROWS FETCH NEXT 1 ROWS ONLY)
                    DECLARE @ProfesorId8 INT = (SELECT Id FROM Profesores ORDER BY Id OFFSET 7 ROWS FETCH NEXT 1 ROWS ONLY)
                    DECLARE @ProfesorId9 INT = (SELECT Id FROM Profesores ORDER BY Id OFFSET 8 ROWS FETCH NEXT 1 ROWS ONLY)
                    DECLARE @ProfesorId10 INT = (SELECT Id FROM Profesores ORDER BY Id OFFSET 9 ROWS FETCH NEXT 1 ROWS ONLY)
                    DECLARE @ProfesorId11 INT = (SELECT Id FROM Profesores ORDER BY Id OFFSET 10 ROWS FETCH NEXT 1 ROWS ONLY)
                    DECLARE @ProfesorId12 INT = (SELECT Id FROM Profesores ORDER BY Id OFFSET 11 ROWS FETCH NEXT 1 ROWS ONLY)
                    DECLARE @ProfesorId13 INT = (SELECT Id FROM Profesores ORDER BY Id OFFSET 12 ROWS FETCH NEXT 1 ROWS ONLY)
                    DECLARE @ProfesorId14 INT = (SELECT Id FROM Profesores ORDER BY Id OFFSET 13 ROWS FETCH NEXT 1 ROWS ONLY)
                    DECLARE @ProfesorId15 INT = (SELECT Id FROM Profesores ORDER BY Id OFFSET 14 ROWS FETCH NEXT 1 ROWS ONLY)

                    INSERT INTO Notas (IdEstudiante, IdProfesor, Nombre, Valor) VALUES
                    (@EstudianteId1, @ProfesorId1, 'Cálculo I', 4.5),
                    (@EstudianteId2, @ProfesorId2, 'Física Mecánica', 3.8),
                    (@EstudianteId3, @ProfesorId3, 'Química Orgánica', 4.2),
                    (@EstudianteId4, @ProfesorId4, 'Biología Celular', 4.7),
                    (@EstudianteId5, @ProfesorId5, 'Historia Universal', 3.9),
                    (@EstudianteId6, @ProfesorId6, 'Literatura Española', 4.1),
                    (@EstudianteId7, @ProfesorId7, 'Programación I', 4.8),
                    (@EstudianteId8, @ProfesorId8, 'Inglés Avanzado', 4.3),
                    (@EstudianteId9, @ProfesorId9, 'Microeconomía', 3.7),
                    (@EstudianteId10, @ProfesorId10, 'Filosofía Moderna', 4.0),
                    (@EstudianteId11, @ProfesorId11, 'Historia del Arte', 4.4),
                    (@EstudianteId12, @ProfesorId12, 'Teoría Musical', 4.6),
                    (@EstudianteId13, @ProfesorId13, 'Deportes I', 5.0),
                    (@EstudianteId14, @ProfesorId14, 'Geografía Física', 3.6),
                    (@EstudianteId15, @ProfesorId15, 'Derecho Civil', 4.2)
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar datos en orden inverso por integridad referencial
            migrationBuilder.Sql("DELETE FROM Notas WHERE Id BETWEEN 1 AND 15");
            migrationBuilder.Sql("DELETE FROM Estudiantes WHERE Id BETWEEN 1 AND 15");
            migrationBuilder.Sql("DELETE FROM Profesores WHERE Id BETWEEN 1 AND 15");
        }
    }
}
