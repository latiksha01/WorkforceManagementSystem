import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { AnnouncementService } from '../../core/services/announcement.service';
import { Announcement } from '../../core/models/announcement';

@Component({
  selector: 'app-announcement-form', standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatDialogModule, MatFormFieldModule, MatInputModule,
    MatButtonModule, MatIconModule, MatSlideToggleModule, MatProgressSpinnerModule],
  templateUrl: './announcement-form.component.html', styleUrl: './announcement-form.component.css'
})
export class AnnouncementFormComponent implements OnInit {
  form!: FormGroup;
  isEdit = false;
  saving = false;

  constructor(private fb: FormBuilder, private svc: AnnouncementService, private ref: MatDialogRef<AnnouncementFormComponent>, @Inject(MAT_DIALOG_DATA) public data: Announcement | null) {}

  ngOnInit() {
    this.isEdit = !!this.data;
    this.form = this.fb.group({
      title: [this.data?.title || '', Validators.required],
      message: [this.data?.message || '', Validators.required],
      isActive: [this.data ? this.data.isActive : true]
    });
  }

  save() {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    this.saving = true;
    // NOTE: createdBy expects the logged-in user's numeric ID.
    // Since the login response only provides username/role, we default to 1 (admin).
    // Update this if your AuthService starts storing the actual user ID from the JWT.
    const payload: any = { ...this.form.value, createdBy: this.data?.createdBy || 1 };
    if (this.isEdit) payload.announcementId = this.data!.announcementId;

    const call = this.isEdit ? this.svc.update(payload) : this.svc.create(payload);
    call.subscribe({ next: () => this.ref.close(true), error: () => this.saving = false });
  }

  close() { this.ref.close(false); }
}
