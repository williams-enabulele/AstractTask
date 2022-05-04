using AstractTask.Domain.Entities;
using AstractTask.Domain.Interfaces;
using AstractTask.Infrastruture.Persistence;

namespace AstractTask.Infrastruture.Repository
{
    public class TaskCategoryRepository : GenericRepository<TaskCategory>, ITaskCategoryRepository
    {
        public TaskCategoryRepository(ApplicationContext context) : base(context)
        {
        }
    }
}