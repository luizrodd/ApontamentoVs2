using ApontamentoVs2.Domain;
using MediatR;

namespace ApontamentoVs2.Application.Commands
{
    public class CreateAppointmentCommand : IRequest<bool>
    {
        public Guid ProjectId;
        public Guid UserId;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
