import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ProfesorService } from '../../services/profesor.service';
import { Profesor, CreateProfesor } from '../../models/profesor.model';
import { PagedResult } from '../../models/common.model';

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
  
  currentPage = 1;
  pageSize = 10;
  totalItems = 0;
  totalPages = 0;
  
  showToast = false;
  toastMessage = '';
  toastType: 'success' | 'error' | 'info' = 'success';
  
  formData: CreateProfesor = {
    nombre: ''
  };

  constructor(private profesorService: ProfesorService) {}

  ngOnInit(): void {
    this.loadProfesores();
  }

  loadProfesores(): void {
    this.profesorService.getAll(this.currentPage, this.pageSize).subscribe({
      next: (result: PagedResult<Profesor>) => {
        this.profesores = result.data;
        this.currentPage = result.currentPage;
        this.pageSize = result.pageSize;
        this.totalItems = result.totalItems;
        this.totalPages = result.totalPages;
      },
      error: (error) => {
        console.error('Error al cargar profesores:', error);
        this.showToastMessage('Error al cargar profesores', 'error');
      }
    });
  }

  filterProfesores(): void {
    if (this.filterValue.trim()) {
      this.profesorService.getAll(this.currentPage, this.pageSize, undefined, 'nombre', this.filterValue).subscribe({
        next: (result: PagedResult<Profesor>) => {
          this.profesores = result.data;
          this.currentPage = result.currentPage;
          this.pageSize = result.pageSize;
          this.totalItems = result.totalItems;
          this.totalPages = result.totalPages;
        },
        error: (error) => {
          console.error('Error al filtrar:', error);
          this.showToastMessage('Error al filtrar profesores', 'error');
        }
      });
    } else {
      this.loadProfesores();
    }
  }

  goToPage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      if (this.filterValue.trim()) {
        this.filterProfesores();
      } else {
        this.loadProfesores();
      }
    }
  }

  get pages(): number[] {
    return Array.from({ length: this.totalPages }, (_, i) => i + 1);
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
      this.showToastMessage('El nombre es requerido', 'error');
      return;
    }

    if (this.isEditing && this.selectedProfesor) {
      const updatedProfesor: Profesor = {
        id: this.selectedProfesor.id,
        nombre: this.formData.nombre
      };
      this.profesorService.update(this.selectedProfesor.id, updatedProfesor).subscribe({
        next: () => {
          this.showToastMessage('Profesor actualizado exitosamente', 'success');
          this.loadProfesores();
          this.closeForm();
        },
        error: (error) => {
          console.error('Error al actualizar:', error);
          this.showToastMessage('Error al actualizar profesor', 'error');
        }
      });
    } else {
      this.profesorService.create(this.formData).subscribe({
        next: () => {
          this.showToastMessage('Profesor creado exitosamente', 'success');
          this.loadProfesores();
          this.closeForm();
        },
        error: (error) => {
          console.error('Error al crear:', error);
          this.showToastMessage('Error al crear profesor', 'error');
        }
      });
    }
  }

  deleteProfesor(id: number): void {
    if (confirm('¿Está seguro de que desea eliminar este profesor?')) {
      this.profesorService.delete(id).subscribe({
        next: (response) => {
          this.showToastMessage(response.message, 'success');
          this.loadProfesores();
        },
        error: (error) => {
          console.error('Error al eliminar:', error);
          const message = error.error?.message || 'Error al eliminar profesor';
          this.showToastMessage(message, 'error');
        }
      });
    }
  }

  showToastMessage(message: string, type: 'success' | 'error' | 'info'): void {
    this.toastMessage = message;
    this.toastType = type;
    this.showToast = true;
    setTimeout(() => {
      this.showToast = false;
    }, 4000);
  }

  closeToast(): void {
    this.showToast = false;
  }
}
