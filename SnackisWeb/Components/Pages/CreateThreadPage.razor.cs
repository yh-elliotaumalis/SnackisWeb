
using Microsoft.AspNetCore.Components;
using SnackisWeb.Interfaces;
using SnackisWeb.Models;
using System.ComponentModel.DataAnnotations;

namespace SnackisWeb.Components.Pages;

public class ThreadInput
{
    [Required, MinLength(3), MaxLength(100)]
    public string Title { get; set; } = "";
    [Required, MinLength(3), MaxLength(1000)]
    public string Body { get; set; } = "";
}

public partial class CreateThreadPage
{
    [Parameter]
    public int CategoryId { get; set; }

    private readonly IForumAPIService _forumAPIService;
    private readonly NavigationManager _navigationManager;

    public CreateThreadPage(IForumAPIService forumAPIService, NavigationManager navigationManager)
    {
        _forumAPIService = forumAPIService;
        _navigationManager = navigationManager;
    }

    private ForumCategory? Category { get; set; }
    private ThreadInput ThreadRequest { get; set; } = new ThreadInput();
    public string? Error { get; set; } = null;
    private bool IsCreatingThread { get; set; } = false;

    protected override async Task OnInitializedAsync()
    {
        var categories = await _forumAPIService.GetForumCategories();
        Category = categories.FirstOrDefault(c => c.Id == CategoryId);
    }

    private async Task CreateThread()
    {
        IsCreatingThread = true;
        var createThreadRequest = new CreateThreadRequest
        {
            CategoryId = CategoryId,
            Title = ThreadRequest.Title,
            Content = ThreadRequest.Body
        };
        try
        {
            var createdThread = await _forumAPIService.CreateForumThread(createThreadRequest);
            if (createdThread is null)
            {
                Error = "Något gick fel, vänligen försök igen.";
                return;
            }
            _navigationManager.NavigateTo($"/threads/{createdThread.Id}", true);
            Error = null;
        }
        catch (HttpRequestException)
        {
            Error = "Något gick fel, vänligen försök igen.";
        }
        IsCreatingThread = false;
    }
}
