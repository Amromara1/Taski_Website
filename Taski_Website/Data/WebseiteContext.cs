using Microsoft.EntityFrameworkCore;
using Taski_Website.Data.Configurations;
using Taski_Website.Model;

namespace Taski_Website.Data
{
    public class WebseiteContext : DbContext
    {
        public DbSet<TaskiTask> Tasks { get; set; }
        public DbSet<TaskiUser> Users { get; set; }
        public DbSet<UserTask> UserTask { get; set; }
        public WebseiteContext(DbContextOptions<WebseiteContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(@"Data source=TaskiWebsite.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new UserConfiguration()).UserSeed();
            //modelBuilder.ApplyConfiguration(new UserConfiguration()).TaskSeed();

            modelBuilder.Entity<UserTask>()
                .HasOne(s => s.Task)
                .WithMany(fs => fs.UserTask)
                .HasForeignKey(f => f.TaskId);
            modelBuilder.Entity<UserTask>()
                .HasOne(s => s.User)
                .WithMany(fs => fs.UserTask)
                .HasForeignKey(f => f.UserId);
        }
    }
}
