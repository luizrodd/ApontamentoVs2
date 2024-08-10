using ApontamentoVs2.Domain;
using ApontamentoVs2.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApontamentoVs2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ApplicationDataContext _context;

        public ProjectsController(ApplicationDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            return await _context.Projects.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Project>> PostProject([FromBody] string name)
        {
            var project = new Project(name);

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return Ok();

        }


    }
}
