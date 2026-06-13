import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { MatPaginatorModule, MatPaginator } from '@angular/material/paginator';
import { MatSortModule, MatSort } from '@angular/material/sort';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTooltipModule } from '@angular/material/tooltip';
import { Employee } from '../../core/models/employee';
import { Department } from '../../core/models/department';
import { Role } from '../../core/models/role';
import { EmployeeService } from '../../core/services/employee.service';
import { DepartmentService } from '../../core/services/department.service';
import { RoleService } from '../../core/services/role.service';
import { EmployeeFormComponent } from '../employee-form/employee-form.component';

@Component({
  selector: 'app-employee-list', standalone: true,
  imports: [CommonModule,MatTableModule,MatPaginatorModule,MatSortModule,MatFormFieldModule,
    MatInputModule,MatSelectModule,MatButtonModule,MatIconModule,MatDialogModule,MatSnackBarModule,
    MatProgressSpinnerModule,MatTooltipModule],
  templateUrl: './employee-list.component.html', styleUrl: './employee-list.component.css'
})
export class EmployeeListComponent implements OnInit {
  cols = ['employeeId','name','departmentName','roleName','phoneNumber','status','actions'];
  dataSource = new MatTableDataSource<Employee>();
  allEmployees: Employee[] = [];
  loading = true;

  departments: Department[] = [];
  roles: Role[] = [];
  selectedDept: number | null = null;
  selectedRole: number | null = null;
  searchTerm = '';

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private svc: EmployeeService,
    private deptSvc: DepartmentService,
    private roleSvc: RoleService,
    private dialog: MatDialog,
    private sb: MatSnackBar
  ) {}

  ngOnInit() {
    this.deptSvc.getAll().subscribe(d => this.departments = d);
    this.roleSvc.getAll().subscribe(r => this.roles = r);
    this.load();
  }

  load() {
    this.loading = true;
    this.svc.getAll().subscribe({
      next: d => {
        this.allEmployees = d;
        this.dataSource.data = d;
        setTimeout(() => { this.dataSource.paginator = this.paginator; this.dataSource.sort = this.sort; });
        this.loading = false;
      },
      error: () => this.loading = false
    });
  }

  applyFilter(e: Event) {
    this.searchTerm = (e.target as HTMLInputElement).value.trim().toLowerCase();
    this.applyFilters();
  }

  applyFilters() {
    let result = this.allEmployees;

    if (this.searchTerm) {
      result = result.filter(emp =>
        `${emp.firstName} ${emp.lastName}`.toLowerCase().includes(this.searchTerm) ||
        emp.email.toLowerCase().includes(this.searchTerm)
      );
    }
    if (this.selectedDept) {
      result = result.filter(emp => emp.departmentId === this.selectedDept);
    }
    if (this.selectedRole) {
      result = result.filter(emp => emp.roleId === this.selectedRole);
    }

    this.dataSource.data = result;
    this.dataSource.paginator?.firstPage();
  }

  openDialog(emp?: Employee) {
    this.dialog.open(EmployeeFormComponent, { width: '640px', data: emp || null, maxHeight: '90vh' })
      .afterClosed().subscribe(r => { if (r) this.load(); });
  }

  viewEmployee(emp: Employee) {
    this.dialog.open(EmployeeFormComponent, { width: '640px', data: emp, maxHeight: '90vh', autoFocus: false }).afterClosed().subscribe(r => { if (r) this.load(); });
  }

  delete(emp: Employee) {
    if (!confirm(`Delete employee "${emp.firstName} ${emp.lastName}"?`)) return;
    this.svc.delete(emp.employeeId).subscribe({
      next: () => { this.sb.open('Employee deleted', 'Close', { duration: 3000, panelClass: ['snack-success'] }); this.load(); },
      error: () => this.sb.open('Delete failed', 'Close', { duration: 3000, panelClass: ['snack-error'] })
    });
  }
}
