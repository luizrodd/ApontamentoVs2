using ApontamentoVs2.Domain;
using ApontamentoVs2.Infrastructure;
using System.Security.Claims;

namespace ApontamentoVs2.Repository
{
    public class AppointmentRepository : Repository<Appointment, Guid>, IAppointmentRepository
    {
        public AppointmentRepository(ApplicationDataContext context) : base(context)
        {
        }
    }
}
