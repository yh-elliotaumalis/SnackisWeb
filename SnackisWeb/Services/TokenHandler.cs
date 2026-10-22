using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;

namespace SnackisWeb.Services;

public class TokenHandler(IHttpContextAccessor httpContext) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var context = httpContext.HttpContext;
        if (context is not null)
        {
            var accessToken = await context.GetTokenAsync("accessToken");
            if (accessToken is not null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }
        }

        var response = await base.SendAsync(request, cancellationToken);

        return response;
    }
}