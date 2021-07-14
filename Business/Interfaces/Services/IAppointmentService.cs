using Business.Entity.Appointment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interfaces.Services
{
    public interface IAppointmentService
    {
        int InsertAppointment(Appointments appointment);
        int InsertComment(Comment comment);
        Appointments GetAppointmentById(int appointmendId);
        List<ViewAppointment> GetTodayAppointments();
        List<ViewAppointment> GetAppointmentByUserId(int userId);
        List<AppointmentTime> GetAvailableTimeSlotBySelectedDate(string selectedDate);
    }
}
