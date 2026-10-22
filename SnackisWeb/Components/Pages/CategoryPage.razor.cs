using Microsoft.AspNetCore.Components;
using SnackisWeb.Interfaces;
using SnackisWeb.Models;

namespace SnackisWeb.Components.Pages;

public partial class CategoryPage
{
    [Parameter]
    public int CategoryId { get; set; }

    public ForumCategory? Category { get; set; }
    public List<ForumCategory> Categories { get; set; } = new List<ForumCategory>();
    public List<ForumThread> Threads { get; set; } = new List<ForumThread>();

    private readonly IForumAPIService _forumAPIService;

    public CategoryPage(IForumAPIService forumAPIService)
    {
        _forumAPIService = forumAPIService;
    }

    protected override async Task OnInitializedAsync()
    {
        var categories = await _forumAPIService.GetForumCategories();
        var threads = await _forumAPIService.GetForumThreads();

        var category = categories.FirstOrDefault(c => c.Id == CategoryId);

        Category = category;
        Categories = categories;
        Threads = threads;
    }
}
