import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { EstudianteService } from '../../services/estudiante.service';
import { Estudiante, CreateEstudiante } from '../../models/estudiante.model';

@Component({
  selector: 'app-estudiantes',
  imports: [CommonModule, FormsModule],
  templateUrl: './estudiantes.html',
  styleUrl: './estudiantes.css',
})
export class Estudiantes implements OnInit {
  estudiantes: Estudiante[] = [];
  selectedEstudiante: Estudiante | null = null;
  isEditing = false;
  showForm = false;
  filterValue = '';
  
  formData: CreateEstudiante = {
    nombre: ''
  };

  constructor(private estudianteService: EstudianteService) {}

  ngOnInit(): void {
    this.loadEstudiantes();
  }

  loadEstudiantes(): void {
    this.estudianteService.getAll().subscribe({
      next: (data) => this.estudiantes = data,
      error: (error) => console.error('Error al cargar estudiantes:', error)
    });
  }

  filterEstudiantes(): void {
    if (this.filterValue.trim()) {
      this.estudianteService.getAll('nombre', 'nombre', this.filterValue).subscribe({
        next: (data) => this.estudiantes = data,
        error: (error) => console.error('Error al filtrar:', error)
      });
    } else {
      this.loadEstudiantes();
    }
  }

  openCreateForm(): void {
    this.showForm = true;
    this.isEditing = false;
    this.formData = { nombre: '' };
    this.selectedEstudiante = null;
  }

  openEditForm(estudiante: Estudiante): void {
    this.showForm = true;
    this.isEditing = true;
    this.selectedEstudiante = estudiante;
    this.formData = { nombre: estudiante.nombre };
  }

  closeForm(): void {
    this.showForm = false;
    this.isEditing = false;
    this.selectedEstudiante = null;
    this.formData = { nombre: '' };
  }

  saveEstudiante(): void {
    if (!this.formData.nombre.trim()) {
      alert('El nombre es requerido');
      return;
    }

    if (this.isEditing && this.selectedEstudiante) {
      const updatedEstudiante: Estudiante = {
        id: this.selectedEstudiante.id,
        nombre: this.formData.nombre
      };
      this.estudianteService.update(this.selectedEstudiante.id, updatedEstudiante).subscribe({
        next: () => {
          this.loadEstudiantes();
          this.closeForm();
        },
        error: (error) => console.error('Error al actualizar:', error)
      });
    } else {
      this.estudianteService.create(this.formData).subscribe({
        next: () => {
          this.loadEstudiantes();
          this.closeForm();
        },
        error: (error) => console.error('Error al crear:', error)
      });
    }
  }

  deleteEstudiante(id: number): void {
    if (confirm('¿Está seguro de eliminar este estudiante?')) {
      this.estudianteService.delete(id).subscribe({
        next: () => this.loadEstudiantes(),
        error: (error) => console.error('Error al eliminar:', error)
      });
    }
  }
}
