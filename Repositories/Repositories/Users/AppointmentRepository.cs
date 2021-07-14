using Business.Entity.Appointment;
using Business.Interfaces.Repositories.Appointment;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Repositories.Users
{
    public class AppointmentRepository : DapperRepository, IAppointmentRepository
    {
        #region Constructors
        public AppointmentRepository(IConfiguration configuration)
                    : base(configuration)
        {
        }
        #endregion

        #region Public Methods
        public int InsertAppointment(Appointments appointment)
        {
            return QueryScalar<int>(
                "dbo.Appointment_Insert",
                new
                {
                    appointment.AppointmentTypeId,
                    appointment.AppointmentDate,
                    appointment.AppointmentTimeId,
                    appointment.Description,
                    appointment.PatientId
                }
            );
        }

        public int InsertComment(Comment comment)
        {
            return QueryScalar<int>(
                "dbo.Comment_Insert",
                new
                    {
                    comment.AppointmentId,
                    comment.Comments,
                    comment.@CommentDate
                    }
                );
        }

        public Appointments GetAppointmentById(int appointmendId)
        {
            return QueryScalar<Appointments>(
                "dbo.Appointment_GetAppointmentById",
                new
                {
                    AppointmentId = appointmendId
                }
            );
        }

        public List<ViewAppointment> GetTodayAppointments()
        {
            return QueryList<ViewAppointment>("dbo.Appointment_GetTodayAppointments");
        }

        public List<ViewAppointment> GetAppointmentByUserId(int userId)
        {
            return QueryList<ViewAppointment>("dbo.Appointment_GetAppointmentByUserId",
                new
                {
                    Userid = userId
                }

            );
        }

        public List<AppointmentTime> GetAvailableTimeSlotBySelectedDate(string selectedDate)
        {
            return QueryList<AppointmentTime>("dbo.Appointment_GetAvailableTimeSlotBySelectedDate",
                new
                {
                    SelectedDate = selectedDate
                }

            );
        }
        #endregion
    }
}
