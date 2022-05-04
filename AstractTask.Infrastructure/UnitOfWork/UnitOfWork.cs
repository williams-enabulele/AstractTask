using AstractTask.Domain.Interfaces;
using AstractTask.Infrastruture.Persistence;
using AstractTask.Infrastruture.Repository;

namespace AstractTask.Infrastruture.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        private ITaskRepository _taskRepository;
        private ITaskCategoryRepository _taskCategoryRepository;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }

        public ITaskRepository TaskItem => _taskRepository ??= new TaskRepository(_context);
        public ITaskCategoryRepository Category => _taskCategoryRepository ??= new TaskCategoryRepository(_context);

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}