using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Retro.Module.Team.Application.Dtos;
using Retro.Module.Team.Domain;

namespace Retro.Module.Team.Infrastructure.Persistence;

public class TeamRepository(TeamDbContext dbContext, IMemoryCache cache) : ITeamRepository
{
    public async Task<Domain.Team?> GetByIdAsync(Guid id)
    {
        var semaphore = new SemaphoreSlim(1, 1);

        if (cache.TryGetValue($"team{id}", out Domain.Team? team)) return team;
        try
        {
            await semaphore.WaitAsync();
            if (!cache.TryGetValue($"team{id}", out team))
            {
                var teamEntity = await dbContext.Teams.FindAsync(id);

                if (teamEntity is null)
                {
                    return null;
                }

                var memberEntities =
                    await dbContext.Members.Where(m => m.TeamId == id).Select(m => m.UserId).ToListAsync();
                team = new Domain.Team(teamEntity.Id, teamEntity.OwnerId, teamEntity.Name, memberEntities);

                cache.Set($"team{id}", team, new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                });
            }
        }
        finally
        {
            semaphore.Release();
        }

        return team;
    }

    public async Task<UserTeamsDto> GetByUserIdAsync(Guid userId)
    {
        var semaphore = new SemaphoreSlim(1, 1);
        if (!cache.TryGetValue($"user{userId}teams", out UserTeamsDto? userTeams))
        {
            try
            {
                await semaphore.WaitAsync();
                if (!cache.TryGetValue($"user{userId}teams", out userTeams))
                {
                    var teamIds = await dbContext.Members.Where(m => m.UserId == userId && m.ValidTo == null)
                        .Select(m => m.TeamId).ToListAsync();
                    userTeams = new UserTeamsDto(userId, teamIds);
                    cache.Set($"user{userId}teams", userTeams, new MemoryCacheEntryOptions
                    {
                        SlidingExpiration = TimeSpan.FromMinutes(30)
                    });
                }
            }
            finally
            {
                semaphore.Release();
            }
        }

        return userTeams ?? new UserTeamsDto(userId, []);
    }

    public async Task AddAsync(Domain.Team entity)
    {
        var teamEntity = new Entities.TeamEntity
        {
            Id = entity.Id,
            OwnerId = entity.OwnerId,
            Name = entity.Name
        };

        dbContext.Teams.Add(teamEntity);
        await dbContext.SaveChangesAsync();
    }

    public Task UpdateAsync(Domain.Team entity)
    {
        var teamEntity = new Entities.TeamEntity
        {
            Id = entity.Id,
            OwnerId = entity.OwnerId,
            Name = entity.Name
        };
        var memberEntities = entity.Members.Select(m => new Entities.TeamMemberEntity
        {
            Id = Guid.NewGuid(),
            TeamId = entity.Id,
            UserId = m,
            ValidFrom = DateTimeOffset.UtcNow
        }).ToList();

        var existingMembers = dbContext.Members.Where(m => m.TeamId == entity.Id && m.ValidTo == null);

        foreach (var memberEntity in memberEntities.Where(memberEntity =>
                     !existingMembers.Any(m => m.UserId == memberEntity.UserId)))
        {
            dbContext.Members.Add(memberEntity);
        }

        foreach (var existingMember in existingMembers.Where(m => memberEntities.All(me => me.UserId != m.UserId)))
        {
            existingMember.ValidTo = DateTimeOffset.UtcNow;
            dbContext.Members.Update(existingMember);
        }

        dbContext.Teams.Update(teamEntity);
        return dbContext.SaveChangesAsync();
    }
}