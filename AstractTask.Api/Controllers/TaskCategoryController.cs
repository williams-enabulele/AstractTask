using AstractTask.Api.MiddleWares;
using AstractTask.Domain.DTOs;
using AstractTask.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AstractTask.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskCategoryController : Controller
    {
        private readonly ILogger<TaskCategoryController> _logger;
        private ITaskCategoryService _taskCategoryService;

        public TaskCategoryController(ILogger<TaskCategoryController> logger, ITaskCategoryService taskCategoryService)
        {
            _logger = logger;
            _taskCategoryService = taskCategoryService;
        }

        [HttpPost]
        [Route("AddCategory")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Policy = Policies.User)]
        public IActionResult AddTask([FromBody] CategoryDTO categoryDTO)
        {
            var result = _taskCategoryService.AddCategory(categoryDTO);
            return StatusCode(result.Result.StatusCode, result.Result);
        }

        [HttpGet]
        [Route("GetCategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Policy = Policies.AdminAndUser)]
        public IActionResult GetCategories()
        {
            var result = _taskCategoryService.GetCategories();
            return StatusCode(result.Result.StatusCode, result.Result);
        }
    }
}