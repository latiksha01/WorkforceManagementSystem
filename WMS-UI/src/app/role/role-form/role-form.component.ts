import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { RoleService } from '../../core/services/role.service';
import { Role } from '../../core/models/role';

@Component({
  selector: 'app-role-form', standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatDialogModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatIconModule, MatProgressSpinnerModule],
  templateUrl: './role-form.component.html', styleUrl: './role-form.component.css'
})
export class RoleFormComponent implements OnInit {
  form!: FormGroup;
  isEdit = false;
  saving = false;

  constructor(private fb: FormBuilder, private svc: RoleService, private ref: MatDialogRef<RoleFormComponent>, @Inject(MAT_DIALOG_DATA) public data: Role | null) {}

  ngOnInit() {
    this.isEdit = !!this.data;
    this.form = this.fb.group({
      roleName: [this.data?.roleName || '', Validators.required],
      description: [this.data?.description || '']
    });
  }

  save() {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    this.saving = true;
    const payload = this.isEdit ? { roleId: this.data!.roleId, ...this.form.value } : this.form.value;
    const call = this.isEdit ? this.svc.update(payload) : this.svc.create(payload);
    call.subscribe({ next: () => this.ref.close(true), error: () => this.saving = false });
  }
  close() { this.ref.close(false); }
}