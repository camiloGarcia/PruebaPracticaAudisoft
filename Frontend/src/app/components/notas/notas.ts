import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NotaService } from '../../services/nota.service';
import { EstudianteService } from '../../services/estudiante.service';
import { ProfesorService } from '../../services/profesor.service';
import { Nota, CreateNota } from '../../models/nota.model';
import { Estudiante } from '../../models/estudiante.model';
import { Profesor } from '../../models/profesor.model';
import { PagedResult } from '../../models/common.model';

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
  
  currentPage = 1;
  pageSize = 10;
  totalItems = 0;
  totalPages = 0;
  
  showToast = false;
  toastMessage = '';
  toastType: 'success' | 'error' | 'info' = 'success';
  
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
    this.notaService.getAll(this.currentPage, this.pageSize).subscribe({
      next: (result: PagedResult<Nota>) => {
        this.notas = result.data;
        this.currentPage = result.currentPage;
        this.pageSize = result.pageSize;
        this.totalItems = result.totalItems;
        this.totalPages = result.totalPages;
      },
      error: (error) => {
        console.error('Error al cargar notas:', error);
        this.showToastMessage('Error al cargar notas', 'error');
      }
    });
  }

  loadEstudiantes(): void {
    this.estudianteService.getAll(1, 1000).subscribe({
      next: (result: PagedResult<Estudiante>) => {
        this.estudiantes = result.data;
      },
      error: (error) => console.error('Error al cargar estudiantes:', error)
    });
  }

  loadProfesores(): void {
    this.profesorService.getAll(1, 1000).subscribe({
      next: (result: PagedResult<Profesor>) => {
        this.profesores = result.data;
      },
      error: (error) => console.error('Error al cargar profesores:', error)
    });
  }

  filterNotas(): void {
    if (this.filterValue.trim()) {
      this.notaService.getAll(this.currentPage, this.pageSize, undefined, 'nombre', this.filterValue).subscribe({
        next: (result: PagedResult<Nota>) => {
          this.notas = result.data;
          this.currentPage = result.currentPage;
          this.pageSize = result.pageSize;
          this.totalItems = result.totalItems;
          this.totalPages = result.totalPages;
        },
        error: (error) => {
          console.error('Error al filtrar:', error);
          this.showToastMessage('Error al filtrar notas', 'error');
        }
      });
    } else {
      this.loadNotas();
    }
  }

  goToPage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      if (this.filterValue.trim()) {
        this.filterNotas();
      } else {
        this.loadNotas();
      }
    }
  }

  get pages(): number[] {
    return Array.from({ length: this.totalPages }, (_, i) => i + 1);
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
      this.showToastMessage('Todos los campos son requeridos', 'error');
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
          this.showToastMessage('Nota actualizada exitosamente', 'success');
          this.loadNotas();
          this.closeForm();
        },
        error: (error) => {
          console.error('Error al actualizar:', error);
          this.showToastMessage('Error al actualizar nota', 'error');
        }
      });
    } else {
      this.notaService.create(this.formData).subscribe({
        next: () => {
          this.showToastMessage('Nota creada exitosamente', 'success');
          this.loadNotas();
          this.closeForm();
        },
        error: (error) => {
          console.error('Error al crear:', error);
          this.showToastMessage('Error al crear nota', 'error');
        }
      });
    }
  }

  deleteNota(id: number): void {
    if (confirm('¿Está seguro de que desea eliminar esta nota?')) {
      this.notaService.delete(id).subscribe({
        next: () => {
          this.showToastMessage('Nota eliminada exitosamente', 'success');
          this.loadNotas();
        },
        error: (error) => {
          console.error('Error al eliminar:', error);
          this.showToastMessage('Error al eliminar nota', 'error');
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
