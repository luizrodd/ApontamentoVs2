using ApontamentoVs2.Domain;
using ApontamentoVs2.Repository;
using MediatR;

namespace ApontamentoVs2.Application.Commands
{
    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, bool>
    {
        private IAppointmentRepository _repository;
        private readonly AppointmentRuleEngine _ruleEngine;
        private IUserRepository _userRepository;

        public CreateAppointmentCommandHandler(IAppointmentRepository repository, AppointmentRuleEngine ruleEngine, IUserRepository userRepository)
        {
            _repository = repository;
            _ruleEngine = ruleEngine;
            _userRepository = userRepository;
        }

        public Task<bool> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetById(request.UserId);

            var appointment = new Appointment(request.ProjectId, request.UserId, request.StartTime, request.EndTime, DateTime.Now);

            appointment.User = user;

            _ruleEngine.Validate(appointment);
            _repository.Add(appointment);
            _repository.SaveChanges();

            return Task.FromResult(true);
        }
    }


}
