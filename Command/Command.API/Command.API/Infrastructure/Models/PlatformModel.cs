using System.Collections.Generic;


namespace Command.API.Infrastructure.Models
{
  public class PlatformModel
  {
    public int Id { get; set; }
    public int ExternalId { get; set; }
    public string Title { get; set; }
    public ICollection<CommandModel> CommandModels { get; set; } = new List<CommandModel>();
  }
}
