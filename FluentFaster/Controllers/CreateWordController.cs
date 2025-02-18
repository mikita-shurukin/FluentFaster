using FluentFaster.Data;
using FluentFaster.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FluentFaster.Controllers
{
    public class CreateWordController : Controller
    {
        private readonly AppDbContext _context;

        public CreateWordController(AppDbContext context)
        {
            _context = context;
        }
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(int gameId)
        {
            var game = await _context.Games
                .Include(g => g.Words)
                .FirstOrDefaultAsync(g => g.GameID == 3); 

            if (game == null)
                return NotFound();

            var randomWord = game.Words.OrderBy(_ => Guid.NewGuid()).FirstOrDefault(); 
            if (randomWord == null)
                return Content("No words available for this game.");

            var scrambledWord = new string(randomWord.Text.OrderBy(_ => Guid.NewGuid()).ToArray());

            ViewBag.GameId = gameId;
            ViewBag.OriginalWord = randomWord.Text;
            ViewBag.ScrambledWord = scrambledWord;

            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SubmitAnswer(int gameId, string originalWord, string userAnswer, string action)
        {
            try
            {
                var game = await _context.Games
                    .Include(g => g.Words)
                    .FirstOrDefaultAsync(g => g.GameID == 3); 

                if (game == null)
                    return Json(new { message = "Game not found!", scrambledWord = "", originalWord = "" });

                string message;
                Word nextWord;

                if (action == "Skip" || !string.Equals(originalWord, userAnswer, StringComparison.OrdinalIgnoreCase))
                {
                    message = action == "Skip" ? "Skipped!" : $"Wrong. The correct word is: {originalWord}";

                    nextWord = game.Words
                        .Where(w => w.Text != originalWord)
                        .OrderBy(_ => Guid.NewGuid())
                        .FirstOrDefault();
                }
                else
                {
                    message = "Correct!";
                    nextWord = game.Words
                        .Where(w => w.Text != originalWord)
                        .OrderBy(_ => Guid.NewGuid())
                        .FirstOrDefault();
                }

                if (nextWord == null)
                {
                    return Json(new { message = "Game Over!", scrambledWord = "", originalWord = "" });
                }

                var scrambledWord = new string(nextWord.Text.OrderBy(_ => Guid.NewGuid()).ToArray());

                return Json(new
                {
                    message,
                    scrambledWord,
                    originalWord = nextWord.Text
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(new { message = "An unexpected error occurred. Please try again.", scrambledWord = "", originalWord = "" });
            }
        }
        #region admin
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddWord(string newWord)
        {
            if (string.IsNullOrWhiteSpace(newWord))
                return BadRequest("Word cannot be empty.");

            var game = await _context.Games.FirstOrDefaultAsync(g => g.GameID == 3);
            if (game == null)
                return NotFound();

            var word = new Word { Text = newWord, GameID = 3 };
            _context.Words.Add(word);
            await _context.SaveChangesAsync();

            return RedirectToAction("ManageWords");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteWord(int wordId)
        {
            var word = await _context.Words.FindAsync(wordId);
            if (word == null)
                return NotFound();

            _context.Words.Remove(word);
            await _context.SaveChangesAsync();

            return RedirectToAction("ManageWords");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditWord(int wordId, string updatedWord)
        {
            if (string.IsNullOrWhiteSpace(updatedWord))
                return BadRequest("Word cannot be empty.");

            var word = await _context.Words.FindAsync(wordId);
            if (word == null)
                return NotFound();

            word.Text = updatedWord;
            _context.Words.Update(word);
            await _context.SaveChangesAsync();

            return RedirectToAction("ManageWords");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ManageWords()
        {
            try
            {
                var game = await _context.Games
                    .Include(g => g.Words)
                    .FirstOrDefaultAsync(g => g.GameID == 3);

                if (game == null)
                {
                    Console.WriteLine("Game not found.");
                    return NotFound();
                }

                Console.WriteLine($"Game found: {game.GameName}, Words count: {game.Words?.Count ?? 0}");
                return View(game);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }


        #endregion
    }
}