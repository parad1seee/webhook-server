using Microsoft.EntityFrameworkCore;
using WebhookServer.DAL;
using WebhookServer.Models.Emuns;
using WebhookServer.Models.RequestModels;
using WebhookServer.Models.ResponseModel;
using WebhookServer.Services.Interfaces;

namespace WebhookServer.Services.Services
{
    public class UsersService : IUsersService
    {
        private readonly DataContext _dataContext;

        public UsersService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<UserResponseModel>> GetUsers()
        {
            return await _dataContext.Users
                .Where(x => x.Status == UserStatus.Waiting)
                .Take(10)
                .Select(x => new UserResponseModel
                {
                    Id = x.Id,
                    Email = x.Email,
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task UpdateUsersStatus(List<UserStatusChangedRequestModel> users)
        {
            var usersIds = users.Select(x => x.Id);

            var usersToUpdate = await _dataContext.Users
                .Where(x => usersIds.Contains(x.Id))
                .ToListAsync();

            usersToUpdate.ForEach(x =>
            {
                x.Status = UserStatus.Delivered;
            });

            await _dataContext.SaveChangesAsync();
        }
    }
}
