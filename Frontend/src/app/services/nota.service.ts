import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Nota, CreateNota } from '../models/nota.model';

@Injectable({
  providedIn: 'root'
})
export class NotaService {
  private apiUrl = 'http://localhost:5009/api/Notas';

  constructor(private http: HttpClient) {}

  getAll(orderBy?: string, filterBy?: string, filterValue?: string): Observable<Nota[]> {
    let params = new HttpParams();
    if (orderBy) params = params.set('orderBy', orderBy);
    if (filterBy && filterValue) {
      params = params.set('filterBy', filterBy);
      params = params.set('filterValue', filterValue);
    }
    return this.http.get<Nota[]>(this.apiUrl, { params });
  }

  getById(id: number): Observable<Nota> {
    return this.http.get<Nota>(`${this.apiUrl}/${id}`);
  }

  create(nota: CreateNota): Observable<Nota> {
    return this.http.post<Nota>(this.apiUrl, nota);
  }

  update(id: number, nota: Nota): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, nota);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
