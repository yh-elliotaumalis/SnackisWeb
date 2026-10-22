using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SnackisWeb.Models;

public class PublicUser
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = null!;
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;
    [JsonPropertyName("profileImage")]
    public string? ProfileImage { get; set; }
}

public class User
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = null!;
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;
    [JsonPropertyName("email")]
    public string Email { get; set; } = null!;
    [JsonPropertyName("roles")]
    public IList<string> Roles { get; set; } = null!;
    [JsonPropertyName("profileImage")]
    public string? ProfileImage { get; set; }
}

public class UpdateUserRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;
}