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
import { AttendanceService } from '../../core/services/attendance.service';
import { EmployeeService } from '../../core/services/employee.service';
import { Attendance } from '../../core/models/attendance';
import { Employee } from '../../core/models/employee';

@Component({
  selector: 'app-attendance-form', standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatDialogModule, MatFormFieldModule, MatInputModule,
    MatButtonModule, MatIconModule, MatSelectModule, MatDatepickerModule, MatNativeDateModule, MatProgressSpinnerModule],
  templateUrl: './attendance-form.component.html', styleUrl: './attendance-form.component.css'
})
export class AttendanceFormComponent implements OnInit {
  form!: FormGroup;
  saving = false;
  employees: Employee[] = [];
  title = 'Add Attendance Record';

  constructor(
    private fb: FormBuilder,
    private svc: AttendanceService,
    private empSvc: EmployeeService,
    private ref: MatDialogRef<AttendanceFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { record: Attendance | null; mode: 'manual' | 'checkin' | 'checkout' }
  ) {}

  ngOnInit() {
    const r = this.data.record;
    const now = new Date();
    const nowTime = now.toTimeString().slice(0, 5);

    if (this.data.mode === 'checkin') this.title = 'Check In';
    if (this.data.mode === 'checkout') this.title = 'Check Out';
    if (this.data.mode === 'manual' && r) this.title = 'Edit Attendance Record';

    this.form = this.fb.group({
      employeeId: [r?.employeeId || null, Validators.required],
      attendanceDate: [r?.attendanceDate ? new Date(r.attendanceDate) : now],
      checkInTime: [r?.checkIn ? this.toTimeStr(r.checkIn) : (this.data.mode === 'checkin' ? nowTime : '')],
      checkOutTime: [r?.checkOut ? this.toTimeStr(r.checkOut) : (this.data.mode === 'checkout' ? nowTime : '')],
      totalHours: [r?.totalHours ?? null],
      workMode: [r?.workMode || 'Onsite']
    });

    this.empSvc.getAll().subscribe(e => this.employees = e);
  }

  toTimeStr(dateStr: string): string {
    const d = new Date(dateStr);
    return d.toTimeString().slice(0, 5);
  }

  save() {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    this.saving = true;
    const v = this.form.value;
    const dateStr = this.toDateStr(v.attendanceDate);

    const payload: any = {
      employeeId: v.employeeId,
      attendanceDate: dateStr,
      checkIn: v.checkInTime ? `${dateStr}T${v.checkInTime}:00` : null,
      checkOut: v.checkOutTime ? `${dateStr}T${v.checkOutTime}:00` : null,
      totalHours: v.totalHours,
      workMode: v.workMode
    };
    if (this.data.record) payload.attendanceId = this.data.record.attendanceId;

    const call = this.data.record ? this.svc.update(payload) : this.svc.create(payload);
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
