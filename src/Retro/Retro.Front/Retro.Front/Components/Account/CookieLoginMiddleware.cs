using Microsoft.AspNetCore.Identity;
using Retro.Module.User.Account;

namespace Retro.Front.Components.Account;

public class LoginInfo
{
    public string? Email { get; init; }
    public string? Password { get; set; }
    public bool RememberMe { get; init; }
    public string ReturnUrl { get; init; }
}

public class CookieLoginMiddleware
{
    public static IDictionary<Guid, LoginInfo> Logins { get; private set; } = new Dictionary<Guid, LoginInfo>();
    
    private readonly RequestDelegate _next;
    
    public CookieLoginMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context, SignInManager<ApplicationUser> signInManager)
    {
        if (context.Request.Path == "/app/login" && context.Request.Query.ContainsKey("key"))
        {
            var key = Guid.Parse(context.Request.Query["key"]);
            var info = Logins[key];
            
            var result = await signInManager.PasswordSignInAsync(info.Email!, info.Password!, info.RememberMe, lockoutOnFailure: false);
            info.Password = null;
            if (result.Succeeded)
            {
                Logins.Remove(key);
                context.Response.Redirect(info.ReturnUrl);
                return;
            }
            return;
        }
        
        if (context.Request.Path == "/app/logout")
        {
            context.Response.Cookies.Delete("login");
            context.Response.StatusCode = 200;
            return;
        }
        
        await _next(context);
    }
}