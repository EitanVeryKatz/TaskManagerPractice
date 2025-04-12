using Microsoft.EntityFrameworkCore;

namespace TaskManagerPractice.Models
{
    public class TasksDb:DbContext
    {
        public TasksDb(DbContextOptions<TasksDb> options)
            : base(options)
        { 
        }

        public DbSet<Task> Tasks { get; set; }
    }
}
