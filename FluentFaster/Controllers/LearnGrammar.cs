using FluentFaster.Data;
using FluentFaster.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FluentFaster.Controllers
{
    public class LearnGrammarController : Controller
    {
        private readonly AppDbContext _context;

        public LearnGrammarController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> TopicDetails(int id)
        {
            var topic = await _context.GrammarTopics
                .Include(t => t.Tests)
                .FirstOrDefaultAsync(t => t.TopicID == id);

            if (topic == null)
                return NotFound();

            var viewName = topic.TopicName.Replace(" ", "");

            if (!ViewExists(viewName))
            {
                return View("DefaultTopic", topic); 
            }

            return View(viewName, topic);
        }

        private bool ViewExists(string viewName)
        {
            var viewPath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "LearnGrammar", viewName + ".cshtml");
            return System.IO.File.Exists(viewPath);
        }

        public async Task<IActionResult> TestDetails(int id)
        {
            var test = await _context.Tests
                .Include(t => t.Questions)
                    .ThenInclude(q => q.AnswerOptions)
                .FirstOrDefaultAsync(t => t.TestID == id);

            if (test == null)
                return NotFound();

            test.Questions = test.Questions.OrderBy(q => Guid.NewGuid()).ToList(); //random questions

            foreach (var question in test.Questions)
            {
                question.AnswerOptions = question.AnswerOptions.OrderBy(a => Guid.NewGuid()).ToList(); //random anwsers
            }

            return View(test);
        }
    }
}
