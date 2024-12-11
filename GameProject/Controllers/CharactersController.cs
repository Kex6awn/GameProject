using GameProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GameProject.Controllers
{
    public class CharactersController : Controller
    {
        private readonly AppDbContext _context;

        public CharactersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Characters/Customize
        public IActionResult Customize(int? id)
        {
            Character character;

            if (id.HasValue)
            {
                // Edit existing character
                character = _context.Characters.FirstOrDefault(c => c.Id == id);
                if (character == null)
                {
                    return NotFound();
                }
            }
            else
            {
                // Create new character
                character = new Character();
            }

            return View(character);
        }

        // POST: Characters/Customize
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Customize(Character character)
        {
            if (ModelState.IsValid)
            {
                if (character.Id == 0)
                {
                    // New character
                    _context.Characters.Add(character);
                }
                else
                {
                    // Update existing character
                    _context.Characters.Update(character);
                }

                _context.SaveChanges();
                return RedirectToAction("Details", "Team", new { id = character.TeamId });
            }

            return View(character);
        }
    }

}
