using Application.Contracts;
using Infrastructure.Database.Repositories;

namespace Infrastructure.Database;

public class UnitOfWork : IUnitOfWork
{
    private readonly Context _context;

    public UnitOfWork(Context context)
    {
        _context = context;
    }

    private ITeamRepository? _teamRepository;
    public ITeamRepository TeamRepository
    {
        get { return _teamRepository ??= new TeamRepository(_context); }
    }
    
    private IPlayerRepository? _playerRepository;
    public IPlayerRepository PlayerRepository
    {
        get { return _playerRepository ??= new PlayerRepository(_context); }
    }
    
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}