using ChatApp.Domain.Model;

namespace ChatApp.Model
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string ConnectionId { get; set; }
        public string GroupName { get; set; }

    }
}
