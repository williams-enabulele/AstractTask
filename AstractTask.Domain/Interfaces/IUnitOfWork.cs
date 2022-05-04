namespace AstractTask.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ITaskRepository TaskItem { get; }
        ITaskCategoryRepository Category { get; }

        int Complete();
    }
}