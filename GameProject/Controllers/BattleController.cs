using GameProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GameProject.Controllers
{
    public class BattleController : Controller
    {
        private readonly AppDbContext _context;

        public BattleController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Battle/Index
        public IActionResult Index()
        {
            var teams = _context.Teams.ToList();
            return View(teams);
        }

        // POST: Battle/Fight
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Fight(int team1Id, int team2Id)
        {
            var team1 = _context.Teams.Include(t => t.Characters).FirstOrDefault(t => t.Id == team1Id);
            var team2 = _context.Teams.Include(t => t.Characters).FirstOrDefault(t => t.Id == team2Id);

            if (team1 == null || team2 == null || team1Id == team2Id)
            {
                ModelState.AddModelError("", "Invalid team selection.");
                return RedirectToAction(nameof(Index));
            }

            // Simple logic to determine the winner (sum of character strength)
            int team1Strength = team1.Characters.Sum(c => c.Strength);
            int team2Strength = team2.Characters.Sum(c => c.Strength);

            var winnerTeamId = team1Strength > team2Strength ? team1Id : team2Id;

            // Save the battle result
            var battle = new Battle
            {
                Team1Id = team1Id,
                Team2Id = team2Id,
                WinnerTeamId = winnerTeamId
            };

            _context.Battles.Add(battle);
            _context.SaveChanges();

            return RedirectToAction(nameof(Result), new { id = battle.Id });
        }

        // GET: Battle/Result/5
        public IActionResult Result(int id)
        {
            var battle = _context.Battles
                .Include(b => b.Team1)
                .Include(b => b.Team2)
                .Include(b => b.WinnerTeam)
                .FirstOrDefault(b => b.Id == id);

            if (battle == null)
            {
                return NotFound();
            }

            return View(battle);
        }
    }

}
