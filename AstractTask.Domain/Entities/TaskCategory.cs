using System.ComponentModel.DataAnnotations;

namespace AstractTask.Domain.Entities
{
    public class TaskCategory : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        public List<TaskItem> Tasks { get; set; }
    }
}