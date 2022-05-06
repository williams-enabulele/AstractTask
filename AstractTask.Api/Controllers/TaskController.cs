using AstractTask.Api.MiddleWares;
using AstractTask.Domain.DTOs;
using AstractTask.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AstractTask.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private readonly ILogger<TaskController> _logger;
        private ITaskService _taskService;
        private readonly IMapper _mapper;

        public TaskController(ILogger<TaskController> logger, ITaskService taskService, IMapper mapper)
        {
            _logger = logger;
            _taskService = taskService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("AddTask")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Policy = Policies.AdminAndUser)]
        public IActionResult AddTask([FromBody] TaskDTO taskDTO)
        {
            var result = _taskService.AddTask(taskDTO);
            return StatusCode(result.Result.StatusCode, result.Result);
        }

        [HttpPut]
        [Route("UpdateTask/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = Policies.AdminAndUser)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult UpdateTask([FromRoute] string id, [FromBody] UpdateTaskDTO taskDTO)
        {
            var result = _taskService.UpdateTask(id, taskDTO);
            return StatusCode(result.Result.StatusCode, result.Result);
        }

        [HttpGet]
        [Route("GetAllTask")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetAllTask()
        {
            var result = _taskService.GetTasks();
            return StatusCode(result.Result.StatusCode, result.Result);
        }

        [HttpGet]
        [Route("GetTask/{UserId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Policy = Policies.AdminAndUser)]
        public IActionResult GetTask([FromRoute] string UserId)
        {
            var result = _taskService.GetTasksByUser(UserId);
            return StatusCode(result.Result.StatusCode, result.Result);
        }

        [HttpDelete]
        [Route("DeleteTask/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Policy = Policies.AdminAndUser)]
        public IActionResult DeleteTask([FromRoute] string id)
        {
            var result = _taskService.DeleteTask(id);
            return StatusCode(result.Result.StatusCode, result.Result);
        }
    }
}