using Microsoft.AspNetCore.Components;
using SnackisWeb.Models;

namespace SnackisWeb.Components;

public partial class ThreadItem
{
    [Parameter]
    public ForumThread Thread { get; set; } = null!;

    private string GetThreadUrl(ForumThread thread)
    {
        return $"/threads/{Thread.Id}";
    }
}
