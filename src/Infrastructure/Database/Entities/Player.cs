using Infrastructure.Database.Entities.Abstractions;

namespace Infrastructure.Database.Entities;

public class Player : Entity, ITrackable
{
    public string Name { get; set; } = default!;
    public Team? Team { get; set; }
    public int Number { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}