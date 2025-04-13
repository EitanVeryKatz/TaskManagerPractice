using Microsoft.EntityFrameworkCore;

namespace TaskManagerPractice.Models
{
    public class TasksDb:DbContext
    {
        public TasksDb(DbContextOptions<TasksDb> options)
            : base(options)
        { 
        }

        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<TaskItem> CompletedTasks { get; set; }
        public DbSet<TaskItem> NotCompletedTasks { get; set; }

    }
}
