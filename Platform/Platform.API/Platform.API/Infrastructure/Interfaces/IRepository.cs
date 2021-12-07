using System.Collections.Generic;
using System.Threading.Tasks;


namespace Platform.API.Infrastructure.Interfaces
{
    internal interface IRepository
    {
        Task CreateAsync(PlatformModel entity);
        Task<IEnumerable<PlatformModel>> GetAll();
        Task<PlatformModel> GetById(int id);
        Task Update(PlatformModel entity);
        Task Remove(int id);
    }
}
