import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { forkJoin } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class DashboardService {
  private apiUrl = 'https://localhost:7072/api';

  constructor(private http: HttpClient) {}

  getDashboardData() {
  return forkJoin({
    employees: this.http.get<any[]>(`${this.apiUrl}/Employee`),
    departments: this.http.get<any[]>(`${this.apiUrl}/Department`),
    projects: this.http.get<any[]>(`${this.apiUrl}/Project`),
    clients: this.http.get<any[]>(`${this.apiUrl}/Client`),
    leaves: this.http.get<any[]>(`${this.apiUrl}/Leave`),
    announcements: this.http.get<any[]>(`${this.apiUrl}/Announcement`),
    allocations: this.http.get<any[]>(`${this.apiUrl}/EmployeeProjectAllocation`),
    attendance: this.http.get<any[]>(`${this.apiUrl}/Attendance`)
  });
}
}
