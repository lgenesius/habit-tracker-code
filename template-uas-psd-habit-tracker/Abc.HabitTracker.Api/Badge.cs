using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;

//template only. feel free to edit
namespace Abc.HabitTracker.Api
{
  public class Badge
  {
    [JsonPropertyName("id")]
    public Guid ID { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("user_id")]
    public Guid UserID { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    public Badge(Guid id, string name, string description, Guid userid, DateTime createdAt){
      this.ID = id;
      this.Name = name;
      this.Description = description;
      this.UserID = userid;
      this.CreatedAt = createdAt;
    }
  }
}
