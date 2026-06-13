import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { EmployeeService } from '../../core/services/employee.service';
import { DepartmentService } from '../../core/services/department.service';
import { RoleService } from '../../core/services/role.service';
import { Employee } from '../../core/models/employee';
import { Department } from '../../core/models/department';
import { Role } from '../../core/models/role';

@Component({
  selector: 'app-employee-form', standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatDialogModule, MatFormFieldModule, MatInputModule,
    MatButtonModule, MatIconModule, MatSelectModule, MatDatepickerModule, MatNativeDateModule, MatProgressSpinnerModule],
  templateUrl: './employee-form.component.html', styleUrl: './employee-form.component.css'
})
export class EmployeeFormComponent implements OnInit {
  form!: FormGroup;
  isEdit = false;
  saving = false;
  departments: Department[] = [];
  roles: Role[] = [];

  constructor(
    private fb: FormBuilder,
    private svc: EmployeeService,
    private deptSvc: DepartmentService,
    private roleSvc: RoleService,
    private ref: MatDialogRef<EmployeeFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Employee | null
  ) {}

  ngOnInit() {
    this.isEdit = !!this.data;
    this.form = this.fb.group({
      firstName: [this.data?.firstName || '', Validators.required],
      lastName: [this.data?.lastName || '', Validators.required],
      email: [this.data?.email || '', [Validators.required, Validators.email]],
      phoneNumber: [this.data?.phoneNumber || ''],
      gender: [this.data?.gender || 'M'],
      dob: [this.data?.dob ? new Date(this.data.dob) : null],
      doj: [this.data?.doj ? new Date(this.data.doj) : null],
      departmentId: [this.data?.departmentId || null, Validators.required],
      roleId: [this.data?.roleId || null, Validators.required],
      status: [this.data?.status || 'Active']
    });

    this.deptSvc.getAll().subscribe(d => this.departments = d);
    this.roleSvc.getAll().subscribe(r => this.roles = r);
  }

  save() {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    this.saving = true;
    const v = this.form.value;
    const payload: any = {
      ...v,
      dob: v.dob ? this.toDateStr(v.dob) : null,
      doj: v.doj ? this.toDateStr(v.doj) : null
    };
    if (this.isEdit) payload.employeeId = this.data!.employeeId;

    const call = this.isEdit ? this.svc.update(payload) : this.svc.create(payload);
    call.subscribe({ next: () => this.ref.close(true), error: () => this.saving = false });
  }

  toDateStr(d: Date): string {
    const yyyy = d.getFullYear();
    const mm = String(d.getMonth() + 1).padStart(2, '0');
    const dd = String(d.getDate()).padStart(2, '0');
    return `${yyyy}-${mm}-${dd}`;
  }

  close() { this.ref.close(false); }
}
