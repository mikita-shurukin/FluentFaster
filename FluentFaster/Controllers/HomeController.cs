using FluentFaster.Data;
using FluentFaster.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FluentFaster.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var games = await _context.Games.ToListAsync();
            return View(games);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult LearnGrammar()
        {
            return View();
        }
        public async Task<IActionResult> Games(int id)
        {
            var game = await _context.Games.Include(t => t.GameName)
                                           .FirstOrDefaultAsync(t => t.GameID == id);
            if (game == null)
                return NotFound();

            return View(game);
        }
        public async Task<IActionResult> GameDetails(int id)
        {
            var game = await _context.Games
                                     .Include(g => g.GrammarTopics)
                                     .FirstOrDefaultAsync(g => g.GameID == id);

            if (game == null)
                return NotFound();

            return View(game);
        }

    }
}
