using Microsoft.AspNetCore.Mvc;
using Mission08_Team4.Models;
using Microsoft.EntityFrameworkCore;

namespace Mission08_Team4.Controllers
{
    public class HomeController : Controller
    {
        private ITaskRepository _repo;

        [cite_start]// Constructor for Dependency Injection 
        public HomeController(ITaskRepository temp)
        {
            _repo = temp;
        }

        public IActionResult Index()
        {
            [cite_start]// The Quadrant View logic will be handled by Person 3 
            var tasks = _repo.Tasks.Include(x => x.Category).ToList();
            return View(tasks);
        }

        [HttpGet]
        public IActionResult AddEditTask()
        {
            [cite_start]// Populate dropdown with categories from the database 
            ViewBag.Categories = _repo.Categories.OrderBy(x => x.CategoryName).ToList();
            return View(new TaskItem());
        }

        [HttpPost]
        public IActionResult AddEditTask(TaskItem response)
        {
            if (ModelState.IsValid)
            {
                [cite_start]// Check if it's a new task or an update 
                if (response.TaskId == 0)
                {
                    _repo.AddTask(response);
                }
                else
                {
                    _repo.UpdateTask(response);
                }
                return RedirectToAction("Index");
            }

            [cite_start]// If validation fails, reload the form with categories 
            ViewBag.Categories = _repo.Categories.OrderBy(x => x.CategoryName).ToList();
            return View(response);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var task = _repo.Tasks.Single(x => x.TaskId == id);
            ViewBag.Categories = _repo.Categories.OrderBy(x => x.CategoryName).ToList();
            return View("AddEditTask", task);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var task = _repo.Tasks.Single(x => x.TaskId == id);
            return View(task);
        }

        [HttpPost]
        public IActionResult Delete(TaskItem task)
        {
            _repo.DeleteTask(task);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult MarkCompleted(int id)
        {
            var task = _repo.Tasks.Single(x => x.TaskId == id);
            task.Completed = true;
            _repo.UpdateTask(task);
            return RedirectToAction("Index");
        }
    }
}