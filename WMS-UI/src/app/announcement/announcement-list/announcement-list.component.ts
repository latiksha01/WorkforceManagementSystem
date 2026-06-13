import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTooltipModule } from '@angular/material/tooltip';
import { Announcement } from '../../core/models/announcement';
import { AnnouncementService } from '../../core/services/announcement.service';
import { AnnouncementFormComponent } from '../announcement-form/announcement-form.component';

@Component({
  selector: 'app-announcement-list', standalone: true,
  imports: [CommonModule, MatButtonModule, MatIconModule, MatDialogModule, MatSnackBarModule, MatProgressSpinnerModule, MatTooltipModule],
  templateUrl: './announcement-list.component.html', styleUrl: './announcement-list.component.css'
})
export class AnnouncementListComponent implements OnInit {
  announcements: Announcement[] = [];
  loading = true;

  constructor(private svc: AnnouncementService, private dialog: MatDialog, private sb: MatSnackBar) {}
  ngOnInit() { this.load(); }

  load() {
    this.loading = true;
    this.svc.getAll().subscribe({
      next: d => {
        this.announcements = d.sort((a, b) => new Date(b.createdOn).getTime() - new Date(a.createdOn).getTime());
        this.loading = false;
      },
      error: () => this.loading = false
    });
  }

  openDialog(item?: Announcement) {
    this.dialog.open(AnnouncementFormComponent, { width: '520px', data: item || null })
      .afterClosed().subscribe(r => { if (r) this.load(); });
  }

  delete(item: Announcement) {
    if (!confirm(`Delete announcement "${item.title}"?`)) return;
    this.svc.delete(item.announcementId).subscribe({
      next: () => { this.sb.open('Announcement deleted', 'Close', { duration: 3000, panelClass: ['snack-success'] }); this.load(); },
      error: () => this.sb.open('Delete failed', 'Close', { duration: 3000, panelClass: ['snack-error'] })
    });
  }
}
