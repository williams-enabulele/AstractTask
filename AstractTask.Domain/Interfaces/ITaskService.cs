using AstractTask.Domain.DTOs;
using AstractTask.Domain.Entities;

namespace AstractTask.Domain.Interfaces
{
    public interface ITaskService
    {
        Task<Response<bool>> AddTask(TaskDTO taskDto);

        Task<Response<IEnumerable<TaskDTO>>> GetTasks();

        Task<Response<bool>> UpdateTask(UpdateTaskDTO taskDTO);

        Task<Response<bool>> DeleteTask(string id);

        Task<Response<IEnumerable<TaskDTO>>> GetTasksByUser(string userId);
    }
}