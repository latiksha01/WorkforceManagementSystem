import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Employee } from '../models/employee';

@Injectable({ providedIn: 'root' })
export class EmployeeService {
  private api = 'https://localhost:7072/api/Employee';
  constructor(private http: HttpClient) {}
  getAll(): Observable<Employee[]> { return this.http.get<Employee[]>(this.api); }
  getById(id: number): Observable<Employee> { return this.http.get<Employee>(`${this.api}/${id}`); }
  create(dto: any): Observable<any> { return this.http.post(this.api, dto); }
  update(dto: any): Observable<any> { return this.http.put(this.api, dto); }
  delete(id: number): Observable<any> { return this.http.delete(`${this.api}/${id}`); }
}