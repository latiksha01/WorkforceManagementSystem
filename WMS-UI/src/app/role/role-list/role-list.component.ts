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
import { Role } from '../../core/models/role';
import { RoleService } from '../../core/services/role.service';
import { RoleFormComponent } from '../role-form/role-form.component';

@Component({
  selector: 'app-role-list', standalone: true,
  imports: [CommonModule,MatTableModule,MatPaginatorModule,MatSortModule,MatFormFieldModule,
    MatInputModule,MatButtonModule,MatIconModule,MatDialogModule,MatSnackBarModule,
    MatProgressSpinnerModule,MatTooltipModule],
  templateUrl: './role-list.component.html', styleUrl: './role-list.component.css'
})
export class RoleListComponent implements OnInit {
  cols = ['roleId','roleName','description','createdOn','actions'];
  dataSource = new MatTableDataSource<Role>();
  loading = true;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private svc: RoleService, private dialog: MatDialog, private sb: MatSnackBar) {}
  ngOnInit() { this.load(); }

  load() {
    this.loading = true;
    this.svc.getAll().subscribe({
      next: d => { this.dataSource.data = d; setTimeout(() => { this.dataSource.paginator = this.paginator; this.dataSource.sort = this.sort; }); this.loading = false; },
      error: () => this.loading = false
    });
  }
  applyFilter(e: Event) { this.dataSource.filter = (e.target as HTMLInputElement).value.trim().toLowerCase(); }
  openDialog(role?: Role) {
    this.dialog.open(RoleFormComponent, { width: '480px', data: role || null })
      .afterClosed().subscribe(r => { if (r) this.load(); });
  }
  delete(role: Role) {
    if (!confirm(`Delete "${role.roleName}"?`)) return;
    this.svc.delete(role.roleId).subscribe({
      next: () => { this.sb.open('Role deleted', 'Close', { duration: 3000, panelClass: ['snack-success'] }); this.load(); },
      error: () => this.sb.open('Delete failed', 'Close', { duration: 3000, panelClass: ['snack-error'] })
    });
  }
}