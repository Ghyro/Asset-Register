using System;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using System.Text.Json;
using System.Threading.Tasks;

namespace Command.API.EventProcessor
{
  using Command.API.Infrastructure.Dtos;
  using Command.API.Infrastructure.Interfaces;
  using Command.API.Infrastructure.Models;  

  public class EventProcessor : IEventProcessor
  {
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMapper _mapper;

    public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
    {
      _scopeFactory = scopeFactory;
      _mapper = mapper;
    }

    public async Task ProcessEvent(string message)
    {
      var eventType = DetermineEvent(message);
      switch (eventType)
      {
        case EventType.PlatformPublished:
          await AddPlatform(message);
          break;
        default:
          break;
      }
    }

    private async Task AddPlatform(string platformPublishedMessage)
    {
      using var scope = _scopeFactory.CreateScope();
      var repository = scope.ServiceProvider.GetRequiredService<IRepository>();
      var platforPublishedDto = JsonSerializer.Deserialize<PlatformPublishedDto>(platformPublishedMessage);
      try
      {
        var platform = _mapper.Map<PlatformModel>(platforPublishedDto);
        if (!repository.ExternalPlatformExists(platform.ExternalId))
        {           
          await repository.CreatePlatform(platform);
          Console.WriteLine("Platform has been created");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Could not add Platform to database. Exception: {ex.Message}");
      }
    }

    private static EventType DetermineEvent(string message)
    {
      Console.WriteLine("Determining event...");
      var eventType = JsonSerializer.Deserialize<GenericEventDto>(message);
      switch (eventType.Event)
      {
        case "Platform_Published":
          Console.WriteLine("Platform Published Event Detected.");
          return EventType.PlatformPublished;
        default:
          Console.WriteLine("Unknown Event Detected");
          return EventType.Unknown;
      }
    }
  }
}
