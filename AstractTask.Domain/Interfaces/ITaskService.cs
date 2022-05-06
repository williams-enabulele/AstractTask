using AstractTask.Domain.DTOs;
using AstractTask.Domain.Entities;

namespace AstractTask.Domain.Interfaces
{
    public interface ITaskService
    {
        Task<Response<bool>> AddTask(TaskDTO taskDto);

        Task<Response<IEnumerable<TaskResponseDTO>>> GetTasks();

        Task<Response<bool>> UpdateTask(string id, UpdateTaskDTO taskDTO);

        Task<Response<bool>> DeleteTask(string id);

        Task<Response<IEnumerable<TaskResponseDTO>>> GetTasksByUser(string userId);
    }
}