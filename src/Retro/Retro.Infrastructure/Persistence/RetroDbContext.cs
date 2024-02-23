using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Retro.Infrastructure.Account;

namespace Retro.Infrastructure.Persistence;

public class RetroDbContext(DbContextOptions<RetroDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    
}