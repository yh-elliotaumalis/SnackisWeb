using Microsoft.AspNetCore.Components;
using SnackisWeb.Interfaces;
using SnackisWeb.Models;

namespace SnackisWeb.Components.Pages;
public class PostInput
{
    public string Body { get; set; } = "";
}

public partial class ThreadPage
{
    [Parameter]
    public int ThreadId { get; set; }
    private readonly IForumAPIService _forumAPIService;
    private readonly NavigationManager _navigationManager;
    public ThreadPage(IForumAPIService forumAPIService, NavigationManager navigationManager)
    {
        _forumAPIService = forumAPIService;
        _navigationManager = navigationManager;
    }
    public ForumThread? Thread { get; set; }
    public List<ForumPost> Posts { get; set; } = new List<ForumPost>();

    protected override async Task OnInitializedAsync()
    {
        var thread = await _forumAPIService.GetForumThread(ThreadId);
        if (thread is null)
        {
            return;
        }

        Thread = thread;
        Posts = thread.Posts;
    }
    private PostInput CreatingPost { get; set; } = new PostInput();
    private bool IsCreatingPost { get; set; } = false;
    private string? Error { get; set; } = null;
    private async Task CreatePost()
    {
        IsCreatingPost = true;
        var postRequest = new CreatePostRequest
        {
            ThreadId = ThreadId,
            Body = CreatingPost.Body
        };

        try
        {
            var createdPost = await _forumAPIService.CreateForumPost(postRequest);

            _navigationManager.NavigateTo($"/threads/{ThreadId}", true);
            Error = null;
        } catch (HttpRequestException)
        {
            Error = "Något gick fel, vänligen försök igen.";
        }

        IsCreatingPost = false;
    }
}
