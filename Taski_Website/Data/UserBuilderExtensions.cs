using Microsoft.EntityFrameworkCore;
using Taski_Website.Model;

namespace Taski_Website.Data
{
    public static class UserBuilderExtensions
    {
        public static ModelBuilder UserSeed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskiUser>().HasData(
                new TaskiUser
                {
                    UserId = 1,
                    //UserName = "Muster_Teacher",
                    Email = "Muster_Teacher@Taski.de",
                    Password = "123",
                    Role = "Teacher",
                },
                new TaskiUser
                {
                    UserId = 2,
                    //UserName = "Muster_Student",
                    Email = "Muster_Student@Taski.de",
                    Password = "123",
                    Role = "Student",
                }

                );

            return modelBuilder;
        }
    }
}
