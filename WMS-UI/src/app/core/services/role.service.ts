import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Role } from '../models/role';

@Injectable({ providedIn: 'root' })
export class RoleService {
  private api = 'https://localhost:7072/api/Role';
  constructor(private http: HttpClient) {}
  getAll(): Observable<Role[]> { return this.http.get<Role[]>(this.api); }
  getById(id: number): Observable<Role> { return this.http.get<Role>(`${this.api}/${id}`); }
  create(dto: any): Observable<any> { return this.http.post(this.api, dto); }
  update(dto: any): Observable<any> { return this.http.put(this.api, dto); }
  delete(id: number): Observable<any> { return this.http.delete(`${this.api}/${id}`); }
}