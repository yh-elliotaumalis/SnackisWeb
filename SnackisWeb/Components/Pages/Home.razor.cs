using SnackisWeb.Interfaces;
using SnackisWeb.Models;

namespace SnackisWeb.Components.Pages;

public partial class Home
{
    private readonly IForumAPIService _forumApi;
    private List<ForumThread> Threads = new();
    private List<ForumCategory> Categories = new();
    private List<ForumCategory> PrimaryCategories = new();
    public Home(IForumAPIService forumApi)
    {
         _forumApi = forumApi;
    }
    protected override async Task OnInitializedAsync()
    {
        Threads = await _forumApi.GetForumThreads();
        Categories = await _forumApi.GetForumCategories();

        PrimaryCategories = Categories.Where(c => c.ParentCategoryId == null).ToList();
    }
}
