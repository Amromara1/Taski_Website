using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taski_Website.Model;

namespace Taski_Website.Data.Configurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<TaskiTask>
    {
        public void Configure(EntityTypeBuilder<TaskiTask> builder)
        {
            builder.HasKey(p => p.TaskId);
            builder.Property(p => p.TaskName).HasColumnName("TaskName");
            //builder.Property(p => p.DueDate).HasColumnName("DueDate");


        }
    }
}
