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
import { ProjectService } from '../../core/services/project.service';
import { ClientService } from '../../core/services/client.service';
import { Project } from '../../core/models/project';
import { Client } from '../../core/models/client';

@Component({
  selector: 'app-project-form', standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatDialogModule, MatFormFieldModule, MatInputModule,
    MatButtonModule, MatIconModule, MatSelectModule, MatDatepickerModule, MatNativeDateModule, MatProgressSpinnerModule],
  templateUrl: './project-form.component.html', styleUrl: './project-form.component.css'
})
export class ProjectFormComponent implements OnInit {
  form!: FormGroup;
  isEdit = false;
  saving = false;
  clients: Client[] = [];

  constructor(private fb: FormBuilder, private svc: ProjectService, private clientSvc: ClientService, private ref: MatDialogRef<ProjectFormComponent>, @Inject(MAT_DIALOG_DATA) public data: Project | null) {}

  ngOnInit() {
    this.isEdit = !!this.data;
    this.form = this.fb.group({
      projectName: [this.data?.projectName || '', Validators.required],
      clientId: [this.data?.clientId || null],
      startDate: [this.data?.startDate ? new Date(this.data.startDate) : null],
      endDate: [this.data?.endDate ? new Date(this.data.endDate) : null],
      status: [this.data?.status || 'Active', Validators.required]
    });

    this.clientSvc.getAll().subscribe(c => this.clients = c);
  }

  save() {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    this.saving = true;
    const v = this.form.value;
    const payload: any = {
      ...v,
      startDate: v.startDate ? this.toDateStr(v.startDate) : null,
      endDate: v.endDate ? this.toDateStr(v.endDate) : null
    };
    if (this.isEdit) payload.projectId = this.data!.projectId;

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
