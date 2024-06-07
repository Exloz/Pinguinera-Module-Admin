using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using pinguinera_final_module.Services.Interfaces;
using pinguinera_final_module.Shared.Enums;

namespace pinguinera_final_module.Controllers;

public class RoleAuthorizationAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    private readonly RoleType _requiredRole;

    public RoleAuthorizationAttribute(RoleType requiredRole)
    {
        _requiredRole = requiredRole;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.Identity.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var userId = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
        Console.WriteLine($"UserId: {userId}");

        if (string.IsNullOrEmpty(userId))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
        var user = await userService.GetUserById(new Guid(userId));
        if (user.Role != _requiredRole)
        {
            context.Result = new ForbidResult();
        }
    }
}