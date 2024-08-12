using ApontamentoVs2.Domain;
using ApontamentoVs2.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ApontamentoVs2.Repository
{
    public class AppointmentRepository : Repository<Appointment, Guid>, IAppointmentRepository
    {
        public AppointmentRepository(ApplicationDataContext context) : base(context)
        {

        }

        public IEnumerable<Appointment> GetAppointmentsForUserOnDate(Guid userId, DateTime date)
        {
            var startOfDay = date.Date;
            var endOfDay = date.Date.AddDays(1).AddTicks(-1);

            return _entity
                .Where(a => a.User.Id == userId && a.StartTime >= startOfDay && a.EndTime <= endOfDay)
                .ToList();
        }
    }
}
