import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Department } from '../models/department';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {
  getAll(): Observable<Department[]> {
  return this.http.get<Department[]>(this.apiUrl);
}

  private apiUrl =
    'https://localhost:7072/api/Department';

  constructor(private http: HttpClient) {}

  getAllDepartments(): Observable<Department[]> {
    return this.http.get<Department[]>(this.apiUrl);
  }

  createDepartment(data: any): Observable<any> {
    return this.http.post(this.apiUrl, data);
  }

  updateDepartment(data: any): Observable<any> {
    return this.http.put(this.apiUrl, data);
  }

  deleteDepartment(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}