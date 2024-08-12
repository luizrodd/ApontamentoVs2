using ApontamentoVs2.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApontamentoVs2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private IMediator _mediator;

        public AppointmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentCommand command)
        {
            command.ProjectId = new Guid("F024466C-6AB0-4879-A476-3AD9D1CCD86C");
            command.UserId = new Guid("D0F83055-0E4B-4876-A817-F1A93ABBD5D9");
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
