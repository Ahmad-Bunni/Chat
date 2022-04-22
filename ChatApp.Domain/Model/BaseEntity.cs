using System;
using System.Text.Json.Serialization;

namespace ChatApp.Domain.Model
{
    public abstract record BaseEntity
    {
        [JsonPropertyName("id")]
        public string Id { get; init; }

        public DateTimeOffset CreatedDateTime { get; init; } = DateTime.UtcNow;
    }
}
