using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) { }

    public DbSet<Team> Teams { get; set; } = null!;
    public DbSet<Player> Players { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Team>(entity =>
        {
            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(250);
        });
        
        modelBuilder.Entity<Player>(entity =>
        {
            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(250);
            
            entity.Property(x => x.Number)
                .IsRequired();
            
            entity
                .HasOne(o => o.Team)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
