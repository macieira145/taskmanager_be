using CardExchange.DTO;
using CardExchange.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CardExchange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TaskmanagerContext _taskmanagerContext;

        public UserController(TaskmanagerContext taskmanagerContext)
        {
            _taskmanagerContext = taskmanagerContext;
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<List<UserDTO>>> Get()
        {
            
        }

    }
}
