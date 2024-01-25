using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Security.Claims;
using Taski_Website.Data;
using Taski_Website.Model;

namespace Taski_Website.Pages
{
    //[Authorize]
    public class TaskModel : PageModel
    {
        private WebseiteContext context;

        [BindProperty]
        public int TaskId { get; set; }
        [BindProperty, Required, Display(Name = "Please add a Title to the Task")]
        public string Title { get; set; }
        [BindProperty, Required, Display(Name = "Please add a Description to the Task")]
        public string Description { get; set; }
        [BindProperty, Required, Display(Name = "Please add a DueDate to the Task")]
        public DateTime DueDate { get; set; }
        public TaskModel(WebseiteContext webseitenContext)
        {
            this.context = webseitenContext;
        }
        public List<TaskiTask> Tasks { get; set; } = new();
        public string UserEmail { get; private set; }

        public async Task OnGetAsync()
        {

            //IQueryable<TaskiTask> query = context.Tasks;

            // Execute the final query
            UserEmail = HttpContext.Session.GetString("UserEmail") ?? "";
            Tasks = await context.Tasks.ToListAsync();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var test = TaskId;
            if (TaskId == 0)
            {
                var Task = new TaskiTask();
                Task.TaskId = this.context.Tasks.Max(x => x.TaskId) + 1;
                Task.TaskName = this.Title;
                Task.TaskDescription = this.Description;
                Task.DueDate = this.DueDate;


                //if (!ModelState.IsValid)
                //{
                //    return Page();
                //}

                await this.context.Tasks.AddAsync(Task);
                await this.context.SaveChangesAsync();

            }
            else
            {
                TaskiTask existingTask = await this.context.Tasks.FindAsync(this.TaskId);
                if (existingTask != null)
                {
                    // If the task with the given TaskId exists, update it
                    existingTask.TaskName = this.Title;
                    existingTask.TaskDescription = this.Description;
                    existingTask.DueDate = this.DueDate;

                    await this.context.SaveChangesAsync();
                }
            }


            return RedirectToPage("Task");
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var test = TaskId;
            if (TaskId != null)
            {
                TaskiTask existingTask = await this.context.Tasks.FindAsync(this.TaskId);
                if (existingTask != null)
                {
                    // If the task with the given TaskId exists, update it
                    existingTask.TaskName = this.Title;
                    existingTask.TaskDescription = this.Description;
                    existingTask.DueDate = this.DueDate;
                    this.context.Tasks.Remove(existingTask);
                    await this.context.SaveChangesAsync();
                }
            }
            return RedirectToPage("Task");
        }

    }
}
