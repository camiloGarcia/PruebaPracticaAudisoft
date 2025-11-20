import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { EstudianteService } from '../../services/estudiante.service';
import { Estudiante, CreateEstudiante } from '../../models/estudiante.model';
import { PagedResult } from '../../models/common.model';

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
  
  // Paginación
  currentPage = 1;
  pageSize = 10;
  totalItems = 0;
  totalPages = 0;
  
  // Mensajes toast
  showToast = false;
  toastMessage = '';
  toastType: 'success' | 'error' | 'info' = 'success';
  
  formData: CreateEstudiante = {
    nombre: ''
  };

  constructor(private estudianteService: EstudianteService) {}

  ngOnInit(): void {
    this.loadEstudiantes();
  }

  loadEstudiantes(): void {
    this.estudianteService.getAll(this.currentPage, this.pageSize).subscribe({
      next: (result: PagedResult<Estudiante>) => {
        this.estudiantes = result.data;
        this.currentPage = result.currentPage;
        this.pageSize = result.pageSize;
        this.totalItems = result.totalItems;
        this.totalPages = result.totalPages;
      },
      error: (error) => {
        console.error('Error al cargar estudiantes:', error);
        this.showToastMessage('Error al cargar estudiantes', 'error');
      }
    });
  }

  filterEstudiantes(): void {
    if (this.filterValue.trim()) {
      this.estudianteService.getAll(this.currentPage, this.pageSize, undefined, 'nombre', this.filterValue).subscribe({
        next: (result: PagedResult<Estudiante>) => {
          this.estudiantes = result.data;
          this.currentPage = result.currentPage;
          this.pageSize = result.pageSize;
          this.totalItems = result.totalItems;
          this.totalPages = result.totalPages;
        },
        error: (error) => {
          console.error('Error al filtrar:', error);
          this.showToastMessage('Error al filtrar estudiantes', 'error');
        }
      });
    } else {
      this.loadEstudiantes();
    }
  }

  goToPage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      if (this.filterValue.trim()) {
        this.filterEstudiantes();
      } else {
        this.loadEstudiantes();
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
      this.showToastMessage('El nombre es requerido', 'error');
      return;
    }

    if (this.isEditing && this.selectedEstudiante) {
      const updatedEstudiante: Estudiante = {
        id: this.selectedEstudiante.id,
        nombre: this.formData.nombre
      };
      this.estudianteService.update(this.selectedEstudiante.id, updatedEstudiante).subscribe({
        next: () => {
          this.showToastMessage('Estudiante actualizado exitosamente', 'success');
          this.loadEstudiantes();
          this.closeForm();
        },
        error: (error) => {
          console.error('Error al actualizar:', error);
          this.showToastMessage('Error al actualizar estudiante', 'error');
        }
      });
    } else {
      this.estudianteService.create(this.formData).subscribe({
        next: () => {
          this.showToastMessage('Estudiante creado exitosamente', 'success');
          this.loadEstudiantes();
          this.closeForm();
        },
        error: (error) => {
          console.error('Error al crear:', error);
          this.showToastMessage('Error al crear estudiante', 'error');
        }
      });
    }
  }

  deleteEstudiante(id: number): void {
    if (confirm('¿Está seguro de que desea eliminar este estudiante?')) {
      this.estudianteService.delete(id).subscribe({
        next: (response) => {
          this.showToastMessage(response.message, 'success');
          this.loadEstudiantes();
        },
        error: (error) => {
          console.error('Error al eliminar:', error);
          const message = error.error?.message || 'Error al eliminar estudiante';
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
