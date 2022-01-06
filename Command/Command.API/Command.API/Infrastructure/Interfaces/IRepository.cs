using System.Collections.Generic;
using System.Threading.Tasks;


namespace Command.API.Infrastructure.Interfaces
{
  using Command.API.Infrastructure.Models;

  public interface IRepository
  {
    Task<IEnumerable<CommandModel>> GetAllCommandsForPlatform(int platformId);
    Task<CommandModel> GetCommandForPlatform(int platformId, int commandId);
    Task<int> CreateCommand(int platformId, CommandModel command);
  }
}
