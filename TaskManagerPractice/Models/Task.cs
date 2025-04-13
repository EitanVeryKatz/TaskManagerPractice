using System.ComponentModel.DataAnnotations;

namespace TaskManagerPractice.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        public string TaskName { get; set; }


        public string? TaskDescription { get; set; } = "";

        [Required]
        public DateOnly DeadlineDate { get; set; }

        public bool WasCompleted { get; set; }
    }
}
