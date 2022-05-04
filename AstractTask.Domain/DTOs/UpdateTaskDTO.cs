namespace AstractTask.Domain.DTOs
{
    public class UpdateTaskDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DeadLine { get; set; }
        public string TaskCategoryId { get; set; }
        public string UserId { get; set; }
        public Boolean IsCompleted { get; set; }
    }
}