using AstractTask.Domain.DTOs;
using AstractTask.Domain.Entities;
using AstractTask.Domain.Interfaces;
using AutoMapper;
using System.Net;

namespace AstractTask.Infrastruture.Services
{
    public class TaskService : ITaskService
    {
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public TaskService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<bool>> AddTask(TaskDTO taskDto)
        {
            var taskItem = _mapper.Map<TaskItem>(taskDto);
            var response = new Response<bool>();
            try
            {
                _unitOfWork.TaskItem.Add(taskItem);
                _unitOfWork.Complete();
                response.StatusCode = (int)HttpStatusCode.Created;
                response.Message = "Task Created";
                response.Succeeded = true;

                return response;
            }
            catch (Exception ex)
            {
                response.Message = "An error occured";
                response.Succeeded = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                return response;
            }
        }

        public async Task<Response<IEnumerable<TaskResponseDTO>>> GetTasks()
        {
            var response = new Response<IEnumerable<TaskResponseDTO>>();
            try
            {
                var tasks = _unitOfWork.TaskItem.GetAll();
                var items = _mapper.Map<IEnumerable<TaskResponseDTO>>(tasks);

                response.StatusCode = (int)HttpStatusCode.OK;
                response.Message = "Successfully fetched";
                response.Succeeded = true;
                response.Data = items;
                return response;
            }
            catch (Exception)
            {
                response.Message = "An error occured";
                response.Succeeded = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                return response;
            }
        }

        public async Task<Response<bool>> UpdateTask(string id, UpdateTaskDTO taskDTO)
        {
            var item = _unitOfWork.TaskItem.GetById(id);
            var response = new Response<bool>();
            if (item != null)
            {
                var taskItem = _mapper.Map<TaskItem>(taskDTO);
                try
                {
                    _unitOfWork.TaskItem.Update(taskItem);
                    _unitOfWork.Complete();
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Task Updated";
                    response.Succeeded = true;
                    return response;
                }
                catch (Exception ex)
                {
                    response.Message = "Task was not updated!";
                    response.Succeeded = false;
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return response;
                }
            }
            else
            {
                response.Message = "An error occured, ensure an id is sent along with the request";
                response.Succeeded = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                return response;
            }
        }

        public async Task<Response<bool>> DeleteTask(string id)
        {
            var response = new Response<bool>();
            try
            {
                var task = _unitOfWork.TaskItem.GetById(id);
                _unitOfWork.TaskItem.Remove(task);
                _unitOfWork.Complete();
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Message = "Task Deleted";
                response.Succeeded = true;
                return response;
            }
            catch (Exception)
            {
                response.Message = "An error occured";
                response.Succeeded = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                return response;
            }
        }

        public async Task<Response<IEnumerable<TaskResponseDTO>>> GetTasksByUser(string userId)
        {
            var response = new Response<IEnumerable<TaskResponseDTO>>();
            try
            {
                var userTasks = _unitOfWork.TaskItem.Find(x => x.UserId == userId);
                var items = _mapper.Map<IEnumerable<TaskResponseDTO>>(userTasks);
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Message = "Successfully fetched";
                response.Succeeded = true;
                response.Data = items;
                return response;
            }
            catch (Exception)
            {
                response.Message = "An error occured";
                response.Succeeded = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                return response;
            }
        }
    }
}