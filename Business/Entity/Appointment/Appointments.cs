using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Entity.Appointment
{
    public class Appointments
    {
		public int AppointmentId { get; set; }
		public int AppointmentTypeId { get; set; }
		public int AppointmentTimeId { get; set; }
		public string Description { get; set; }
		public int PatientId { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime AppointmentDate { get; set; }
		public DateTime? LastEditDate { get; set; }
		public DateTime? CreatedDate { get; set; }
	}
}
