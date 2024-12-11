using Microsoft.AspNetCore.Mvc;
using System;

namespace GameProject.Controllers
{
    public class LeaderboardController : Controller
    {
        private readonly AppDbContext _context;

        public LeaderboardController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Leaderboard/Index
        public IActionResult Index()
        {
            var topFighters = _context.Characters
                .OrderByDescending(c => c.Wins)
                .Take(10)
                .ToList();

            return View(topFighters);
        }
    }

}
