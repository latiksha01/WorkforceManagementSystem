export interface AuditLog {
  auditId: number;
  entityName: string;
  recordId: number;
  action: string;
  createdBy: number;
  createdByName: string;
  createdOn: string;
}