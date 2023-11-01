namespace Domain.Entities;

public class Player
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public Team? Team { get; set; }
    public int Number { get; set; }
}