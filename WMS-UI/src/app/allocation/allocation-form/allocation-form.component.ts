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
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { AllocationService } from '../../core/services/allocation.service';
import { EmployeeService } from '../../core/services/employee.service';
import { ProjectService } from '../../core/services/project.service';
import { AuthService } from '../../core/services/auth.service';
import { Allocation } from '../../core/models/allocation';
import { Employee } from '../../core/models/employee';
import { Project } from '../../core/models/project';

@Component({
  selector: 'app-allocation-form', standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatDialogModule, MatFormFieldModule, MatInputModule,
    MatButtonModule, MatIconModule, MatSelectModule, MatDatepickerModule, MatNativeDateModule,
    MatSlideToggleModule, MatProgressSpinnerModule],
  templateUrl: './allocation-form.component.html', styleUrl: './allocation-form.component.css'
})
export class AllocationFormComponent implements OnInit {
  form!: FormGroup;
  isEdit = false;
  saving = false;
  employees: Employee[] = [];
  projects: Project[] = [];

  constructor(
    private fb: FormBuilder,
    private svc: AllocationService,
    private empSvc: EmployeeService,
    private projSvc: ProjectService,
    private authSvc: AuthService,
    private ref: MatDialogRef<AllocationFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Allocation | null
  ) {}

  ngOnInit() {
    this.isEdit = !!this.data;
    this.form = this.fb.group({
      employeeId: [this.data?.employeeId || null, Validators.required],
      projectId: [this.data?.projectId || null, Validators.required],
      assignedOn: [this.data?.assignedOn ? new Date(this.data.assignedOn) : new Date(), Validators.required],
      status: [this.data ? this.data.status : true]
    });

    this.empSvc.getAll().subscribe(e => this.employees = e);
    this.projSvc.getAll().subscribe(p => this.projects = p);
  }

  save() {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    this.saving = true;
    const v = this.form.value;
    const payload: any = {
      ...v,
      assignedOn: this.toDateStr(v.assignedOn),
      createdBy: this.authSvc.getUsername()
    };
    if (this.isEdit) payload.allocationId = this.data!.allocationId;

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
