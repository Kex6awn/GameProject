using GameProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GameProject.Controllers
{
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;

        public TeamController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Team/Details/5
        public IActionResult Details(int id)
        {
            var team = _context.Teams.Include(t => t.Characters).FirstOrDefault(t => t.Id == id);
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }

        // POST: Team/AddCharacter
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCharacter(int teamId, Character character)
        {
            var team = _context.Teams.Include(t => t.Characters).FirstOrDefault(t => t.Id == teamId);
            if (team == null)
            {
                return NotFound();
            }

            if (team.Characters.Count >= 10)
            {
                ModelState.AddModelError("", "A team can only have up to 10 characters.");
                return RedirectToAction(nameof(Details), new { id = teamId });
            }

            character.TeamId = teamId;
            _context.Characters.Add(character);
            _context.SaveChanges();

            return RedirectToAction(nameof(Details), new { id = teamId });
        }
    }

}
