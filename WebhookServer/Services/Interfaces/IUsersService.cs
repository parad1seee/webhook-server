using WebhookServer.Models.RequestModels;
using WebhookServer.Models.ResponseModel;

namespace WebhookServer.Services.Interfaces
{
    public interface IUsersService
    {
        public Task<List<UserResponseModel>> GetUsers();

        public Task UpdateUsersStatus(List<UserStatusChangedRequestModel> users);
    }
}
