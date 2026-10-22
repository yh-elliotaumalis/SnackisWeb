using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SnackisWeb.Models;

public class ForumPost
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("body")]
    public string Body { get; set; }
    [JsonPropertyName("date")]
    public DateTime Date { get; set; }
    [JsonPropertyName("userId")]
    public string UserId { get; set; }
    [JsonPropertyName("userName")]
    public string UserName { get; set; }
    [JsonPropertyName("profileImage")]
    public string? ProfileImage { get; set; }
}

public class CreatePostRequest
{
    [JsonPropertyName("body")]
    public string Body { get; set; } = "";
    [JsonPropertyName("threadId")]
    public int ThreadId { get; set; }
}

public class UpdatePostRequest
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("body")]
    public string Body { get; set; } = "";
}