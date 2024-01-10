namespace Taski_Website.Model
{
    public class Task
    {
        public string TaskId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string TaskStatus { get; set; }
        public DateTime DueDate { get; set; }
        public List<User> user {  get; set; }

    }
}
