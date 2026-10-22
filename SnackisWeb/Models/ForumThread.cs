using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SnackisWeb.Models;


public class CreateThreadRequest
{
    [JsonPropertyName("title")]
    public string Title { get; set; } = "";
    [JsonPropertyName("content")]
    public string Content { get; set; } = "";
    [JsonPropertyName("categoryId")]
    public int CategoryId { get; set; }
}

public class UpdateThreadRequest
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = "";
}

public class ForumThread
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("title")]
    public string Title { get; set; } = "";
    [JsonPropertyName("date")]
    public DateTime Date { get; set; }
    [JsonPropertyName("categoryId")]
    public int CategoryId { get; set; }
    [JsonPropertyName("userId")]
    public string UserId { get; set; } = "";
    [JsonPropertyName("userName")]
    public string UserName { get; set; } = "";
    [JsonPropertyName("profileImage")]
    public string? ProfileImage { get; set; }
    [JsonPropertyName("posts")]
    public List<ForumPost> Posts { get; set; } = new List<ForumPost>();
}