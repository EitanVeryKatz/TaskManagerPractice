using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq.Expressions;
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

        public IActionResult MyTasks(string SortBy, string Show)
        {
            List<TaskItem> AllTasks= _context.Tasks.ToList(); ;

            switch (Show)
            {
                case "Completed":
                    AllTasks = AllTasks.Where(task => task.WasCompleted == true).ToList();
                    break;
                case "NonCompleted":
                    AllTasks = AllTasks.Where(task=>task.WasCompleted==false).ToList(); 
                    break;
                
            }


            switch (SortBy)
            {
                case "ByDueDate":
                  AllTasks = AllTasks.OrderBy(task => task.DeadlineDate).ToList();
                    break;
                case "ByOrder":
                    AllTasks = AllTasks.OrderBy(task => task.Id).ToList();
                    break;
                case "ByComplition":
                    AllTasks = AllTasks.OrderBy(task => task.WasCompleted).ToList();
                    break;
                case "ByName":
                    AllTasks = AllTasks.OrderBy(task => task.TaskName).ToList();
                    break;
            }
            return View(AllTasks);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult EditOrCreateNewTask(int Id)
        {
            TaskItem task;
            if(Id== 0)
            {
                task = new TaskItem();
            }
            else//existing Item
            {
                task = _context.Tasks.SingleOrDefault(task => task.Id == Id);

            }
            return View(task);
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
                return RedirectToAction("EditOrCreateNewTask",i_task);
            }

            if (i_task.Id == 0)
            {
                _context.Tasks.Add(i_task);
            }
            else
            {
                _context.Tasks.Update(i_task);
            }
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
            var task = _context.Tasks.FirstOrDefault(task => task.Id == Id);
            _context.Tasks.Remove(task);

            _context.SaveChanges();
            return RedirectToAction("MyTasks");
        }

        public IActionResult ChangeTaskComplitionStatus(int Id)
        {
            var task = _context.Tasks.FirstOrDefault(task => task.Id == Id);
            task.WasCompleted = !task.WasCompleted;
            _context.Tasks.Update(task);
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
