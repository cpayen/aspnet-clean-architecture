namespace Infrastructure.Database.Configuration;

public class DatabaseConfiguration
{
    public string Host { get; set; } = default!;
    public string User { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string Database { get; set; } = default!;
    public int ServerVersionMajor { get; set; }
    public int ServerVersionMinor { get; set; }
}