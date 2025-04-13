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

        [HttpPost]
        public IActionResult CreateNewTaskFormSubmit(TaskItem i_task) 
        {
            if (string.IsNullOrWhiteSpace(i_task.TaskDescription))
            {
                i_task.TaskDescription = "";
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction("CreateNewTask");
            }
            _context.Tasks.Add(i_task);
            _context.SaveChanges();
            return RedirectToAction("MyTasks");
        }

        public IActionResult ClearAllTasks()
        {
            foreach (var item in _context.Tasks)
            {
                _context.Tasks.Remove(item);
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteTask(int Id)
        {
            var task = _context.Tasks.FirstOrDefault(task=>task.Id == Id);
            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return RedirectToAction("MyTasks");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
