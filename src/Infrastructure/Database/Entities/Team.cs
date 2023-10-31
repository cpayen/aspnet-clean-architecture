using Infrastructure.Database.Entities.Abstractions;

namespace Infrastructure.Database.Entities;

public class Team : Entity, ITrackable
{
    public string Name { get; set; } = default!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}