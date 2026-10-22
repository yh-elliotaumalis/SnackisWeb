using System.Text.Json.Serialization;

namespace SnackisWeb.Models;

public class ForumMessage
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("content")]
    public string Content { get; set; } = null!;
    [JsonPropertyName("date")]
    public DateTime Date { get; set; }
    [JsonPropertyName("sender")]
    public PublicUser Sender { get; set; } = null!;
    [JsonPropertyName("recipient")]
    public PublicUser Recipient { get; set; } = null!;
}

public class CreateMessageRequest
{
    [JsonPropertyName("recipientId")]
    public string RecipientId { get; set; } = null!;
    [JsonPropertyName("content")]
    public string Content { get; set; } = null!;
}
