import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Leave } from '../models/leave';

@Injectable({ providedIn: 'root' })
export class LeaveService {
  private api = 'https://localhost:7072/api/Leave';
  constructor(private http: HttpClient) {}
  getAll(): Observable<Leave[]> { return this.http.get<Leave[]>(this.api); }
  getById(id: number): Observable<Leave> { return this.http.get<Leave>(`${this.api}/${id}`); }
  create(dto: any): Observable<any> { return this.http.post(this.api, dto); }
  update(dto: any): Observable<any> { return this.http.put(this.api, dto); }
  delete(id: number): Observable<any> { return this.http.delete(`${this.api}/${id}`); }
}