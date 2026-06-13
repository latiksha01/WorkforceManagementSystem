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
import { LeaveService } from '../../core/services/leave.service';
import { EmployeeService } from '../../core/services/employee.service';
import { Leave } from '../../core/models/leave';
import { Employee } from '../../core/models/employee';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-leave-form', standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatDialogModule, MatFormFieldModule, MatInputModule,
    MatButtonModule, MatIconModule, MatSelectModule, MatDatepickerModule, MatNativeDateModule, MatProgressSpinnerModule],
  templateUrl: './leave-form.component.html', styleUrl: './leave-form.component.css'
})
export class LeaveFormComponent implements OnInit {
  form!: FormGroup;
  isEdit = false;
  saving = false;
  employees: Employee[] = [];
  role = '';
  employeeId = 0;
  employeeName = '';

  constructor(
  private fb: FormBuilder,
  private svc: LeaveService,
  private empSvc: EmployeeService,
  private authService: AuthService,
  private ref: MatDialogRef<LeaveFormComponent>,
  @Inject(MAT_DIALOG_DATA) public data: Leave | null
) {}

  ngOnInit() {
    this.isEdit = !!this.data;
    this.role = this.authService.getRole();

this.employeeId =
  Number(localStorage.getItem('employeeId'));
    this.form = this.fb.group({
      employeeId: [this.data?.employeeId || null, Validators.required],
      leaveType: [this.data?.leaveType || '', Validators.required],
      fromDate: [this.data?.fromDate ? new Date(this.data.fromDate) : null, Validators.required],
      toDate: [this.data?.toDate ? new Date(this.data.toDate) : null, Validators.required],
      reason: [this.data?.reason || '']
    });

    this.empSvc.getAll().subscribe(e => {

  this.employees = e;

  if (this.role === 'Employee') {

    const currentEmployee =
      e.find(emp =>
        emp.employeeId === this.employeeId);

    if (currentEmployee) {

      this.employeeName =
        `${currentEmployee.firstName} ${currentEmployee.lastName}`;

      this.form.patchValue({
        employeeId: currentEmployee.employeeId
      });
    }
  }
});
  }

  save() {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    this.saving = true;
    const v = this.form.value;
    const payload: any = {
      ...v,
      fromDate: this.toDateStr(v.fromDate),
      toDate: this.toDateStr(v.toDate)
    };
    if (this.isEdit) {
      payload.leaveId = this.data!.leaveId;
      payload.status = this.data!.status;
    }

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
