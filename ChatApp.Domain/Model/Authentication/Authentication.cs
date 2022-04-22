using System;

namespace ChatApp.Domain.Model.Authentication;

public record Authentication
{
    public string Token { get; init; }
    public DateTime? ExpirationDate { get; init; }
}
