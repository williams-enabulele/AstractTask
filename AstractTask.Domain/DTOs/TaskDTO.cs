namespace AstractTask.Domain.DTOs
{
    public class TaskDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string DeadLine { get; set; }
        public string TaskCategoryId { get; set; }
        public string UserId { get; set; }
        public Boolean IsCompleted { get; set; }
    }
}