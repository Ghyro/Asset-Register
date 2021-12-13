using System.Collections.Generic;
using System.Threading.Tasks;


namespace Platform.API.Infrastructure.Interfaces
{
    public interface IRepository
    {
        Task<int> CreateAsync(PlatformModel entity);
        Task<IEnumerable<PlatformModel>> GetAll();
        Task<PlatformModel> GetById(int id);
        Task Update(PlatformModel entity);
        Task Remove(int id);
    }
}
