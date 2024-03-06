using Microsoft.EntityFrameworkCore;
using Retro.Module.Team.Domain;

namespace Retro.Module.Team.Infrastructure.Persistence;

public class TeamRepository(TeamDbContext dbContext) : ITeamRepository
{
    public async Task<Domain.Team?> GetByIdAsync(Guid id)
    {
        var teamEntity = await dbContext.Teams.FindAsync(id);

        if (teamEntity is null)
        {
            return null;
        }

        var memberEntities = await dbContext.Members.Where(m => m.TeamId == id).Select(m => m.UserId).ToListAsync();

        var team = new Domain.Team(teamEntity.Id, teamEntity.OwnerId, teamEntity.Name, memberEntities);

        return team;
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