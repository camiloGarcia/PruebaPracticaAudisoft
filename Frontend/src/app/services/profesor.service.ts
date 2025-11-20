import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Profesor, CreateProfesor } from '../models/profesor.model';
import { PagedResult } from '../models/common.model';

@Injectable({
  providedIn: 'root'
})
export class ProfesorService {
  private apiUrl = 'http://localhost:5009/api/Profesores';

  constructor(private http: HttpClient) {}

  getAll(page: number = 1, pageSize: number = 10, orderBy?: string, filterBy?: string, filterValue?: string): Observable<PagedResult<Profesor>> {
    let params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());
    if (orderBy) params = params.set('orderBy', orderBy);
    if (filterBy && filterValue) {
      params = params.set('filterBy', filterBy);
      params = params.set('filterValue', filterValue);
    }
    return this.http.get<PagedResult<Profesor>>(this.apiUrl, { params });
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

  delete(id: number): Observable<{ message: string }> {
    return this.http.delete<{ message: string }>(`${this.apiUrl}/${id}`);
  }
}
