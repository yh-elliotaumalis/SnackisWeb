using Microsoft.AspNetCore.Components;
using SnackisWeb.Interfaces;
using SnackisWeb.Models;
using System.ComponentModel.DataAnnotations;

namespace SnackisWeb.Components.Pages;

public class MessageInput
{
    [Required]
    public string RecipientId { get; set; } = "";
    [Required, MinLength(1), MaxLength(10000)]
    public string Content { get; set; } = "";
}

public partial class MessagesPage
{
    private readonly IForumAPIService _forumAPIService;
    private readonly NavigationManager _navigationManager;

    public MessagesPage(IForumAPIService forumAPIService, NavigationManager navigationManager)
    {
        _forumAPIService = forumAPIService;
        _navigationManager = navigationManager;
    }

    private List<ForumMessage> Messages { get; set; } = new List<ForumMessage>();
    private List<PublicUser> Users { get; set; } = new List<PublicUser>();
    private MessageInput MessageRequest { get; set; } = new MessageInput();
    private string? Error { get; set; } = null;
    private bool IsCreatingMessage { get; set; } = false;

    protected override async Task OnInitializedAsync()
    {
        Messages = await _forumAPIService.GetForumMessages();
        Users = await _forumAPIService.GetForumUsers();
    }

    private async Task CreateMessage()
    {
        IsCreatingMessage = true;
        Error = null;
        var createMessageRequest = new CreateMessageRequest
        {
            RecipientId = MessageRequest.RecipientId,
            Content = MessageRequest.Content
        };
        try
        {
            var createdMessage = await _forumAPIService.CreateForumMessage(createMessageRequest);
            _navigationManager.Refresh(true);
            return;
        }
        catch (HttpRequestException)
        {
            Error = "Något gick fel, vänligen försök igen.";
        }
        IsCreatingMessage = false;
    }
}
