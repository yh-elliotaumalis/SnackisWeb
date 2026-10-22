using SnackisWeb.Interfaces;
using SnackisWeb.Models;

namespace SnackisWeb.Components.Pages;

public partial class AdminReportsPage
{

    public List<ForumPostReport> Reports { get; set; } = new List<ForumPostReport>();
    private readonly IForumAPIService _forumAPIService;

    public AdminReportsPage(IForumAPIService forumAPIService)
    {
        _forumAPIService = forumAPIService;
    }

    protected override async Task OnInitializedAsync()
    {
        Reports = await _forumAPIService.GetForumPostReports();
    }

    private async Task DeleteReport(int reportId)
    {
        var success = await _forumAPIService.DeleteForumPostReport(reportId);
        if (success)
        {
            Reports = Reports.Where(r => r.Id != reportId).ToList();
        }
    }

    private async Task DeletePost(int postId)
    {
        var success = await _forumAPIService.DeleteForumPost(postId);
        if (success)
        {
            Reports = Reports.Where(r => r.ForumPost.Id != postId).ToList();
        }
    }
}
