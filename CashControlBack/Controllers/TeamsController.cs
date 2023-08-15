using CashControl.Models;
using CashControlBack.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CashControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : Controller
    {
        private readonly ApplicationDb _context;

        public TeamsController(ApplicationDb context)
        {
            _context = context;
        }

        // GET: api/teams
        [HttpGet]
        public ActionResult<IEnumerable<Team>> GetTeams()
        {
            return _context.Teams.ToList();
        }

        // GET: api/teams/{id}
        [HttpGet("{id}")]
        public ActionResult<Team> GetTeam(int id)
        {
            var team = _context.Teams.Find(id);
            if (team == null)
            {
                return NotFound();
            }
            return team;
        }

        // POST: api/teams
        [HttpPost]
        public IActionResult CreateTeam([FromBody] Team team)
        {
            _context.Teams.Add(team);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetTeam), new { id = team.ID }, team);
        }

        // PUT: api/teams/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateTeam(int id, [FromBody] Team updatedTeam)
        {
            var team = _context.Teams.Find(id);
            if (team == null)
            {
                return NotFound();
            }
            team.Name = updatedTeam.Name;
            team.City = updatedTeam.City;
        }
    }
}
