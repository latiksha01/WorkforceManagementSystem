import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Allocation } from '../models/allocation';

@Injectable({ providedIn: 'root' })
export class AllocationService {
  private api = 'https://localhost:7072/api/EmployeeProjectAllocation';
  constructor(private http: HttpClient) {}
  getAll(): Observable<Allocation[]> { return this.http.get<Allocation[]>(this.api); }
  getById(id: number): Observable<Allocation> { return this.http.get<Allocation>(`${this.api}/${id}`); }
  create(dto: any): Observable<any> { return this.http.post(this.api, dto); }
  update(dto: any): Observable<any> { return this.http.put(this.api, dto); }
  delete(id: number): Observable<any> { return this.http.delete(`${this.api}/${id}`); }
}