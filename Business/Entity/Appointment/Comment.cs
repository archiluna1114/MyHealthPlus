using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Entity.Appointment
{
    public class Comment
    {
        public int AppointmentId { get; set; }
        public string Comments { get; set; }
        public DateTime CommentDate { get; set; }
    }
}
