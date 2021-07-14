using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Entity.Appointment
{
    public class ViewAppointment
    {
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string DisplayTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
