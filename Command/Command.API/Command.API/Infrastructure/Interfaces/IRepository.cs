using System.Collections.Generic;
using System.Threading.Tasks;


namespace Command.API.Infrastructure.Interfaces
{
  using Command.API.Infrastructure.Models;

  public interface IRepository
  {
    Task<IEnumerable<PlatformModel>> GetAllPlatforms();
    Task CreatePlatform(PlatformModel plat);
    bool PlatformExits(int platformId);
    bool ExternalPlatformExists(int externalPlatformId);
    Task<IEnumerable<CommandModel>> GetCommandsForPlatform(int platformId);
    Task<CommandModel> GetCommand(int platformId, int commandId);
    Task CreateCommand(int platformId, CommandModel command);
  }
}
