import { Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { MainLayoutComponent } from './shared/layouts/main-layout/main-layout.component';
import { DashboardHomeComponent } from './dashboard/dashboard-home/dashboard-home.component';
import { DepartmentListComponent } from './department/department-list/department-list.component';
import { RoleListComponent } from './role/role-list/role-list.component';
import { EmployeeListComponent } from './employee/employee-list/employee-list.component';
import { ClientListComponent } from './client/client-list/client-list.component';
import { ProjectListComponent } from './project/project-list/project-list.component';
import { AllocationListComponent } from './allocation/allocation-list/allocation-list.component';
import { AttendanceListComponent } from './attendance/attendance-list/attendance-list.component';
import { LeaveListComponent } from './leave/leave-list/leave-list.component';
import { AnnouncementListComponent } from './announcement/announcement-list/announcement-list.component';
import { AuditlogListComponent } from './auditlog/auditlog-list/auditlog-list.component';
import { authGuard } from './core/guards/auth.guard';
import { roleGuard } from './core/guards/role.guard';
import { ChangePasswordComponent } from './auth/change-password/change-password.component';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  {
    path: '',
    component: MainLayoutComponent,
    canActivate: [authGuard],
    children: [
      { path: 'dashboard',    component: DashboardHomeComponent },
      { path: 'employee',     component: EmployeeListComponent, canActivate: [authGuard, roleGuard],data: { roles: ['Admin','Manager'] } },
      { path: 'department',   component: DepartmentListComponent,canActivate: [authGuard, roleGuard],data: { roles: ['Admin'] }},
      { path: 'role',         component: RoleListComponent,canActivate: [authGuard, roleGuard],data: { roles: ['Admin'] } },
      { path: 'client',       component: ClientListComponent, canActivate: [authGuard, roleGuard],data: { roles: ['Admin','Manager'] }  },
      { path: 'project',      component: ProjectListComponent, canActivate: [authGuard, roleGuard],data: { roles: ['Admin','Manager'] }  },
      { path: 'allocation',   component: AllocationListComponent, canActivate: [authGuard, roleGuard],data: { roles: ['Admin','Manager'] }  },
      { path: 'attendance',   component: AttendanceListComponent, canActivate: [authGuard, roleGuard],data: { roles: ['Admin','Manager','Employee'] }  },
      { path: 'leave',        component: LeaveListComponent, canActivate: [authGuard, roleGuard],data: { roles: ['Admin','Manager','Employee'] }  },
      { path: 'announcement', component: AnnouncementListComponent, canActivate: [authGuard, roleGuard],data: { roles: ['Admin','Manager','Employee'] }  },
      { path: 'auditlog',     component: AuditlogListComponent, canActivate: [authGuard, roleGuard],data: { roles: ['Admin'] }  },
      { path: 'change-password', component: ChangePasswordComponent, canActivate: [authGuard]}
    ]
  },

  { path: '**', redirectTo: 'login' }
];