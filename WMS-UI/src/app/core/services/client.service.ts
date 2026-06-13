import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Client } from '../models/client';

@Injectable({ providedIn: 'root' })
export class ClientService {
  private api = 'https://localhost:7072/api/Client';
  constructor(private http: HttpClient) {}
  getAll(): Observable<Client[]> { return this.http.get<Client[]>(this.api); }
  getById(id: number): Observable<Client> { return this.http.get<Client>(`${this.api}/${id}`); }
  create(dto: any): Observable<any> { return this.http.post(this.api, dto); }
  update(dto: any): Observable<any> { return this.http.put(this.api, dto); }
  delete(id: number): Observable<any> { return this.http.delete(`${this.api}/${id}`); }
}