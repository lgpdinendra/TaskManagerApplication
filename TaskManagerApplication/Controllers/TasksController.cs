using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApplication.Areas.Identity.Data;
using TaskManagerApplication.Models;


namespace TaskManagerApplication.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly TaskManagerApplicationDbContext context;

        public TasksController(TaskManagerApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            try
            {
                var tasks = context.Tasks.ToList();
                return View(tasks);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while fetching tasks. Please try again later.";
                return RedirectToAction("Error");
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TasksDto tasksDto)
        {
            if (tasksDto.Title == null)
            {
                ModelState.AddModelError("Title", "This Feild is empty");
            }
            if (!ModelState.IsValid)
            {
                return View(tasksDto);
            }
            try
            {

                //save new Task
                Tasks tasks = new Tasks()
                {
                    TaskId = tasksDto.TaskId,
                    Title = tasksDto.Title,
                    Description = tasksDto.Description,
                };

                context.Tasks.Add(tasks);
                context.SaveChanges();

                return RedirectToAction("Index", "Tasks");
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while creating the task. Please try again later.";
                return View(tasksDto);
            }
        }

        public IActionResult Edit(int id)
        {
            try
            {


                var task = context.Tasks.Find(id);

                if (task == null)
                {
                    return RedirectToAction("Index", "Tasks");
                }

                //create tasksdto from tasks
                var tasksDto = new TasksDto()
                {
                    TaskId = task.TaskId,
                    Title = task.Title,
                    Description = task.Description,
                };

                ViewData["TasksId"] = task.Id;

                return View(tasksDto);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while fetching the task for editing. Please try again later.";
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public IActionResult Edit(int id, TasksDto tasksDto)
        {
            try
            {

                var task = context.Tasks.Find(id);

                if (task == null)
                {
                    return RedirectToAction("Index", "Tasks");
                }
                if (!ModelState.IsValid)
                {
                    ViewData["TasksId"] = task.Id;
                    return View(tasksDto);
                }

                //update task database
                task.Title = tasksDto.Title;
                task.Description = tasksDto.Description;

                context.SaveChanges();

                return RedirectToAction("Index", "Tasks");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while updating the task. Please try again later.";
                return View(tasksDto);
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                var task = context.Tasks.Find(id);

                if (task == null)
                {
                    return RedirectToAction("Index", "Tasks");
                }

                context.Tasks.Remove(task);
                context.SaveChanges(true);

                return RedirectToAction("Index", "Tasks");
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the task. Please try again later.";
                return RedirectToAction("Error");
            }

        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
