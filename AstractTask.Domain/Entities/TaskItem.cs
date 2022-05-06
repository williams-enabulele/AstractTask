using System.ComponentModel.DataAnnotations;

namespace AstractTask.Domain.Entities
{
    public class TaskItem : BaseEntity
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
        public Boolean IsCompleted { get; set; }
        public string DeadLine { get; set; }
        public string TaskCategoryId { get; set; }
        public string UserId { get; set; }
        public TaskCategory TaskCategory { get; set; }
    }
}