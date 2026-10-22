using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SnackisWeb.Models;

public class CreateForumCategory
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;
    [JsonPropertyName("topic")]
    public string Topic { get; set; } = null!;
    [JsonPropertyName("priority")]
    public int? Priority { get; set; } = 0;
    [JsonPropertyName("parentCategoryId")]
    public int? ParentCategoryId { get; set; } = null!;
}

public class UpdateForumCategory
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;
    [JsonPropertyName("topic")]
    public string Topic { get; set; } = null!;
    [JsonPropertyName("priority")]
    public int? Priority { get; set; } = null!;
    [JsonPropertyName("parentCategoryId")]
    public int? ParentCategoryId { get; set; } = null!;
}

public class ForumCategory
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;
    [JsonPropertyName("topic")]
    public string Topic { get; set; } = null!;
    [JsonPropertyName("priority")]
    public int? Priority { get; set; } = null!;
    [JsonPropertyName("parentCategoryId")]
    public int? ParentCategoryId { get; set; } = null!;
}