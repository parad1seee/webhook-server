using Microsoft.AspNetCore.Mvc;
using WebhookServer.Models.RequestModels;
using WebhookServer.Services.Interfaces;

namespace WebhookServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUsersService _usersService;

        public UsersController(ILogger<UsersController> logger,
            IUsersService usersService)
        {
            _logger = logger;
            _usersService = usersService;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetUsers()
        //{
        //    var users = await _usersService.GetUsers();

        //    return Ok(users);
        //}

        [HttpPatch]
        public async Task<IActionResult> ChangeUsersStatus([FromBody] List<UserStatusChangedRequestModel> users)
        {
            await _usersService.UpdateUsersStatus(users);

            return Ok();
        }
    }
}