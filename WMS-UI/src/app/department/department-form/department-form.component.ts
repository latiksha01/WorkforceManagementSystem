import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { DepartmentService } from '../../core/services/department.service';
import { Department } from '../../core/models/department';

@Component({
  selector: 'app-department-form',
  standalone: true,
  imports: [
    CommonModule, ReactiveFormsModule,
    MatDialogModule, MatFormFieldModule, MatInputModule,
    MatButtonModule, MatIconModule, MatProgressSpinnerModule
  ],
  templateUrl: './department-form.component.html',
  styleUrl: './department-form.component.css'
})
export class DepartmentFormComponent implements OnInit {
  form!: FormGroup;
  isEdit = false;
  saving = false;

  constructor(
    private fb: FormBuilder,
    private deptService: DepartmentService,
    private dialogRef: MatDialogRef<DepartmentFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Department | null
  ) {}

  ngOnInit(): void {
    this.isEdit = !!this.data;
    this.form = this.fb.group({
      departmentName: [this.data?.departmentName || '', [Validators.required, Validators.maxLength(100)]],
      description:    [this.data?.description    || '', Validators.maxLength(255)]
    });
  }

  save(): void {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    this.saving = true;

    const payload = this.isEdit
      ? { departmentId: this.data!.departmentId, ...this.form.value }
      : this.form.value;

    const call = this.isEdit
      ? this.deptService.updateDepartment(payload)
      : this.deptService.createDepartment(payload);

    call.subscribe({
      next: () => this.dialogRef.close(true),
      error: () => { this.saving = false; }
    });
  }

  close(): void { this.dialogRef.close(false); }
}
