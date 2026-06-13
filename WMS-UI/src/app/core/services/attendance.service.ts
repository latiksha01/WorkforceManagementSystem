import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Attendance } from '../models/attendance';

@Injectable({ providedIn: 'root' })
export class AttendanceService {
  private api = 'https://localhost:7072/api/Attendance';
  constructor(private http: HttpClient) {}
  getAll(): Observable<Attendance[]> { return this.http.get<Attendance[]>(this.api); }
  getById(id: number): Observable<Attendance> { return this.http.get<Attendance>(`${this.api}/${id}`); }
  create(dto: any): Observable<any> { return this.http.post(this.api, dto); }
  update(dto: any): Observable<any> { return this.http.put(this.api, dto); }
  delete(id: number): Observable<any> { return this.http.delete(`${this.api}/${id}`); }
}