namespace ChatApp.Domain.Model.Authentication;

public record AuthSettings
{
    public string Secret { get; init; }
}
