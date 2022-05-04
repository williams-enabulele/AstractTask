using AstractTask.Domain.DTOs;
using AstractTask.Domain.Entities;
using AstractTask.Domain.Interfaces;
using AutoMapper;
using System.Net;

namespace AstractTask.Infrastruture.Services
{
    public class TaskCategoryService : ITaskCategoryService
    {
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public TaskCategoryService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<bool>> AddCategory(CategoryDTO categoryDto)
        {
            var category = _mapper.Map<TaskCategory>(categoryDto);
            var response = new Response<bool>();
            try
            {
                _unitOfWork.Category.Add(category);
                _unitOfWork.Complete();
                response.StatusCode = (int)HttpStatusCode.Created;
                response.Message = "Category Created";
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

        public async Task<Response<IEnumerable<CategoryResponseDTO>>> GetCategories()
        {
            var response = new Response<IEnumerable<CategoryResponseDTO>>();
            try
            {
                var categories = _unitOfWork.Category.GetAll();
                var category = _mapper.Map<IEnumerable<CategoryResponseDTO>>(categories);
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Message = "Successfully fetched";
                response.Succeeded = true;
                response.Data = category;
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