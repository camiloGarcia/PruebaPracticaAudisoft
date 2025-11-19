import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NotaService } from '../../services/nota.service';
import { EstudianteService } from '../../services/estudiante.service';
import { ProfesorService } from '../../services/profesor.service';
import { Nota, CreateNota } from '../../models/nota.model';
import { Estudiante } from '../../models/estudiante.model';
import { Profesor } from '../../models/profesor.model';

@Component({
  selector: 'app-notas',
  imports: [CommonModule, FormsModule],
  templateUrl: './notas.html',
  styleUrl: './notas.css',
})
export class Notas implements OnInit {
  notas: Nota[] = [];
  estudiantes: Estudiante[] = [];
  profesores: Profesor[] = [];
  selectedNota: Nota | null = null;
  isEditing = false;
  showForm = false;
  filterValue = '';
  
  formData: CreateNota = {
    nombre: '',
    idProfesor: 0,
    idEstudiante: 0,
    valor: 0
  };

  constructor(
    private notaService: NotaService,
    private estudianteService: EstudianteService,
    private profesorService: ProfesorService
  ) {}

  ngOnInit(): void {
    this.loadNotas();
    this.loadEstudiantes();
    this.loadProfesores();
  }

  loadNotas(): void {
    this.notaService.getAll().subscribe({
      next: (data) => this.notas = data,
      error: (error) => console.error('Error al cargar notas:', error)
    });
  }

  loadEstudiantes(): void {
    this.estudianteService.getAll().subscribe({
      next: (data) => this.estudiantes = data,
      error: (error) => console.error('Error al cargar estudiantes:', error)
    });
  }

  loadProfesores(): void {
    this.profesorService.getAll().subscribe({
      next: (data) => this.profesores = data,
      error: (error) => console.error('Error al cargar profesores:', error)
    });
  }

  filterNotas(): void {
    if (this.filterValue.trim()) {
      this.notaService.getAll('nombre', 'nombre', this.filterValue).subscribe({
        next: (data) => this.notas = data,
        error: (error) => console.error('Error al filtrar:', error)
      });
    } else {
      this.loadNotas();
    }
  }

  openCreateForm(): void {
    this.showForm = true;
    this.isEditing = false;
    this.formData = { nombre: '', idProfesor: 0, idEstudiante: 0, valor: 0 };
    this.selectedNota = null;
  }

  openEditForm(nota: Nota): void {
    this.showForm = true;
    this.isEditing = true;
    this.selectedNota = nota;
    this.formData = {
      nombre: nota.nombre,
      idProfesor: nota.idProfesor,
      idEstudiante: nota.idEstudiante,
      valor: nota.valor
    };
  }

  closeForm(): void {
    this.showForm = false;
    this.isEditing = false;
    this.selectedNota = null;
    this.formData = { nombre: '', idProfesor: 0, idEstudiante: 0, valor: 0 };
  }

  saveNota(): void {
    if (!this.formData.nombre.trim() || this.formData.idProfesor === 0 || this.formData.idEstudiante === 0) {
      alert('Todos los campos son requeridos');
      return;
    }

    if (this.isEditing && this.selectedNota) {
      const updatedNota: Nota = {
        id: this.selectedNota.id,
        nombre: this.formData.nombre,
        idProfesor: this.formData.idProfesor,
        nombreProfesor: '',
        idEstudiante: this.formData.idEstudiante,
        nombreEstudiante: '',
        valor: this.formData.valor
      };
      this.notaService.update(this.selectedNota.id, updatedNota).subscribe({
        next: () => {
          this.loadNotas();
          this.closeForm();
        },
        error: (error) => console.error('Error al actualizar:', error)
      });
    } else {
      this.notaService.create(this.formData).subscribe({
        next: () => {
          this.loadNotas();
          this.closeForm();
        },
        error: (error) => console.error('Error al crear:', error)
      });
    }
  }

  deleteNota(id: number): void {
    if (confirm('¿Está seguro de eliminar esta nota?')) {
      this.notaService.delete(id).subscribe({
        next: () => this.loadNotas(),
        error: (error) => console.error('Error al eliminar:', error)
      });
    }
  }
}
