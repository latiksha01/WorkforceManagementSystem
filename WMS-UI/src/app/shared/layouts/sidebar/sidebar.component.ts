import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive, MatIconModule],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {

  constructor(public authService: AuthService) {}

  get role(): string {
    return this.authService.getRole();
  }

  mainItems = [
  { label: 'Dashboard', icon: 'dashboard', route: '/dashboard' }
];

managerItems = [
  { label: 'Employees', icon: 'people_alt', route: '/employee' },
  { label: 'Clients', icon: 'business', route: '/client' },
  { label: 'Projects', icon: 'work', route: '/project' },
  { label: 'Allocations', icon: 'assignment_ind', route: '/allocation' },
  { label: 'Attendance', icon: 'event_available', route: '/attendance' },
  { label: 'Leave', icon: 'beach_access', route: '/leave' },
  { label: 'Announcements', icon: 'campaign', route: '/announcement' }
];

adminItems = [
  { label: 'Departments', icon: 'corporate_fare', route: '/department' },
  { label: 'Roles', icon: 'badge', route: '/role' },
  { label: 'Audit Logs', icon: 'manage_search', route: '/auditlog' }
];
}