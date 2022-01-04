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

        const string SQL_GET_ALL = "SELECT * FROM Platforms";
        const string SQL_GET_BY_ID = "SELECT * FROM Platforms WHERE Id = @id";
        const string SQL_REMOVE = "DELETE FROM Platforms WHERE Id = @id";
        const string SQL_INSERT = "INSERT INTO Platforms (Title, Publisher, Cost, CreatedAt, ModifiedAt) VALUES (@Title, @Publisher, @Cost, @CreatedAt, @ModifiedAt); SELECT CAST(SCOPE_IDENTITY() as int)";
        const string SQL_UPDATE = "UPDATE Platforms SET (Title = @Title, Publisher = @Publisher, Cost = @Cost) WHERE Id = @id";

        public Repository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<PlatformModel>> GetAll()
        {
            IEnumerable<PlatformModel> platforms = new List<PlatformModel>();

            using (var connection = new SqlConnection(_connectionString))
            {
                platforms = await connection.QueryAsync<PlatformModel>(SQL_GET_ALL).ConfigureAwait(false);
            }

            return platforms;
        }

        public async Task<PlatformModel> GetById(int id)
        {
            PlatformModel entity = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                var entities = await connection.QueryAsync<PlatformModel>(SQL_GET_BY_ID,
                    param: new { id }).ConfigureAwait(false);

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
            var entities = await connection.ExecuteAsync(SQL_REMOVE,
                param: new { id }).ConfigureAwait(false);
        }

        public async Task<int> CreateAsync(PlatformModel entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var ids = await connection.QueryAsync<int>(SQL_INSERT,
                    param: new { entity.Title, entity.Publisher, entity.Cost, entity.CreatedAt, entity.ModifiedAt })
                    .ConfigureAwait(false);

                return ids.FirstOrDefault();
            };
        }

        public async Task Update(PlatformModel entity)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(SQL_UPDATE,
                param: new { entity.Title, entity.Publisher, entity.Cost, id = entity.Id })
                .ConfigureAwait(false);
        }
    }
}
