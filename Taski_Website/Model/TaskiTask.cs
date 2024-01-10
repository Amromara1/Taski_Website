using System.ComponentModel.DataAnnotations;

namespace Taski_Website.Model
{
    public class TaskiTask
    {
        [Key]
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string TaskStatus { get; set; }
        public DateTime DueDate { get; set; }
        //public List<TaskiUser> User { get; set; }
        public List<UserTask> UserTask { get; set; }

    }
}
