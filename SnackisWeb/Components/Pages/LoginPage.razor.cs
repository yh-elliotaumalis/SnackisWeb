using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using SnackisWeb.Interfaces;
using SnackisWeb.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace SnackisWeb.Components.Pages;

public class LoginRequestInput
{
    [Required, EmailAddress]
    public string Email { get; set; } = "";
    [Required]
    public string Password { get; set; } = "";
}

public partial class LoginPage
{
    private readonly IForumAPIService _forumAPIService;
    private readonly NavigationManager _navigationManager;
    [CascadingParameter]
    private HttpContext? _httpContext { get; set; }
    public LoginPage(IForumAPIService forumAPIService, NavigationManager navigationManager)
    {
        _forumAPIService = forumAPIService;
        _navigationManager = navigationManager;
    }
    [SupplyParameterFromForm]
    private LoginRequestInput LoginInput { get; set; } = new LoginRequestInput();
    private string Error { get; set; } = "";
    private async Task HandleLogin()
    {
        var tokenResponse = await _forumAPIService.Login(new LoginRequest
        {
            Email = LoginInput.Email,
            Password = LoginInput.Password
        });
        if (tokenResponse is null)
        {
            Error = "Fel e-post eller lösenord";
            return;
        }

        var user = await _forumAPIService.GetUser(tokenResponse.AccessToken);
        if (user is null)
        {
            Error = "Användaren kunde inte hittas";
            return;
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, string.IsNullOrWhiteSpace(user.Name) ? user.Email : user.Name),
            new Claim(ClaimTypes.Email, user.Email)
        };
        claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

        var properties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
        };

        properties.StoreTokens(new List<AuthenticationToken>
        {
            new AuthenticationToken { Name = "accessToken", Value = tokenResponse.AccessToken },
            new AuthenticationToken { Name = "refreshToken", Value = tokenResponse.RefreshToken },
        });

        await _httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);

        _navigationManager.NavigateTo("/");
    }

}
