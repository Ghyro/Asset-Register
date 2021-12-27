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

        private readonly List<PlatformModel> platformModels;

        const string SQL_GET_ALL = "SELECT * FROM Platform";
        const string SQL_GET_BY_ID = "SELECT * FROM Platform WHERE Id = @id";
        const string SQL_REMOVE = "DELETE FROM Platform WHERE Id = @id";
        const string SQL_INSERT = "INSERT INTO Platform (Title, Publisher, Cost, CreatedAt, ModifiedAt) VALUES (@Title, @Publisher, @Cost, @CreatedAt, @ModifiedAt); SELECT CAST(SCOPE_IDENTITY() as int)";
        const string SQL_UPDATE = "UPDATE Platform SET (Title = @Title, Publisher = @Publisher, Cost = @Cost) WHERE Id = @id";

        public Repository(string connectionString)
        {
            _connectionString = connectionString;
            // TODO: Should be removed after database is introduced
            platformModels = new List<PlatformModel>
            {
                new PlatformModel(1, "TestTitle_1", "TestPublisher_1", "TestCost_1"),
                new PlatformModel(2, "TestTitle_2", "TestPublisher_2", "TestCost_2"),
                new PlatformModel(3, "TestTitle_3", "TestPublisher_3", "TestCost_3"),
                new PlatformModel(4, "TestTitle_4", "TestPublisher_4", "TestCost_4"),
                new PlatformModel(5, "TestTitle_5", "TestPublisher_5", "TestCost_5")
            };
        }

        public async Task<IEnumerable<PlatformModel>> GetAll()
        {
            // TODO: Should be removed after database is introduced
            return platformModels;
            //IEnumerable<PlatformModel> platforms = new List<PlatformModel>();

            //using (var connection = new SqlConnection(_connectionString))
            //{
            //    platforms = await connection.QueryAsync<PlatformModel>(SQL_GET_ALL).ConfigureAwait(false);
            //}

            //return platforms;
        }

        public async Task<PlatformModel> GetById(int id)
        {
            // TODO: Should be removed after database is introduced

            return platformModels.FirstOrDefault(x => x.Id == id);
            //PlatformModel entity = null;

            //using (var connection = new SqlConnection(_connectionString))
            //{
            //    var entities = await connection.QueryAsync<PlatformModel>(SQL_GET_BY_ID,
            //        param: new { id }).ConfigureAwait(false);

            //    if (entities.Any())
            //    {
            //        entity = entities.FirstOrDefault();
            //    }
            //}

            //return entity;
        }

        public async Task Remove(int id)
        {
            // TODO: Should be removed after database is introduced
            var platform = platformModels.FirstOrDefault(x => x.Id == id);
            if (platform != null)
            {
                platformModels.Remove(platform);
            }            
            //using var connection = new SqlConnection(_connectionString);
            //var entities = await connection.ExecuteAsync(SQL_REMOVE,
            //    param: new { id }).ConfigureAwait(false);
        }

        public async Task<int> CreateAsync(PlatformModel entity)
        {
            // TODO: Should be removed after database is introduced
            var lastId = platformModels.LastOrDefault().Id;
            entity.Id = ++lastId;
            platformModels.Add(entity);
            return lastId;
            //using (var connection = new SqlConnection(_connectionString))
            //{
            //    var ids = await connection.QueryAsync<int>(SQL_INSERT,
            //        param: new { entity.Title, entity.Publisher, entity.Cost, entity.CreatedAt, entity.ModifiedAt })
            //        .ConfigureAwait(false);

            //    return ids.FirstOrDefault();
            //};
            
        }

        public async Task Update(PlatformModel entity)
        {
            // TODO: Should be uncommented after database is introduced

            //using var connection = new SqlConnection(_connectionString);
            //await connection.ExecuteAsync(SQL_UPDATE,
            //    param: new { entity.Title, entity.Publisher, entity.Cost, id = entity.Id })
            //    .ConfigureAwait(false);
        }
    }
}
