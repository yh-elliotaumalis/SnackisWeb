using SnackisWeb.Interfaces;
using SnackisWeb.Models;
using System.Text.Json;

namespace SnackisWeb.Services;

public class ForumAPIService : IForumAPIService
{
    private readonly HttpClient _client;

    public ForumAPIService(IHttpClientFactory httpClientFactory)
    {
        _client = httpClientFactory.CreateClient("ForumClient");
    }

    private async Task<T> GetAsync<T>(string url)
    {
        var response = await _client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(content);
    }

    public async Task<AccessTokenResponse?> Login(LoginRequest loginRequest)
    {
        var response = await _client.PostAsJsonAsync("/identity/login", loginRequest);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadFromJsonAsync<AccessTokenResponse>();
    }

    public async Task<bool> Register(RegisterRequest registerRequest)
    {
        var response = await _client.PostAsJsonAsync("/identity/register", registerRequest);
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> CreateForumCategory(CreateForumCategory createForumCategory)
    {
        var response = await _client.PostAsJsonAsync("/api/categories", createForumCategory);
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }
        return true;
    }

    public async Task<ForumPost> CreateForumPost(CreatePostRequest createPostRequest)
    {
        var response = await _client.PostAsJsonAsync("/api/posts", createPostRequest);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ForumPost>();
    }

    public async Task<bool> CreateForumPostReport(CreateForumPostReport createForumPostReport)
    {
        var response = await _client.PostAsJsonAsync("/api/reports", createForumPostReport);
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }
        return true;
    }

    public async Task<ForumThread?> CreateForumThread(CreateThreadRequest createForumThread)
    {
        var response = await _client.PostAsJsonAsync("/api/threads", createForumThread);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<ForumThread>();
    }

    public async Task<bool> DeleteForumCategory(int id)
    {
        var response = await _client.DeleteAsync($"/api/categories/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> DeleteForumPost(int id)
    {
        var response = await _client.DeleteAsync($"/api/posts/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> DeleteForumPostReport(int id)
    {
        var response = await _client.DeleteAsync($"/api/reports/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> DeleteForumThread(int id)
    {
        var response = await _client.DeleteAsync($"/api/threads/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }
        return true;
    }

    public async Task<List<ForumCategory>> GetForumCategories()
    {
        var categories = await GetAsync<List<ForumCategory>>("api/categories");
        return categories;
    }

    public async Task<ForumPost> GetForumPost(int id)
    {
        var post = await GetAsync<ForumPost>($"api/posts/{id}");
        return post;
    }

    public Task<ForumPostReport> GetForumPostReport(int id)
    {
        var report = GetAsync<ForumPostReport>($"api/reports/{id}");
        return report;
    }

    public Task<List<ForumPostReport>> GetForumPostReports()
    {
        var reports = GetAsync<List<ForumPostReport>>("api/reports");
        return reports;
    }

    public async Task<ForumThread> GetForumThread(int id)
    {
        var thread = await GetAsync<ForumThread>($"api/threads/{id}");
        return thread;
    }

    public async Task<List<ForumMessage>> GetForumMessages()
    {
        var messages = await GetAsync<List<ForumMessage>>("api/messages");
        return messages;
    }

    public async Task<ForumMessage> CreateForumMessage(CreateMessageRequest createMessageRequest)
    {
        var response = await _client.PostAsJsonAsync("/api/messages", createMessageRequest);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ForumMessage>();
    }

    public async Task<List<ForumThread>> GetForumThreads()
    {
        var threads = await GetAsync<List<ForumThread>>("api/threads");
        return threads;
    }

    public async Task<List<PublicUser>> GetForumUsers()
    {
        var users = await GetAsync<List<PublicUser>>("api/users");
        return users;
    }

    public async Task<PublicUser> GetForumUser(string id)
    {
        var user = await GetAsync<PublicUser>($"api/users/{id}");
        return user;
    }

    public async Task<User?> GetUser(string accessToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/me");
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
        var response = await _client.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
        return await response.Content.ReadFromJsonAsync<User>();
    }

    public async Task<bool> UpdateForumCategory(UpdateForumCategory updateForumCategory)
    {
        var request = await _client.PutAsJsonAsync($"/api/categories/{updateForumCategory.Id}", updateForumCategory);
        if (!request.IsSuccessStatusCode)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> UpdateForumPost(UpdatePostRequest updatePostRequest)
    {
        var request = await _client.PutAsJsonAsync($"/api/posts/{updatePostRequest.Id}", updatePostRequest);
        if (!request.IsSuccessStatusCode)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> UpdateForumThread(UpdateThreadRequest updateForumThread)
    {
        var request = await _client.PutAsJsonAsync($"/api/threads/{updateForumThread.Id}", updateForumThread);
        if (!request.IsSuccessStatusCode)
        {
            return false;
        }
        return true;
    }

    public async Task<User?> UpdateProfileImage(IFormFile image)
    {
        using var content = new MultipartFormDataContent();
        using var stream = image.OpenReadStream();
        content.Add(new StreamContent(stream), "image", image.FileName);
        var response = await _client.PutAsync("/api/me/image", content);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
        return await response.Content.ReadFromJsonAsync<User>();
    }

    public async Task<bool> UpdateUser(UpdateUserRequest updateUserRequest)
    {
        var request = await _client.PutAsJsonAsync($"/api/me", updateUserRequest);
        if (!request.IsSuccessStatusCode)
        {
            return false;
        }
        return true;
    }
}