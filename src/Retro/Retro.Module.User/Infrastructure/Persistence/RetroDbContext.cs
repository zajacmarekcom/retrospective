using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Retro.Module.User.Account;

namespace Retro.Module.User.Infrastructure.Persistence;

public class RetroDbContext(DbContextOptions<RetroDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    
}