using System;


namespace Platform.API.Infrastructure
{
    using Platform.API.Infrastructure.Interfaces;

    public class PlatformModel : IIdentifiable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public string Cost { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
