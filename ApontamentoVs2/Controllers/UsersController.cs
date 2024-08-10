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
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDataContext _context;

        public UsersController(ApplicationDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserRequest user)
        {
            var user1 = new User(user.Name, user.Email, user.Password);
            _context.Users.Add(user1);
            await _context.SaveChangesAsync();

            return Ok();

        }
    }
}
