using Microsoft.EntityFrameworkCore;
using Taski_Website.Model;

namespace Taski_Website.Data
{
    public static class TaskBuilderExtensions
    {
        public static ModelBuilder TaskSeed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskiTask>().HasData(
                new TaskiTask
                {
                    TaskId = 1,
                    TaskName = "Test Task 1",
                    TaskDescription = "Big Task 1",
                    TaskStatus = "Active",
                    DueDate = DateTime.Now,
                },
                new TaskiTask
                {
                    TaskId = 2,
                    TaskName = "Test Task 2",
                    TaskDescription = "Big Task",
                    TaskStatus = "Failed",
                    DueDate = DateTime.Now,
                }

                );

            return modelBuilder;
        }
    }
}
