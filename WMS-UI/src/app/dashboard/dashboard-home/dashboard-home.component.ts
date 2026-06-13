import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatIconModule } from '@angular/material/icon';
import { forkJoin } from 'rxjs';
import { DashboardService } from '../../core/services/dashboard.service';
import { AuthService } from '../../core/services/auth.service';
import { RouterLink } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-dashboard-home',
  standalone: true,
  imports: [CommonModule, MatProgressSpinnerModule, MatIconModule, RouterLink, MatButtonModule],
  templateUrl: './dashboard-home.component.html',
  styleUrl: './dashboard-home.component.css'
})
export class DashboardHomeComponent implements OnInit {
  loading = true;
  today = new Date();
  username = '';
  greeting = '';
  role ='';

  myAttendanceToday = false;

  myLeaveStats = {
    approved: 0,
    pending: 0,
    rejected: 0
  };

  myAttendance: any = null;



  myProjectsCount = 0;

  stats = { employees: 0, departments: 0, projects: 0, clients: 0, pendingLeaves: 0 };
  leaveStats = { approved: 0, pending: 0, rejected: 0 };
  announcements: any[] = [];
  recentProjects: any[] = [];
  myProjects: any[] = [];

  constructor(
    private dashboardService: DashboardService,
    private authService: AuthService
  ) {
    this.username = this.authService.getUsername();
    this.role = this.authService.getRole();
    const h = new Date().getHours();
    this.greeting = h < 12 ? 'morning' : h < 17 ? 'afternoon' : 'evening';
  }

  ngOnInit(): void {
    this.dashboardService.getDashboardData().subscribe({
      next: (data) => {

  console.log('Attendance Data:', data.attendance);
  console.log('Leaves:', data.leaves);

  this.stats.employees = data.employees.length;
  this.stats.departments = data.departments.length;
  this.stats.projects = data.projects.length;
  this.stats.clients = data.clients?.length ?? 0;

  this.stats.pendingLeaves =
    data.leaves.filter((l: any) => l.status === 'Pending').length;

  this.leaveStats.approved =
    data.leaves.filter((l: any) => l.status === 'Approved').length;

  this.leaveStats.pending =
    data.leaves.filter((l: any) => l.status === 'Pending').length;

  this.leaveStats.rejected =
    data.leaves.filter((l: any) => l.status === 'Rejected').length;

  this.announcements = (data.announcements || [])
    .sort((a: any, b: any) =>
      new Date(b.createdOn).getTime() -
      new Date(a.createdOn).getTime())
    .slice(0, 5);

  this.recentProjects = (data.projects || [])
    .sort((a: any, b: any) =>
      new Date(b.createdOn).getTime() -
      new Date(a.createdOn).getTime())
    .slice(0, 5);

  const employeeId =
    Number(localStorage.getItem('employeeId'));

  // Employee Attendance
  this.myAttendance =
    data.attendance.find(
      (a: any) => a.employeeId === employeeId
    );

  if (this.isEmployee) {

    // Employee Projects
    this.myProjects =
      (data.allocations || []).filter(
        (a: any) => a.employeeId === employeeId
      );

    this.myProjectsCount =
      this.myProjects.length;

    // Employee Leaves
    const myLeaves =
      (data.leaves || []).filter(
        (l: any) => l.employeeId === employeeId
      );

    this.myLeaveStats.approved =
      myLeaves.filter(
        (l: any) => l.status === 'Approved'
      ).length;

    this.myLeaveStats.pending =
      myLeaves.filter(
        (l: any) => l.status === 'Pending'
      ).length;

    this.myLeaveStats.rejected =
      myLeaves.filter(
        (l: any) => l.status === 'Rejected'
      ).length;

    // Attendance Status
    this.myAttendanceToday =
      data.attendance.some(
        (a: any) => a.employeeId === employeeId
      );
  }

  this.loading = false;
},
      error: () => { this.loading = false; }
    });
  }

  getPercent(val: number): number {
    const total = this.leaveStats.approved + this.leaveStats.pending + this.leaveStats.rejected;
    return total === 0 ? 0 : Math.round((val / total) * 100);
  }
  get isAdmin(): boolean {
  return this.role === 'Admin';
}

get isManager(): boolean {
  return this.role === 'Manager';
}

get isEmployee(): boolean {
  return this.role === 'Employee';
}
}
