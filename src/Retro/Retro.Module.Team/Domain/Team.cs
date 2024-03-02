using Retro.Common.Domain;
using Retro.Common.Exceptions;

namespace Retro.Module.Team.Domain;

public class Team : Entity<Guid>
{
    private readonly List<Guid> _members = [];
    public Guid OwnerId { get; }
    public string Name { get; private set; }
    
    public IReadOnlyList<Guid> Members => _members.AsReadOnly();
    
    public Team(Guid id, Guid ownerId, string name, List<Guid> members) : base(id)
    {
        OwnerId = ownerId;
        Name = name;
        _members = members;
    }
    
    public Team (Guid id, Guid ownerId, string name) : base(id)
    {
        OwnerId = ownerId;
        Name = name;
    }

    public Team(Guid ownerId, string name) : base(Guid.NewGuid())
    {
        OwnerId = ownerId;
        Name = name;
    }

    public void AddMember(Guid userId)
    {
        if (_members.Contains(userId))
            throw new DomainException($"User {userId} is already a member of the team {Id}.");

        _members.Add(userId);
    }

    public void RemoveMember(Guid userId)
    {
        if (OwnerId == userId)
        {
            throw new DomainException($"User {userId} is the owner of the team {Id} and cannot be removed.");
        }

        if (!_members.Contains(userId))
        {
            throw new DomainException($"User {userId} is not a member of the team {Id}.");
        }

        _members.Remove(userId);
    }
    
    public void ChangeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException("Team name cannot be empty.");
        }
        
        Name = name;
    }
}