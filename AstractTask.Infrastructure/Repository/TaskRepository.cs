using AstractTask.Domain.Interfaces;
using AstractTask.Infrastruture.Persistence;
using TaskItem = AstractTask.Domain.Entities.TaskItem;

namespace AstractTask.Infrastruture.Repository
{
    public class TaskRepository : GenericRepository<TaskItem>, ITaskRepository
    {
        public TaskRepository(ApplicationContext context) : base(context)
        {
        }
    }
}