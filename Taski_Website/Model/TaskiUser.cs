using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;

namespace Taski_Website.Model
{
    public class TaskiUser : IdentityUser<int>
    {
        [Key]
        public int UserId { get; set; }
        //public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        //public List<TaskiTask> Task {  get; set; }
        public List<UserTask> UserTask { get; set; }

    }
}
