using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Retro.Front.Client.Pages;
using Retro.Front.Components;
using Retro.Front.Components.Account;
using Retro.Front.Components.Common.Services;
using Retro.Front.Endpoints;
using Retro.Front.Interfaces;
using Retro.Front.Services;
using Retro.Module.Team;
using Retro.Module.User;
using Retro.Module.User.Account;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddSignInManager()
    .AddIdentityDatabase();

builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = "/app/login";
    config.LogoutPath = "/app/logout";
});

builder.Services.AddTransient<ITeamService, TeamService>();

builder.Services.AddScoped<OverlayService>();
builder.Services.AddSingleton<TeamDataService>();

// Modules registration
builder.Services.AddTeamModule(builder.Configuration);
builder.Services.AddUserModule(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<CookieLoginMiddleware>();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Auth).Assembly);

app.MapAdditionalIdentityEndpoints();

// Map modules endpoints
app.AddTeamEndpoints();

app.Run();
