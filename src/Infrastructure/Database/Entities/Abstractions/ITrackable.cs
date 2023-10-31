namespace Infrastructure.Database.Entities.Abstractions;

public interface ITrackable
{
    DateTimeOffset CreatedAt { get; set; }
    DateTimeOffset UpdatedAt { get; set; }
}