﻿using ChatApp.Domain.Model;

namespace ChatApp.Model.Messaging;

public record Message : BaseEntity
{
    public string Content { get; init; }
    public string Username { get; init; }
    public bool IsServer { get; init; }
    public string GroupName { get; init; }
}
