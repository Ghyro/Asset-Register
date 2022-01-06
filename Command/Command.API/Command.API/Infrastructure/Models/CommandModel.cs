using System;


namespace Command.API.Infrastructure.Models
{
  public class CommandModel
  {
    public int Id { get; set; }
    public string HowTo { get; set; }
    public string CommandLine { get; set; }
    public int PlatformId { get; set; }
    public PlatformModel PlatformModel { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
  }
}
