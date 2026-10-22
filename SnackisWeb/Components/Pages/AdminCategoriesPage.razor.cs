using Microsoft.AspNetCore.Components;
using SnackisWeb.Interfaces;
using SnackisWeb.Models;
using System.ComponentModel.DataAnnotations;

namespace SnackisWeb.Components.Pages;
public class CategoryInput
{
    public int Id { get; set; }
    [Required, MinLength(3), MaxLength(100)]
    public string Name { get; set; } = null!;
    [Required, MinLength(3), MaxLength(1000)]
    public string Topic { get; set; } = null!;
    public int Priority { get; set; }
    public int? ParentCategoryId { get; set; }
}
public partial class AdminCategoriesPage
{
    private readonly IForumAPIService _forumAPIService;
    private readonly NavigationManager _navigationManager;
    public AdminCategoriesPage(IForumAPIService forumAPIService, NavigationManager navigationManager)
    {
        _forumAPIService = forumAPIService;
        _navigationManager = navigationManager;
    }

    public List<ForumCategory> Categories { get; set; } = new List<ForumCategory>();

    protected override async Task OnInitializedAsync()
    {
        Categories = await _forumAPIService.GetForumCategories();
    }
    public CategoryInput NewCategory { get; set; } = new CategoryInput();
    public bool CreateCategoryOpen { get; set; } = false;
    public bool IsCreatingCategory { get; set; } = false;
    private async Task ToggleCreateCategory()
    {
        CreateCategoryOpen = !CreateCategoryOpen;
        if (!CreateCategoryOpen)
        {
            NewCategory = new CategoryInput();
        }
    }
    private async Task CreateCategory()
    {
        IsCreatingCategory = true;
        var newCategory = new CreateForumCategory
        {
            Name = NewCategory.Name,
            Topic = NewCategory.Topic,
            Priority = NewCategory.Priority,
            ParentCategoryId = NewCategory.ParentCategoryId
        };
        var createdCategory = await _forumAPIService.CreateForumCategory(newCategory);
        if (createdCategory == true)
        {
            _navigationManager.Refresh(true);
            return;
        }
        IsCreatingCategory = false;
    }

    private async Task DeleteCategory(int categoryId)
    {
        var success = await _forumAPIService.DeleteForumCategory(categoryId);
        if (success)
        {
            Categories = Categories.Where(c => c.Id != categoryId).ToList();
        }
    }
}
