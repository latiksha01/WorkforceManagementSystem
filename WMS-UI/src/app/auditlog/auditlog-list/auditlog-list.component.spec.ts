import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuditlogListComponent } from './auditlog-list.component';

describe('AuditlogListComponent', () => {
  let component: AuditlogListComponent;
  let fixture: ComponentFixture<AuditlogListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AuditlogListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AuditlogListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
