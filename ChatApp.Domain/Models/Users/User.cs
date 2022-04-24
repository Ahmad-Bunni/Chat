using ChatApp.Domain.Models;

namespace ChatApp.Domain.Models.Users;

public record User : BaseEntity
{
    public string Username { get; init; }
    public string ConnectionId { get; init; }
    public string GroupName { get; init; }
}
