using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using SnackisWeb.Interfaces;
using SnackisWeb.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace SnackisWeb.Components.Pages;

public class RegisterRequestInput
{
    [Required, EmailAddress]
    public string Email { get; set; } = "";
    [Required]
    public string Password { get; set; } = "";
}

public partial class RegisterPage
{
    private readonly IForumAPIService _forumAPIService;
    private readonly NavigationManager _navigationManager;
    public RegisterPage(IForumAPIService forumAPIService, NavigationManager navigationManager)
    {
        _forumAPIService = forumAPIService;
        _navigationManager = navigationManager;
    }
    [SupplyParameterFromForm]
    private RegisterRequestInput RegisterInput { get; set; } = new RegisterRequestInput();
    private string Error { get; set; } = "";
    private async Task HandleRegister()
    {
        var success = await _forumAPIService.Register(new RegisterRequest
        {
            Email = RegisterInput.Email,
            Password = RegisterInput.Password
        });
        if (!success)
        {
            Error = "Dubbelkolla e-post och lösenord";
            return;
        }

        _navigationManager.NavigateTo("/login");
    }

}
