using CardExchange.Entities;
using CardExchange.Interfaces;
using CardExchange.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CardExchange.Controllers
{
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
            var tasks = _taskRepository.GetTasks();

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
        public IActionResult Gettask(int taskId)
        {
            // checks if task exists
            if (!_taskRepository.TaskExists(taskId))
                return NotFound();

            // gets the task 
            var task = _taskRepository.GetTask(taskId);

            // checks if task meets all the requirements
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // return the task 
            return Ok(task);
        }
    }
}
