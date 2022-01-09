using System.Threading.Tasks;


namespace Command.API.Infrastructure.Interfaces
{
  public interface IEventProcessor
  {
    Task ProcessEvent(string message);
  }
}
