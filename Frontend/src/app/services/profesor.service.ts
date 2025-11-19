import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Profesor, CreateProfesor } from '../models/profesor.model';

@Injectable({
  providedIn: 'root'
})
export class ProfesorService {
  private apiUrl = 'http://localhost:5009/api/Profesores';

  constructor(private http: HttpClient) {}

  getAll(orderBy?: string, filterBy?: string, filterValue?: string): Observable<Profesor[]> {
    let params = new HttpParams();
    if (orderBy) params = params.set('orderBy', orderBy);
    if (filterBy && filterValue) {
      params = params.set('filterBy', filterBy);
      params = params.set('filterValue', filterValue);
    }
    return this.http.get<Profesor[]>(this.apiUrl, { params });
  }

  getById(id: number): Observable<Profesor> {
    return this.http.get<Profesor>(`${this.apiUrl}/${id}`);
  }

  create(profesor: CreateProfesor): Observable<Profesor> {
    return this.http.post<Profesor>(this.apiUrl, profesor);
  }

  update(id: number, profesor: Profesor): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, profesor);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
