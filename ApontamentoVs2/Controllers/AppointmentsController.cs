using ApontamentoVs2.Controllers.Requests;
using ApontamentoVs2.Domain;
using ApontamentoVs2.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApontamentoVs2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly ApplicationDataContext _context;

        public AppointmentsController(ApplicationDataContext context)
        {
            _context = context;
        }

        // GET: api/Appointments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
        {
            return await _context.Appointments.ToListAsync();
        }
        // POST: api/Appointments
        [HttpPost]
        public async Task<ActionResult<Appointment>> PostAppointment(AppointmentRequest appointmentRequests)
        {
            var userID = "";
            var appointment = new Appointment(appointmentRequests.ProjectId,
                appointmentRequests.UserID,
                appointmentRequests.StartTime, appointmentRequests.EndTime,
                appointmentRequests.Description);

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return Ok();

        }

   
    }
}
