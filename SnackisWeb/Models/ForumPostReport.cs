using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SnackisWeb.Models;

public class ForumPostReport
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("forumPost")]
    public ForumPost ForumPost { get; set; } = null!;
    [JsonPropertyName("reporter")]
    public PublicUser Reporter { get; set; } = null!;
    [JsonPropertyName("reason")]
    public string Reason { get; set; } = null!;
    [JsonPropertyName("date")]
    public DateTime Date { get; set; } = DateTime.Now;
    [JsonPropertyName("threadTitle")]
    public string ThreadTitle { get; set; } = null!;
}

public class CreateForumPostReport
{
    [JsonPropertyName("forumPostId")]
    public int ForumPostId { get; set; }
    [JsonPropertyName("reason")]
    public string Reason { get; set; } = null!;
}
