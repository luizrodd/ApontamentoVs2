using ApontamentoVs2.Domain;

namespace ApontamentoVs2.Repository
{
    public interface IAppointmentRepository : IRepository<Appointment, Guid>
    {
        IEnumerable<Appointment> GetAppointmentsForUserOnDate(Guid userId, DateTime date);
    }
}
