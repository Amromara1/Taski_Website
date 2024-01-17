using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using Taski_Website.Data;
using Taski_Website.Model;

namespace Taski_Website.Pages
{
    public class TaskModel : PageModel
    {
        private WebseiteContext context;

        public TaskModel(WebseiteContext webseitenContext)
        {
            this.context = webseitenContext;
        }
        public List<TaskiTask> Tasks { get; set; } = new();

        public async Task OnGetAsync()
        {
            
            //IQueryable<TaskiTask> query = context.Tasks;

            // Execute the final query
            Tasks = await context.Tasks.ToListAsync();
        }

    }
}
