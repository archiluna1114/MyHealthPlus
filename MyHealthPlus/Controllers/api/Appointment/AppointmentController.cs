using Business.Entity.Appointment;
using Business.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyHealthPlusWeb.Controllers.api.Appointment
{
    [Route("api/Appointment")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {

        public AppointmentController(IAppointmentService appointmentService

            )
        {
            this._appointmentService = appointmentService;
        }

        private IAppointmentService _appointmentService;

        [HttpPost("Insert")]
        public IActionResult InsertAppointment([FromBody] Appointments appointments)
        {
            var response = _appointmentService.InsertAppointment(appointments);

            return Ok(response);
        }

        [HttpPost("InsertComment")]
        public IActionResult InsertComment([FromBody] Comment comment)
        {
            var response = _appointmentService.InsertComment(comment);

            return Ok(response);
        }


        // GET: api/<AppointmentController>
        [HttpGet("GetTodayAppointments")]
        public IActionResult GetTodayAppointments()
        {
            var response = _appointmentService.GetTodayAppointments();
            return Ok(response);
        }

        // GET api/<AppointmentController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var reponse = _appointmentService.GetAppointmentById(id);

            return Ok(reponse);
        }

        [HttpGet("GetAppointmentById")]
        public IActionResult GetAppointmentById([FromQuery]int id)
        {
            var response = _appointmentService.GetAppointmentById(id);

            return Ok(response);
        }

        [HttpGet("GetAvailableTime")]
        public IActionResult GetAvailableTimeSlotBySelectedDate([FromQuery] string date)
        {
            var response = _appointmentService.GetAvailableTimeSlotBySelectedDate(date);

            return Ok(response);
        }

        [HttpGet("GetAppointmentByUserId")]
        public IActionResult GetAppointmentByUserId([FromQuery] int id)
        {
            var reponse = _appointmentService.GetAppointmentByUserId(id);

            return Ok(reponse);
        }

        // POST api/<AppointmentController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AppointmentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AppointmentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
