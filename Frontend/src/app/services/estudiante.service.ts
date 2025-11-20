import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Estudiante, CreateEstudiante } from '../models/estudiante.model';
import { PagedResult, OperationResult } from '../models/common.model';

@Injectable({
  providedIn: 'root'
})
export class EstudianteService {
  private apiUrl = 'http://localhost:5009/api/Estudiantes';

  constructor(private http: HttpClient) {}

  getAll(page: number = 1, pageSize: number = 10, orderBy?: string, filterBy?: string, filterValue?: string): Observable<PagedResult<Estudiante>> {
    let params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());
    if (orderBy) params = params.set('orderBy', orderBy);
    if (filterBy && filterValue) {
      params = params.set('filterBy', filterBy);
      params = params.set('filterValue', filterValue);
    }
    return this.http.get<PagedResult<Estudiante>>(this.apiUrl, { params });
  }

  getById(id: number): Observable<Estudiante> {
    return this.http.get<Estudiante>(`${this.apiUrl}/${id}`);
  }

  create(estudiante: CreateEstudiante): Observable<Estudiante> {
    return this.http.post<Estudiante>(this.apiUrl, estudiante);
  }

  update(id: number, estudiante: Estudiante): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, estudiante);
  }

  delete(id: number): Observable<{ message: string }> {
    return this.http.delete<{ message: string }>(`${this.apiUrl}/${id}`);
  }
}
