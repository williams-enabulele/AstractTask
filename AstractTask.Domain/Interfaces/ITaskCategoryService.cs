using AstractTask.Domain.DTOs;
using AstractTask.Domain.Entities;

namespace AstractTask.Domain.Interfaces
{
    public interface ITaskCategoryService
    {
        Task<Response<bool>> AddCategory(CategoryDTO categoryDto);

        Task<Response<IEnumerable<CategoryResponseDTO>>> GetCategories();
    }
}