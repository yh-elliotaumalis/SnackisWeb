using Microsoft.AspNetCore.Components;
using SnackisWeb.Interfaces;
using SnackisWeb.Models;
using System.ComponentModel.DataAnnotations;

namespace SnackisWeb.Components;

public class ReportInput
{
    [Required, MinLength(3), MaxLength(1000)]
    public string Reason { get; set; } = null!;
}

public partial class PostItem
{
    [Parameter]
    public ForumPost Post { get; set; } = null!;
    [Parameter]
    public bool ShowReportButton { get; set; } = true;

    private readonly IForumAPIService _forumAPIService;

    public PostItem(IForumAPIService forumAPIService)
    {
        _forumAPIService = forumAPIService;
    }

    private bool ReportFormVisible { get; set; } = false;
    private string Error { get; set; } = null!;
    private bool IsCreatingReport { get; set; } = false;
    private bool Reported { get; set; } = false;
    private ReportInput ReportInput { get; set; } = new ReportInput();
    private void OpenReportForm()
    {
        Error = null;
        ReportFormVisible = !ReportFormVisible;
    }

    private async Task CreateReport()
    {
        IsCreatingReport = true;
        Error = null;
        try
        {
            var reportRequest = new CreateForumPostReport
            {
                ForumPostId = Post.Id,
                Reason = ReportInput.Reason
            };

            var success = await _forumAPIService.CreateForumPostReport(reportRequest);
            if (success)
            {
                ReportFormVisible = false;
                Reported = true;
            } else
            {
                Error = "Kunde inte anmäla inlägget.";
            }
            
        }
        catch (HttpRequestException)
        {
            Error = "Något gick fel, vänligen försök igen.";

        }

        IsCreatingReport = false;
    }
}
