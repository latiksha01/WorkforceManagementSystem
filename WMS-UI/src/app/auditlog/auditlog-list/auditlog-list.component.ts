import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { MatPaginatorModule, MatPaginator } from '@angular/material/paginator';
import { MatSortModule, MatSort } from '@angular/material/sort';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { AuditLog } from '../../core/models/audit-log';
import { AuditLogService } from '../../core/services/auditlog.service';

@Component({
  selector: 'app-auditlog-list', standalone: true,
  imports: [CommonModule, MatTableModule, MatPaginatorModule, MatSortModule, MatFormFieldModule,
    MatInputModule, MatSelectModule, MatIconModule, MatProgressSpinnerModule],
  templateUrl: './auditlog-list.component.html', styleUrl: './auditlog-list.component.css'
})
export class AuditlogListComponent implements OnInit {
  cols = ['auditId','entityName','recordId','action','createdByName','createdOn'];
  dataSource = new MatTableDataSource<AuditLog>();
  allLogs: AuditLog[] = [];
  loading = true;
  selectedAction: string | null = null;
  searchTerm = '';
  actionTypes = ['Create', 'Update', 'Delete'];

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private svc: AuditLogService) {}
  ngOnInit() { this.load(); }

  load() {
    this.loading = true;
    this.svc.getAll().subscribe({
      next: d => {
        this.allLogs = d.sort((a, b) => new Date(b.createdOn).getTime() - new Date(a.createdOn).getTime());
        this.dataSource.data = this.allLogs;
        setTimeout(() => { this.dataSource.paginator = this.paginator; this.dataSource.sort = this.sort; });
        this.loading = false;
      },
      error: () => this.loading = false
    });
  }

  applyFilter(e: Event) {
    this.searchTerm = (e.target as HTMLInputElement).value.trim().toLowerCase();
    this.filterByAction();
  }

  filterByAction() {
    let result = this.allLogs;
    if (this.searchTerm) {
      result = result.filter(l =>
        l.entityName.toLowerCase().includes(this.searchTerm) ||
        l.action.toLowerCase().includes(this.searchTerm) ||
        l.createdByName.toLowerCase().includes(this.searchTerm)
      );
    }
    if (this.selectedAction) {
      result = result.filter(l => l.action === this.selectedAction);
    }
    this.dataSource.data = result;
    this.dataSource.paginator?.firstPage();
  }
}
