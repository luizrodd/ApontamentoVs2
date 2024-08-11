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
            command.ProjectId = new Guid("D8FD791F-4EA9-43D0-8FEA-40A0E52F7032");
            command.UserId = new Guid("3CF83033-D023-428A-8E85-8D0C538B709D");
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
