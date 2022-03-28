using WebhookServer.Models.Emuns;

namespace WebhookServer.Models.RequestModels
{
    public class UserStatusChangedRequestModel
    {
        public int Id { get; set; }

        public UserStatus GetUserStatus { get; set; }

        public string WebhookSecret { get; set; }
    }
}
