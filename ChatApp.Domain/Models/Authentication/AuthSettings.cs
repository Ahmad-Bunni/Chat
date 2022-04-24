namespace ChatApp.Domain.Models.Authentication;

public record AuthSettings
{
    public string Secret { get; init; }
}
