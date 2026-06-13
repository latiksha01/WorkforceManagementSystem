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
import { Leave } from '../../core/models/leave';
import { LeaveService } from '../../core/services/leave.service';
import { LeaveFormComponent } from '../leave-form/leave-form.component';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-leave-list', standalone: true,
  imports: [CommonModule,MatTableModule,MatPaginatorModule,MatSortModule,MatFormFieldModule,
    MatInputModule,MatSelectModule,MatButtonModule,MatIconModule,MatDialogModule,MatSnackBarModule,
    MatProgressSpinnerModule,MatTooltipModule],
  templateUrl: './leave-list.component.html', styleUrl: './leave-list.component.css'
})
export class LeaveListComponent implements OnInit {
  cols = ['leaveId','employeeName','leaveType','fromDate','toDate','status','actions'];
  dataSource = new MatTableDataSource<Leave>();
  allLeaves: Leave[] = [];
  loading = true;
  selectedStatus: string | null = null;
  searchTerm = '';
  get role(): string {
    return this.authService.getRole();
  }

  get isAdminOrManager(): boolean {
    return this.role === 'Admin' || this.role === 'Manager';
  }

  get isEmployee(): boolean {
    return this.role === 'Employee';
  }

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
  private svc: LeaveService,
  private dialog: MatDialog,
  private sb: MatSnackBar,
  private authService: AuthService
) {}
  ngOnInit() { this.load(); }

  load() {
    this.loading = true;
    this.svc.getAll().subscribe({
      next: d => {
        this.allLeaves = d;
        this.dataSource.data = d;
        setTimeout(() => { this.dataSource.paginator = this.paginator; this.dataSource.sort = this.sort; });
        this.loading = false;
      },
      error: () => this.loading = false
    });
  }

  applyFilter(e: Event) {
    this.searchTerm = (e.target as HTMLInputElement).value.trim().toLowerCase();
    this.filterByStatus();
  }

  filterByStatus() {
    let result = this.allLeaves;
    if (this.searchTerm) {
      result = result.filter(l => l.employeeName.toLowerCase().includes(this.searchTerm));
    }
    if (this.selectedStatus) {
      result = result.filter(l => l.status === this.selectedStatus);
    }
    this.dataSource.data = result;
    this.dataSource.paginator?.firstPage();
  }

  openDialog(leave?: Leave) {
    this.dialog.open(LeaveFormComponent, { width: '520px', data: leave || null })
      .afterClosed().subscribe(r => { if (r) this.load(); });
  }

  approve(leave: Leave) {
    this.updateStatus(leave, 'Approved');
  }
  reject(leave: Leave) {
    this.updateStatus(leave, 'Rejected');
  }

  updateStatus(leave: Leave, status: string) {
    const payload: any = { ...leave, status, approvedOn: new Date().toISOString() };
    this.svc.update(payload).subscribe({
      next: () => { this.sb.open(`Leave ${status.toLowerCase()}`, 'Close', { duration: 3000, panelClass: ['snack-success'] }); this.load(); },
      error: () => this.sb.open('Action failed', 'Close', { duration: 3000, panelClass: ['snack-error'] })
    });
  }

  delete(leave: Leave) {
    if (!confirm(`Delete leave request for "${leave.employeeName}"?`)) return;
    this.svc.delete(leave.leaveId).subscribe({
      next: () => { this.sb.open('Leave deleted', 'Close', { duration: 3000, panelClass: ['snack-success'] }); this.load(); },
      error: () => this.sb.open('Delete failed', 'Close', { duration: 3000, panelClass: ['snack-error'] })
    });
  }
}
