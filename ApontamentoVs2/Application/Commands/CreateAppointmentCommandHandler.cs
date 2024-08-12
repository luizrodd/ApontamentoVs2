//using ApontamentoVs2.Domain;
//using ApontamentoVs2.Repository;
//using MediatR;

//namespace ApontamentoVs2.Application.Commands
//{
//    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, bool>
//    {
//        private IAppointmentRepository _repository;
//        public CreateAppointmentCommandHandler(IAppointmentRepository repository)
//        {
//            _repository = repository;
//        }
//        public Task<bool> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
//        {
//            var command = new Appointment(request.ProjectId, request.UserId, request.StartTime, request.EndTime);
//            _repository.Add(command);
//            _repository.SaveChanges();

//            return Task.FromResult(true);
//        }
//    }
//}
