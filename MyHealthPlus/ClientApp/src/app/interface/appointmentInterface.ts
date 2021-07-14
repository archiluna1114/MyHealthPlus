export interface IAppointment {
  appointmentId?: number;
  appointmentTypeId: number;
  appointmentTimeId: number;
  description: string;
  patientId: number;
  isDeleted?: boolean;
  appointmentDate: string;
}
