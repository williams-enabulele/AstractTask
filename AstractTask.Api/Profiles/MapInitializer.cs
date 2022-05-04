using AstractTask.Domain.DTOs;
using AstractTask.Domain.Entities;
using AstractTask.Infrastruture.Identity;
using AutoMapper;
using TaskItem = AstractTask.Domain.Entities.TaskItem;

namespace AstractTask.Domain.Utilities
{
    public class MapInitializer : Profile
    {
        public MapInitializer()
        {
            CreateMap<ApplicationUser, RegisterDTO>().ReverseMap();
            CreateMap<ApplicationUser, LoginDTO>().ReverseMap();
            CreateMap<TaskItem, TaskDTO>().ReverseMap();
            CreateMap<TaskItem, UpdateTaskDTO>().ReverseMap();
            CreateMap<TaskCategory, CategoryDTO>().ReverseMap();
            CreateMap<CategoryResponseDTO, TaskCategory>().ReverseMap();
        }
    }
}