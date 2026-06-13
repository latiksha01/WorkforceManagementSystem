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
import { Project } from '../../core/models/project';
import { ProjectService } from '../../core/services/project.service';
import { ProjectFormComponent } from '../project-form/project-form.component';

@Component({
  selector: 'app-project-list', standalone: true,
  imports: [CommonModule,MatTableModule,MatPaginatorModule,MatSortModule,MatFormFieldModule,
    MatInputModule,MatButtonModule,MatIconModule,MatDialogModule,MatSnackBarModule,
    MatProgressSpinnerModule,MatTooltipModule],
  templateUrl: './project-list.component.html', styleUrl: './project-list.component.css'
})
export class ProjectListComponent implements OnInit {
  cols = ['projectId','projectName','clientName','startDate','endDate','status','actions'];
  dataSource = new MatTableDataSource<Project>();
  loading = true;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private svc: ProjectService, private dialog: MatDialog, private sb: MatSnackBar) {}
  ngOnInit() { this.load(); }

  load() {
    this.loading = true;
    this.svc.getAll().subscribe({
      next: d => { this.dataSource.data = d; setTimeout(() => { this.dataSource.paginator = this.paginator; this.dataSource.sort = this.sort; }); this.loading = false; },
      error: () => this.loading = false
    });
  }
  applyFilter(e: Event) { this.dataSource.filter = (e.target as HTMLInputElement).value.trim().toLowerCase(); }
  openDialog(project?: Project) {
    this.dialog.open(ProjectFormComponent, { width: '560px', data: project || null })
      .afterClosed().subscribe(r => { if (r) this.load(); });
  }
  delete(project: Project) {
    if (!confirm(`Delete "${project.projectName}"?`)) return;
    this.svc.delete(project.projectId).subscribe({
      next: () => { this.sb.open('Project deleted', 'Close', { duration: 3000, panelClass: ['snack-success'] }); this.load(); },
      error: () => this.sb.open('Delete failed', 'Close', { duration: 3000, panelClass: ['snack-error'] })
    });
  }
}
