import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Announcement } from '../models/announcement';

@Injectable({ providedIn: 'root' })
export class AnnouncementService {
  private api = 'https://localhost:7072/api/Announcement';
  constructor(private http: HttpClient) {}
  getAll(): Observable<Announcement[]> { return this.http.get<Announcement[]>(this.api); }
  getById(id: number): Observable<Announcement> { return this.http.get<Announcement>(`${this.api}/${id}`); }
  create(dto: any): Observable<any> { return this.http.post(this.api, dto); }
  update(dto: any): Observable<any> { return this.http.put(this.api, dto); }
  delete(id: number): Observable<any> { return this.http.delete(`${this.api}/${id}`); }
}