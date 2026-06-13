import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Project } from '../models/project';

@Injectable({ providedIn: 'root' })
export class ProjectService {
  private api = 'https://localhost:7072/api/Project';
  constructor(private http: HttpClient) {}
  getAll(): Observable<Project[]> { return this.http.get<Project[]>(this.api); }
  getById(id: number): Observable<Project> { return this.http.get<Project>(`${this.api}/${id}`); }
  create(dto: any): Observable<any> { return this.http.post(this.api, dto); }
  update(dto: any): Observable<any> { return this.http.put(this.api, dto); }
  delete(id: number): Observable<any> { return this.http.delete(`${this.api}/${id}`); }
}