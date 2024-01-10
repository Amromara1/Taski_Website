namespace Taski_Website.Model
{
    public class UserTask
    {
        public int Id { get; set; }
        public int TaskId {  get; set; }
        public int UserId { get; set; }

        public virtual TaskiTask Task { get; set; }
        public virtual TaskiUser User { get; set; }

    }
}
