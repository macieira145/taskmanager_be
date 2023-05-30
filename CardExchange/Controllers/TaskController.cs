using CardExchange.Dto;
using CardExchange.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CardExchange.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : Controller
    {
        private readonly ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        /// <summary>
        /// Gets the list of tasks from DB
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Entities.Task>))]
        public IActionResult GetTasks()
        {
            // gets the id of the authenticated user
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);

            var tasks = _taskRepository.GetTasks(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(tasks);
        }


        /// <summary>
        /// End-point to get task by its ID
        /// </summary>
        /// <param name="taskId">ID of the task to get</param>
        /// <returns>Returns the task</returns>
        [HttpGet("{taskId}")]
        [ProducesResponseType(200, Type = typeof(Entities.Task))]
        [ProducesResponseType(400)]
        public IActionResult GetTask(int taskId)
        {
            // gets the id of the authenticated user
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);

            // checks if task exists
            if (!_taskRepository.TaskExists(taskId, id))
                return NotFound();

            // gets the task 
            var task = _taskRepository.GetTask(taskId, id);

            // checks if task meets all the requirements
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // return the task 
            return Ok(task);
        }

        /// <summary>
        /// End-point to create task
        /// </summary>
        /// <param name="taskDTO">DTO of task</param>
        /// <returns>response</returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Entities.Task))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTask([FromBody] TaskDTO taskDTO)
        {
            // checks if user has data
            if (taskDTO == null)
                return BadRequest(ModelState);

            // checks if data is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // id of auth user
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);

            // creates a new task
            var task = new Entities.Task(taskDTO);

            // sets the user id
            task.UsersId = id;

            // tries to create the task
            if (_taskRepository.CreateTask(task) is null)
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            // returns a json response
            return Ok(new
            {
                status = 200,
                message = "Task created successfully",
                data = task
            });
        }

        [HttpPut("{taskId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTask(int taskId,
            [FromBody] TaskDTO taskDTO)
        {
            // gets the id of the authenticated user
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);

            if (taskDTO == null)
                return BadRequest(ModelState);

            if (taskId != taskDTO.Id)
                return BadRequest(ModelState);

            if (!_taskRepository.TaskExists(taskId, id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var task = new Entities.Task(taskDTO);
            task.UsersId = id;

            if (_taskRepository.UpdateTask(task) is null)
            {
                ModelState.AddModelError("", "Something went wrong updating owner");
                return StatusCode(500, ModelState);
            }

            // returns a json response
            return Ok(new
            {
                status = 200,
                message = "Task updated successfully",
                data = task
            });
        }


        /// <summary>
        /// End-point to create task
        /// </summary>
        /// <param name="taskDTO">DTO of task</param>
        /// <returns>response</returns>
        [HttpDelete("{taskId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DeleteTask(int taskId)
        {
            // id of auth user
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);

            var task = _taskRepository.GetTask(taskId, id);

            // checks if user has data
            if (task == null)
                return BadRequest(ModelState);

            // checks if data is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // tries to create the task
            if (!_taskRepository.DeleteTask(task))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            // response
            return Ok(new
            {
                status = 200,
                message = "Task deleted successfully"
            });
        }
    }
}
