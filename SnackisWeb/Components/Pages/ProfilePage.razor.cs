using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using SnackisWeb.Interfaces;
using SnackisWeb.Models;
using System.ComponentModel.DataAnnotations;

namespace SnackisWeb.Components.Pages;

public class NameRequestInput
{
    [Required, MinLength(3), MaxLength(30)]
    public string Name { get; set; } = "";
}

public partial class ProfilePage
{
    private readonly IForumAPIService _forumAPIService;
    [CascadingParameter]
    private HttpContext? _httpContext { get; set; }
    public ProfilePage(IForumAPIService forumAPIService)
    {
        _forumAPIService = forumAPIService;
    }

    private User? CurrentUser { get; set; }
    [SupplyParameterFromForm(FormName = "NameForm")]
    private NameRequestInput NameInput { get; set; } = new NameRequestInput();
    private string Error { get; set; } = "";
    private string NameError { get; set; } = "";

    protected override async Task OnInitializedAsync()
    {
        var accessToken = await _httpContext.GetTokenAsync("accessToken");
        if (accessToken is not null)
        {
            CurrentUser = await _forumAPIService.GetUser(accessToken);
        }
    }

    private async Task UpdateName()
    {
        NameError = "";
        var success = await _forumAPIService.UpdateUser(new UpdateUserRequest
        {
            Name = NameInput.Name
        });
        if (!success)
        {
            NameError = "Kunde inte spara namnet.";
            return;
        }
    }

    private async Task UploadImage()
    {
        var image = _httpContext.Request.Form.Files["image"];
        if (image is null || image.Length == 0)
        {
            Error = "Välj en bild först";
            return;
        }

        var user = await _forumAPIService.UpdateProfileImage(image);
        if (user is null)
        {
            Error = "Kunde inte ladda upp bilden.";
            return;
        }

        CurrentUser = user;
        Error = "";
    }
}
