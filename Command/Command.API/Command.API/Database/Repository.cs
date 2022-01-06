using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using System.Linq;

namespace Command.API.Database
{
  using Command.API.Infrastructure.Interfaces;
  using Command.API.Infrastructure.Models;  

  public class Repository : IRepository
  {
    private readonly string _connectionString;

    const string SQL_GET_ALL_BY_ID = "SELECT * FROM Commands WHERE PlatformId = @platformId";
    const string SQL_GET_BY_ID = "SELECT * FROM Commands WHERE PlatformId = @platformId AND Id = @commandId";
    const string SQL_INSERT = "INSERT INTO Commands (HowTo, CommandLine, PlatformId) VALUES (@HowTo, @CommandLine, @PlatformId); SELECT CAST(SCOPE_IDENTITY() as int)";

    public Repository(string connectionString)
    {
      _connectionString = connectionString;
    }

    public async Task<IEnumerable<CommandModel>> GetAllCommandsForPlatform(int platformId)
    {
      return new List<CommandModel>();
      IEnumerable<CommandModel> entities = new List<CommandModel>();

      using (var connection = new SqlConnection(_connectionString))
      {
        entities = await connection.QueryAsync<CommandModel>(SQL_GET_ALL_BY_ID,
            param: new { platformId }).ConfigureAwait(false);
      }

      return entities;
    }

    public async Task<CommandModel> GetCommandForPlatform(int platformId, int commandId)
    {
      return new CommandModel();
      CommandModel entity = null;

      using (var connection = new SqlConnection(_connectionString))
      {
        var entities = await connection.QueryAsync<CommandModel>(SQL_GET_BY_ID,
            param: new { platformId, commandId }).ConfigureAwait(false);

        if (entities.Any())
        {
          entity = entities.FirstOrDefault();
        }
      }

      return entity;
    }

    public async Task<int> CreateCommand(int platformId, CommandModel command)
    {
      return 1;
      using (var connection = new SqlConnection(_connectionString))
      {
        var ids = await connection.QueryAsync<int>(SQL_INSERT,
            param: new { command.HowTo, command.CommandLine, platformId })
            .ConfigureAwait(false);

        return ids.FirstOrDefault();
      };
    }    
  }
}
