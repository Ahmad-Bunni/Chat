using Newtonsoft.Json;
using System;

namespace ChatApp.Domain.Model
{
    public abstract class BaseEntity
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        public DateTimeOffset CreatedDateTime { get; set; } = DateTime.UtcNow;
    }
}
