export interface Attendance {
  attendanceId: number;
  employeeId: number;
  employeeName: string;
  checkIn: string;
  checkOut?: string;
  totalHours?: number;
  workMode?: string;
  attendanceDate: string;
  createdOn: string;
}