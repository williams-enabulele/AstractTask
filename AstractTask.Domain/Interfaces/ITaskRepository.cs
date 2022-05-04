using TaskItem = AstractTask.Domain.Entities.TaskItem;

namespace AstractTask.Domain.Interfaces
{
    public interface ITaskRepository : IGenericRepository<TaskItem>
    {
    }
}