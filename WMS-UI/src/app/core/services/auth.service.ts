import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { LoginRequest } from '../models/login-request';
import { LoginResponse } from '../models/login-response';

@Injectable({ providedIn: 'root' })
export class AuthService {

  private apiUrl = 'https://localhost:7072/api/Auth';

  constructor(private http: HttpClient) {}

  login(data: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.apiUrl}/login`, data).pipe(
      tap(response => {
        // Store token AND user info
        localStorage.setItem('token', response.token);
localStorage.setItem('wms_username', response.username);
localStorage.setItem('wms_role', response.role);
localStorage.setItem('employeeId', response.employeeId.toString());
      })
    );
  }

  changePassword(data: any): Observable<any> {
  return this.http.post(
    `${this.apiUrl}/change-password`,
    data
  );
}

  saveToken(token: string): void {
    localStorage.setItem('token', token);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  getUsername(): string {
    return localStorage.getItem('wms_username') || 'User';
  }

  getEmployeeId(): number {
  return Number(localStorage.getItem('employeeId'));
}

  getRole(): string {
    return localStorage.getItem('wms_role') || '';
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('wms_username');
    localStorage.removeItem('wms_role');
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }
}
