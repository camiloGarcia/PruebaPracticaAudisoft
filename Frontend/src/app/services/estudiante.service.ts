import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Estudiante, CreateEstudiante } from '../models/estudiante.model';

@Injectable({
  providedIn: 'root'
})
export class EstudianteService {
  private apiUrl = 'http://localhost:5009/api/Estudiantes';

  constructor(private http: HttpClient) {}

  getAll(orderBy?: string, filterBy?: string, filterValue?: string): Observable<Estudiante[]> {
    let params = new HttpParams();
    if (orderBy) params = params.set('orderBy', orderBy);
    if (filterBy && filterValue) {
      params = params.set('filterBy', filterBy);
      params = params.set('filterValue', filterValue);
    }
    return this.http.get<Estudiante[]>(this.apiUrl, { params });
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

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
