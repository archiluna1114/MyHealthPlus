using System;
using System.Collections.Generic;
using Business.Entity.Appointment;
using System.Text;

namespace Business.Interfaces.Repositories.Appointment
{
    public interface IAppointmentRepository
    {
        int InsertAppointment(Appointments appointment);
        int InsertComment(Comment comment);
        Appointments GetAppointmentById(int appointmendId);
        List<ViewAppointment> GetTodayAppointments();
        List<ViewAppointment> GetAppointmentByUserId(int userId);
        List<AppointmentTime> GetAvailableTimeSlotBySelectedDate(string selectedDate);
    }
}
