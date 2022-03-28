using Microsoft.AspNetCore.Mvc;
using WebhookServer.DAL;
using WebhookServer.Models.ResponseModel;

namespace WebhookServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly DataContext _dataContext;

        public UsersController(ILogger<UsersController> logger,
            DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<UserResponseModel> Get()
        {
            return _dataContext.Users.Select(x => new UserResponseModel
            {
                Id = x.Id,
                Email = x.Email,
            });
        }
    }
}