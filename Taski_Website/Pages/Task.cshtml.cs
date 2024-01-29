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
    [Authorize]
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

        [BindProperty]
        public string AssignedUserIds { get; set; }

        private List<int> _assignedUserIds;
        public List<int> AssignedUserIdsList
        {
            get
            {
                if (_assignedUserIds == null)
                {
                    _assignedUserIds = AssignedUserIds?.Split("-").Select(idStr => int.Parse(idStr)).ToList() ?? new List<int>();
                }
                return _assignedUserIds;
            }

        }

        public TaskModel(WebseiteContext webseitenContext)
        {
            this.context = webseitenContext;
        }
        public List<TaskiTask> Tasks { get; set; } = new();
        public List<TaskiUser> Students { get; set; } = new();
        public List<UserTask> UserTasks { get; set; } = new();
        public string UserId { get; private set; }
        public string UserEmail { get; private set; }
        public string UserName { get; private set; }
        public string UserRole { get; private set; }
        public async Task OnGetAsync()
        {

            //IQueryable<TaskiTask> query = context.Tasks;
            UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            UserEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            UserRole = User.FindFirst(ClaimTypes.Role)?.Value;
            UserName = User.FindFirst(ClaimTypes.Name)?.Value;

            // Execute the final query
            //UserEmail = HttpContext.Session.GetString("UserEmail") ?? "";
            Tasks = await context.Tasks.ToListAsync();
            Students = await context.Users.Where(Stu => Stu.Role == "student").ToListAsync();
            
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var test1 = AssignedUserIdsList;
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

                Students = await context.Users
                                         .Where(Stu => Stu.Role == "student" && (AssignedUserIdsList == null || AssignedUserIdsList.Contains(Stu.UserId)))
                                         .ToListAsync();

                if (Students != null && Students.Any())
                {
                    // Add logic to assign the task to selected users
                    foreach (var student in Students)
                    {
                        // Create a UserTask entity and save it to the database
                        var userTask = new UserTask { UserId = student.UserId, TaskId = Task.TaskId }; 
                        context.UserTask.Add(userTask);
                    }

                }

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

                    // Update UserTask relationships
                    var existingUserTasks = context.UserTask.Where(ut => ut.TaskId == existingTask.TaskId).ToList();

                    // Remove existing relationships
                    context.UserTask.RemoveRange(existingUserTasks);

                    // Add new relationships based on selected users
                    Students = await context.Users
                                             .Where(Stu => Stu.Role == "student" && (AssignedUserIdsList == null || AssignedUserIdsList.Contains(Stu.UserId)))
                                             .ToListAsync();

                    if (Students != null && Students.Any())
                    {
                        foreach (var student in Students)
                        {
                            var newUserTask = new UserTask { UserId = student.UserId, TaskId = existingTask.TaskId };
                            context.UserTask.Add(newUserTask);
                        }
                    }

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


                    var existingUserTasks = context.UserTask.Where(ut => ut.TaskId == existingTask.TaskId).ToList();
                    context.UserTask.RemoveRange(existingUserTasks);

                    await this.context.SaveChangesAsync();
                }
            }
            return RedirectToPage("Task");
        }

        public async Task<string> GetUsersForTask(int taskId)
        {
            // Assuming you have a UserTask DbSet in your DbContext
            var userTasks = await context.UserTask
                .Where(ut => ut.TaskId == taskId)
                .Include(ut => ut.User) // Ensure User navigation property is loaded
                .ToListAsync();

            // Extract the users from the UserTask entities
            List<TaskiUser> users = userTasks.Select(ut => ut.User).ToList();
            string usersOut = string.Empty;
            if (users != null)
            {
                foreach (var user in users)
                {

                    usersOut += user.UserId;
                    usersOut += "-";
                }
            }
            return usersOut;
        }

    }
}
