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
import { Attendance } from '../../core/models/attendance';
import { AttendanceService } from '../../core/services/attendance.service';
import { AttendanceFormComponent } from '../attendance-form/attendance-form.component';

@Component({
  selector: 'app-attendance-list', standalone: true,
  imports: [CommonModule,MatTableModule,MatPaginatorModule,MatSortModule,MatFormFieldModule,
    MatInputModule,MatButtonModule,MatIconModule,MatDialogModule,MatSnackBarModule,
    MatProgressSpinnerModule,MatTooltipModule],
  templateUrl: './attendance-list.component.html', styleUrl: './attendance-list.component.css'
})
export class AttendanceListComponent implements OnInit {
  cols = ['attendanceId','employeeName','attendanceDate','checkIn','checkOut','totalHours','workMode','actions'];
  dataSource = new MatTableDataSource<Attendance>();
  loading = true;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private svc: AttendanceService, private dialog: MatDialog, private sb: MatSnackBar) {}
  ngOnInit() { this.load(); }

  load() {
    this.loading = true;
    this.svc.getAll().subscribe({
      next: d => { this.dataSource.data = d; setTimeout(() => { this.dataSource.paginator = this.paginator; this.dataSource.sort = this.sort; }); this.loading = false; },
      error: () => this.loading = false
    });
  }
  applyFilter(e: Event) { this.dataSource.filter = (e.target as HTMLInputElement).value.trim().toLowerCase(); }

  openDialog(item?: Attendance) {
    this.dialog.open(AttendanceFormComponent, { width: '560px', data: { record: item || null, mode: 'manual' } })
      .afterClosed().subscribe(r => { if (r) this.load(); });
  }

  checkIn() {
    this.dialog.open(AttendanceFormComponent, { width: '560px', data: { record: null, mode: 'checkin' } })
      .afterClosed().subscribe(r => { if (r) this.load(); });
  }

  checkOut() {
    this.dialog.open(AttendanceFormComponent, { width: '560px', data: { record: null, mode: 'checkout' } })
      .afterClosed().subscribe(r => { if (r) this.load(); });
  }

  delete(item: Attendance) {
    if (!confirm(`Delete attendance record for "${item.employeeName}"?`)) return;
    this.svc.delete(item.attendanceId).subscribe({
      next: () => { this.sb.open('Record deleted', 'Close', { duration: 3000, panelClass: ['snack-success'] }); this.load(); },
      error: () => this.sb.open('Delete failed', 'Close', { duration: 3000, panelClass: ['snack-error'] })
    });
  }
}
