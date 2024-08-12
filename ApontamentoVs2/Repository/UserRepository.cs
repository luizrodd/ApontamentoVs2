using ApontamentoVs2.Domain;
using ApontamentoVs2.Infrastructure;

namespace ApontamentoVs2.Repository
{
    public class UserRepository : Repository<User, Guid>, IUserRepository
    {
        public UserRepository(ApplicationDataContext context) : base(context)
        {
        }
    }
}
