using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;


namespace Command.API.Database
{
  using Command.API.Infrastructure.Interfaces;
  using Command.API.Infrastructure.Models;  

  public class Repository : IRepository
  {
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
      _context = context;
    }

    public async Task CreateCommand(int platformId, CommandModel command)
    {
      if (command == null)
      {
        throw new ArgumentNullException(nameof(command));
      }

      command.PlatformId = platformId;
      await _context.Commands.AddAsync(command);
      await _context.SaveChangesAsync();
    }

    public async Task CreatePlatform(PlatformModel plat)
    {
      if (plat == null)
      {
        throw new ArgumentNullException(nameof(plat));
      }
      await _context.Platforms.AddAsync(plat);
      await _context.SaveChangesAsync();
    }

    public bool ExternalPlatformExists(int externalPlatformId)
    {
      return _context.Platforms.Any(p => p.ExternalId == externalPlatformId);
    }

    public async Task<IEnumerable<PlatformModel>> GetAllPlatforms()
    {
      return await _context.Platforms.ToListAsync();
    }

    public async Task<CommandModel> GetCommand(int platformId, int commandId)
    {
      return await _context.Commands.Where(c => c.PlatformId == platformId && c.Id == commandId).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<CommandModel>> GetCommandsForPlatform(int platformId)
    {
      return await _context.Commands.Where(c => c.PlatformId == platformId).OrderBy(c => c.PlatformModel.Title).ToListAsync();
    }

    public bool PlatformExits(int platformId)
    {
      return _context.Platforms.Any(p => p.Id == platformId);
    }
  }
}
