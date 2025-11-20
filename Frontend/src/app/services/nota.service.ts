import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Nota, CreateNota } from '../models/nota.model';
import { PagedResult } from '../models/common.model';

@Injectable({
  providedIn: 'root'
})
export class NotaService {
  private apiUrl = 'http://localhost:5009/api/Notas';

  constructor(private http: HttpClient) {}

  getAll(page: number = 1, pageSize: number = 10, orderBy?: string, filterBy?: string, filterValue?: string): Observable<PagedResult<Nota>> {
    let params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());
    if (orderBy) params = params.set('orderBy', orderBy);
    if (filterBy && filterValue) {
      params = params.set('filterBy', filterBy);
      params = params.set('filterValue', filterValue);
    }
    return this.http.get<PagedResult<Nota>>(this.apiUrl, { params });
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
