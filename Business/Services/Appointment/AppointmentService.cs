using Business.Entity.Appointment;
using Business.Interfaces.Repositories.Appointment;
using Business.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services.Appointment
{
    public class AppointmentService : IAppointmentService
    {
        public AppointmentService(IAppointmentRepository appointmentRepository
            )
        {
            this._appointmentRepository = appointmentRepository;
        }

        private IAppointmentRepository _appointmentRepository;

        public int InsertAppointment(Appointments appointments)
        {

            return _appointmentRepository.InsertAppointment(appointments);
        }

        public int InsertComment(Comment comment)
        {
            return _appointmentRepository.InsertComment(comment);
        }

        public Appointments GetAppointmentById(int appointmendId)
        {
            return _appointmentRepository.GetAppointmentById(appointmendId);
        }

        public List<ViewAppointment> GetTodayAppointments()
        {
            return _appointmentRepository.GetTodayAppointments();
        }

        public List<ViewAppointment> GetAppointmentByUserId(int userId)
        {
            return _appointmentRepository.GetAppointmentByUserId(userId);
        }
        public List<AppointmentTime> GetAvailableTimeSlotBySelectedDate(string selectedDate)
        {
            return _appointmentRepository.GetAvailableTimeSlotBySelectedDate(selectedDate);
        }
    }
}
