using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Retro.Front.Client;
using Retro.Infrastructure.Account;

namespace Retro.Front.Components.Account;

// This is a server-side AuthenticationStateProvider that revalidates the security stamp for the connected user
// every 30 minutes an interactive circuit is connected. It also uses PersistentComponentState to flow the
// authentication state to the client which is then fixed for the lifetime of the WebAssembly application.
public class PersistingRevalidatingAuthenticationStateProvider : RevalidatingServerAuthenticationStateProvider
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly PersistentComponentState _state;
    private readonly IdentityOptions _options;

    private readonly PersistingComponentStateSubscription _subscription;

    private Task<AuthenticationState?> _authenticationStateTask;

    public PersistingRevalidatingAuthenticationStateProvider(ILoggerFactory loggerFactory,
        IServiceScopeFactory scopeFactory, PersistentComponentState state,
        IOptions<IdentityOptions> optionsAccessor) : base(loggerFactory)
    {
        _scopeFactory = scopeFactory;
        _state = state;
        _options = optionsAccessor.Value;

        AuthenticationStateChanged += OnAuthenticationStateChanged;
        _subscription = state.RegisterOnPersisting(OnPersistingAsync, RenderMode.InteractiveWebAssembly);
    }

    protected override TimeSpan RevalidationInterval => TimeSpan.FromMinutes(30);

    private async Task OnPersistingAsync()
    {
        if (_authenticationStateTask is null)
        {
            throw new UnreachableException($"Authentication state not set in {nameof(OnPersistingAsync)}().");
        }

        var authenticationState = await _authenticationStateTask;
        var principal = authenticationState?.User;

        if (principal?.Identity?.IsAuthenticated == true)
        {
            var userId = principal.FindFirst(_options.ClaimsIdentity.UserIdClaimType)?.Value;
            var email = principal.FindFirst(_options.ClaimsIdentity.EmailClaimType)?.Value;

            if (userId != null && email != null)
            {
                _state.PersistAsJson(nameof(UserInfo), new UserInfo
                {
                    UserId = userId,
                    Email = email
                });
            }
        }
    }

    private void OnAuthenticationStateChanged(Task<AuthenticationState> task)
        {
            _authenticationStateTask = task!;
        }

    protected override async Task<bool> ValidateAuthenticationStateAsync(AuthenticationState authenticationState,
        CancellationToken cancellationToken)
    {
        // Get the user manager from a new scope to ensure it's fetches fresh data
        await using var scope = _scopeFactory.CreateAsyncScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        return await ValidateSecurityStampAsync(userManager, authenticationState.User);
    }

    private async Task<bool> ValidateSecurityStampAsync(UserManager<ApplicationUser> userManager,
        ClaimsPrincipal principal)
    {
        var user = await userManager.GetUserAsync(principal);
        if (user is null)
        {
            return false;
        }

        if (!userManager.SupportsUserSecurityStamp)
        {
            return true;
        }

        var principalSecurityStamp = principal.FindFirstValue(_options.ClaimsIdentity.SecurityStampClaimType);
        return principalSecurityStamp == await userManager.GetSecurityStampAsync(user);
    }
    
    protected override void Dispose(bool disposing)
    {
        _subscription.Dispose();
        AuthenticationStateChanged -= OnAuthenticationStateChanged;
        base.Dispose(disposing);
    }
}