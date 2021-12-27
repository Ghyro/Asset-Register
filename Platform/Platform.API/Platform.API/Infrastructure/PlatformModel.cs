using System;


namespace Platform.API.Infrastructure
{
    using Platform.API.Infrastructure.Interfaces;

    public class PlatformModel : IIdentifiable
    {
        // TODO: Should be removed after database is introduced
        public PlatformModel(int id, string title, string publisher, string cost)
        {
            Id = id;
            Title = title;
            Publisher = publisher;
            Cost = cost;
            CreatedAt = DateTime.UtcNow;
            ModifiedAt = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public string Cost { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
