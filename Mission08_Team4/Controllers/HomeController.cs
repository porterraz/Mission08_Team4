using Microsoft.AspNetCore.Mvc;
using Mission08_Team4.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Mission08_Team4.Controllers
{
    public class HomeController : Controller
    {
        private ITaskRepository _repo;

        public HomeController(ITaskRepository temp)
        {
            _repo = temp;
        }

        // Display all tasks sorted by quadrant
        public IActionResult Index()
        {
            var tasks = _repo.Tasks.Include(x => x.Category).ToList();
            return View(tasks);
        }

        [HttpGet]
        public IActionResult AddEditTask()
        {
            ViewBag.Categories = _repo.Categories.OrderBy(x => x.CategoryName).ToList();
            return View(new TaskItem());
        }

        [HttpPost]
        public IActionResult AddEditTask(TaskItem response)
        {
            if (ModelState.IsValid)
            {
                // New task has TaskId of 0
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