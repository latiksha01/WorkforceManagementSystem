export interface Leave {
  leaveId: number;
  employeeId: number;
  employeeName: string;
  leaveType: string;
  reason?: string;
  fromDate: string;
  toDate: string;
  status: string;
  appliedOn: string;
  approvedBy?: number;
  approvedByName?: string;
  approvedOn?: string;
  createdOn: string;
}