import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ProfesorService } from '../../services/profesor.service';
import { Profesor, CreateProfesor } from '../../models/profesor.model';

@Component({
  selector: 'app-profesores',
  imports: [CommonModule, FormsModule],
  templateUrl: './profesores.html',
  styleUrl: './profesores.css',
})
export class Profesores implements OnInit {
  profesores: Profesor[] = [];
  selectedProfesor: Profesor | null = null;
  isEditing = false;
  showForm = false;
  filterValue = '';
  
  formData: CreateProfesor = {
    nombre: ''
  };

  constructor(private profesorService: ProfesorService) {}

  ngOnInit(): void {
    this.loadProfesores();
  }

  loadProfesores(): void {
    this.profesorService.getAll().subscribe({
      next: (data) => this.profesores = data,
      error: (error) => console.error('Error al cargar profesores:', error)
    });
  }

  filterProfesores(): void {
    if (this.filterValue.trim()) {
      this.profesorService.getAll('nombre', 'nombre', this.filterValue).subscribe({
        next: (data) => this.profesores = data,
        error: (error) => console.error('Error al filtrar:', error)
      });
    } else {
      this.loadProfesores();
    }
  }

  openCreateForm(): void {
    this.showForm = true;
    this.isEditing = false;
    this.formData = { nombre: '' };
    this.selectedProfesor = null;
  }

  openEditForm(profesor: Profesor): void {
    this.showForm = true;
    this.isEditing = true;
    this.selectedProfesor = profesor;
    this.formData = { nombre: profesor.nombre };
  }

  closeForm(): void {
    this.showForm = false;
    this.isEditing = false;
    this.selectedProfesor = null;
    this.formData = { nombre: '' };
  }

  saveProfesor(): void {
    if (!this.formData.nombre.trim()) {
      alert('El nombre es requerido');
      return;
    }

    if (this.isEditing && this.selectedProfesor) {
      const updatedProfesor: Profesor = {
        id: this.selectedProfesor.id,
        nombre: this.formData.nombre
      };
      this.profesorService.update(this.selectedProfesor.id, updatedProfesor).subscribe({
        next: () => {
          this.loadProfesores();
          this.closeForm();
        },
        error: (error) => console.error('Error al actualizar:', error)
      });
    } else {
      this.profesorService.create(this.formData).subscribe({
        next: () => {
          this.loadProfesores();
          this.closeForm();
        },
        error: (error) => console.error('Error al crear:', error)
      });
    }
  }

  deleteProfesor(id: number): void {
    if (confirm('¿Está seguro de eliminar este profesor?')) {
      this.profesorService.delete(id).subscribe({
        next: () => this.loadProfesores(),
        error: (error) => console.error('Error al eliminar:', error)
      });
    }
  }
}
