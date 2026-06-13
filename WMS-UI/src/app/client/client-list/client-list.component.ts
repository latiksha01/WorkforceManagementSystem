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
import { Client } from '../../core/models/client';
import { ClientService } from '../../core/services/client.service';
import { ClientFormComponent } from '../client-form/client-form.component';

@Component({
  selector: 'app-client-list', standalone: true,
  imports: [CommonModule,MatTableModule,MatPaginatorModule,MatSortModule,MatFormFieldModule,
    MatInputModule,MatButtonModule,MatIconModule,MatDialogModule,MatSnackBarModule,
    MatProgressSpinnerModule,MatTooltipModule],
  templateUrl: './client-list.component.html', styleUrl: './client-list.component.css'
})
export class ClientListComponent implements OnInit {
  cols = ['clientId','clientName','clientLocation','clientPhoneNumber','status','actions'];
  dataSource = new MatTableDataSource<Client>();
  loading = true;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private svc: ClientService, private dialog: MatDialog, private sb: MatSnackBar) {}
  ngOnInit() { this.load(); }

  load() {
    this.loading = true;
    this.svc.getAll().subscribe({
      next: d => { this.dataSource.data = d; setTimeout(() => { this.dataSource.paginator = this.paginator; this.dataSource.sort = this.sort; }); this.loading = false; },
      error: () => this.loading = false
    });
  }
  applyFilter(e: Event) { this.dataSource.filter = (e.target as HTMLInputElement).value.trim().toLowerCase(); }
  openDialog(client?: Client) {
    this.dialog.open(ClientFormComponent, { width: '520px', data: client || null })
      .afterClosed().subscribe(r => { if (r) this.load(); });
  }
  delete(client: Client) {
    if (!confirm(`Delete "${client.clientName}"?`)) return;
    this.svc.delete(client.clientId).subscribe({
      next: () => { this.sb.open('Client deleted', 'Close', { duration: 3000, panelClass: ['snack-success'] }); this.load(); },
      error: () => this.sb.open('Delete failed', 'Close', { duration: 3000, panelClass: ['snack-error'] })
    });
  }
}
