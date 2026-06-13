import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { MatPaginatorModule, MatPaginator } from '@angular/material/paginator';
import { MatSortModule, MatSort } from '@angular/material/sort';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTooltipModule } from '@angular/material/tooltip';

import { Department } from '../../core/models/department';
import { DepartmentService } from '../../core/services/department.service';
import { DepartmentFormComponent } from '../department-form/department-form.component';

@Component({
  selector: 'app-department-list',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule, MatPaginatorModule, MatSortModule,
    MatFormFieldModule, MatInputModule, MatButtonModule,
    MatIconModule, MatDialogModule, MatSnackBarModule,
    MatProgressSpinnerModule, MatTooltipModule
  ],
  templateUrl: './department-list.component.html',
  styleUrl: './department-list.component.css'
})
export class DepartmentListComponent implements OnInit {
  displayedColumns = ['departmentId', 'departmentName', 'description', 'createdOn', 'actions'];
  dataSource = new MatTableDataSource<Department>();
  loading = true;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private deptService: DepartmentService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void { this.load(); }

  load(): void {
    this.loading = true;
    this.deptService.getAllDepartments().subscribe({
      next: (data) => {
        this.dataSource.data = data;
        setTimeout(() => {
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
        });
        this.loading = false;
      },
      error: () => {
        this.notify('Failed to load departments', true);
        this.loading = false;
      }
    });
  }

  applyFilter(e: Event): void {
    this.dataSource.filter = (e.target as HTMLInputElement).value.trim().toLowerCase();
    this.dataSource.paginator?.firstPage();
  }

  openDialog(dept?: Department): void {
    const ref = this.dialog.open(DepartmentFormComponent, {
      width: '480px',
      panelClass: 'wms-dialog',
      data: dept || null
    });
    ref.afterClosed().subscribe(saved => { if (saved) this.load(); });
  }

  deleteDepartment(dept: Department): void {
    if (!confirm(`Delete "${dept.departmentName}"? This cannot be undone.`)) return;
    this.deptService.deleteDepartment(dept.departmentId).subscribe({
      next: () => { this.notify('Department deleted'); this.load(); },
      error: () => this.notify('Delete failed', true)
    });
  }

  notify(msg: string, err = false): void {
    this.snackBar.open(msg, 'Close', {
      duration: 3500,
      panelClass: err ? ['snack-error'] : ['snack-success']
    });
  }
}
