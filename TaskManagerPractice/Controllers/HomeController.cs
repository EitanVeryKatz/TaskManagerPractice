using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TaskManagerPractice.Models;


namespace TaskManagerPractice.Controllers
{
    public class HomeController : Controller
    {
        
        readonly TasksDb _context;

        private readonly ILogger<HomeController> _logger;
        
        public HomeController(ILogger<HomeController> logger, TasksDb context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MyTasks()
        {
            List<TaskItem> AllTasks = _context.Tasks.ToList();
            return View(AllTasks);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult CreateNewTask()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
