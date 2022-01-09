namespace Platform.API.AsyncDataServices
{
  using Platform.API.Infrastructure.Dtos;

  public interface IMessageBusClient
  {
    void PublishNewPlatform(PlatformPublishedDto platformPublishedDto);
  }
}
