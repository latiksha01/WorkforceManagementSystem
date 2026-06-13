import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { ClientService } from '../../core/services/client.service';
import { Client } from '../../core/models/client';

@Component({
  selector: 'app-client-form', standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatDialogModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatIconModule, MatProgressSpinnerModule, MatSlideToggleModule],
  templateUrl: './client-form.component.html', styleUrl: './client-form.component.css'
})
export class ClientFormComponent implements OnInit {
  form!: FormGroup;
  isEdit = false;
  saving = false;

  constructor(private fb: FormBuilder, private svc: ClientService, private ref: MatDialogRef<ClientFormComponent>, @Inject(MAT_DIALOG_DATA) public data: Client | null) {}

  ngOnInit() {
    this.isEdit = !!this.data;
    this.form = this.fb.group({
      clientName: [this.data?.clientName || '', Validators.required],
      clientAddress: [this.data?.clientAddress || ''],
      clientLocation: [this.data?.clientLocation || ''],
      clientPhoneNumber: [this.data?.clientPhoneNumber || null],
      status: [this.data ? this.data.status : true]
    });
  }

  save() {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    this.saving = true;
    const payload = this.isEdit ? { clientId: this.data!.clientId, ...this.form.value } : this.form.value;
    const call = this.isEdit ? this.svc.update(payload) : this.svc.create(payload);
    call.subscribe({ next: () => this.ref.close(true), error: () => this.saving = false });
  }
  close() { this.ref.close(false); }
}
