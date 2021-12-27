using System.Threading.Tasks;


namespace Platform.API.SyncDataServices.Http
{
    using Platform.API.Infrastructure.Dtos;

    public interface ICommandDataClient
    {
        Task SendPlatform(PlatformModelReadDto platform);
    }
}
