using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;

//template only. feel free to edit
namespace Abc.HabitTracker.Api
{
  public class RequestData
  {
    [JsonPropertyName("name")]
    public String Name { get; set; }

    [JsonPropertyName("days_off")]
    public String[] DaysOff { get; set; }
  }

  public class Habit
  {
    [JsonPropertyName("id")]
    public Guid ID { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("days_off")]
    public string[] DaysOff { get; set; }

    [JsonPropertyName("current_streak")]
    public int CurrentStreak { get; set; }

    [JsonPropertyName("longest_streak")]
    public int LongestStreak { get; set; }

    [JsonPropertyName("log_count")]
    public int LogCount { get; set; }

    [JsonPropertyName("logs")]
    public DateTime[] Logs { get; set; }

    [JsonPropertyName("user_id")]
    public Guid UserID { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

  }
}