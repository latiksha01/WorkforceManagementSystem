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
import { Allocation } from '../../core/models/allocation';
import { AllocationService } from '../../core/services/allocation.service';
import { AllocationFormComponent } from '../allocation-form/allocation-form.component';

@Component({
  selector: 'app-allocation-list', standalone: true,
  imports: [CommonModule,MatTableModule,MatPaginatorModule,MatSortModule,MatFormFieldModule,
    MatInputModule,MatButtonModule,MatIconModule,MatDialogModule,MatSnackBarModule,
    MatProgressSpinnerModule,MatTooltipModule],
  templateUrl: './allocation-list.component.html', styleUrl: './allocation-list.component.css'
})
export class AllocationListComponent implements OnInit {
  cols = ['allocationId','employeeName','projectName','assignedOn','status','actions'];
  dataSource = new MatTableDataSource<Allocation>();
  loading = true;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private svc: AllocationService, private dialog: MatDialog, private sb: MatSnackBar) {}
  ngOnInit() { this.load(); }

  load() {
    this.loading = true;
    this.svc.getAll().subscribe({
      next: d => { this.dataSource.data = d; setTimeout(() => { this.dataSource.paginator = this.paginator; this.dataSource.sort = this.sort; }); this.loading = false; },
      error: () => this.loading = false
    });
  }
  applyFilter(e: Event) { this.dataSource.filter = (e.target as HTMLInputElement).value.trim().toLowerCase(); }
  openDialog(item?: Allocation) {
    this.dialog.open(AllocationFormComponent, { width: '520px', data: item || null })
      .afterClosed().subscribe(r => { if (r) this.load(); });
  }
  delete(item: Allocation) {
    if (!confirm(`Remove "${item.employeeName}" from "${item.projectName}"?`)) return;
    this.svc.delete(item.allocationId).subscribe({
      next: () => { this.sb.open('Allocation removed', 'Close', { duration: 3000, panelClass: ['snack-success'] }); this.load(); },
      error: () => this.sb.open('Delete failed', 'Close', { duration: 3000, panelClass: ['snack-error'] })
    });
  }
}
