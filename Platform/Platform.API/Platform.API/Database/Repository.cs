using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Linq;
using Dapper;


namespace Platform.API.Database
{    
    using Platform.API.Infrastructure;
    using Platform.API.Infrastructure.Interfaces;   

    internal class Repository : IRepository
    {
        private readonly string _connectionString;

        const string sql_GetAll = "SELECT * FROM Platform";
        const string sql_GetById = "SELECT * FROM Platform WHERE Id = @id";
        const string sql_Remove = "DELETE FROM Platform WHERE Id = @id";
        const string sql_Insert = "INSERT INTO Platform VALUES (@Title, @Publisher, @Cost)";
        const string sql_Update = "UPDATE Platform SET (Title = @Title, Publisher = @Publisher, Cost = @Cost) WHERE Id = @id";

        public Repository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<PlatformModel>> GetAll()
        {
            IEnumerable<PlatformModel> platforms = new List<PlatformModel>();

            using (var connection = new SqlConnection(_connectionString))
            {
                platforms = await connection.QueryAsync<PlatformModel>(sql_GetAll).ConfigureAwait(false);
            }

            return platforms;
        }

        public async Task<PlatformModel> GetById(int id)
        {
            PlatformModel entity = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                var entities = await connection.QueryAsync<PlatformModel>(sql_GetById,
                    param: new { id = id }).ConfigureAwait(false);

                if (entities.Any())
                {
                    entity = entities.FirstOrDefault();
                }
            }

            return entity;
        }

        public async Task Remove(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var entities = await connection.ExecuteAsync(sql_Remove,
                param: new { id = id }).ConfigureAwait(false);
        }

        public async Task CreateAsync(PlatformModel entity)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(sql_Insert,
                param: new { Title = entity.Title, Publisher = entity.Publisher, Cost = entity.Cost })
                .ConfigureAwait(false);
        }

        public async Task Update(PlatformModel entity)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(sql_Update,
                param: new { Title = entity.Title, Publisher = entity.Publisher, Cost = entity.Cost, id = entity.Id })
                .ConfigureAwait(false);
        }
    }
}
