using Microsoft.EntityFrameworkCore;


namespace Command.API.Database
{
  using Command.API.Infrastructure.Models;

  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {

    }

    public DbSet<PlatformModel> Platforms { get; set; }
    public DbSet<CommandModel> Commands { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder
          .Entity<PlatformModel>()
          .HasMany(p => p.CommandModels)
          .WithOne(p => p.PlatformModel)
          .HasForeignKey(p => p.PlatformId);

      modelBuilder
          .Entity<CommandModel>()
          .HasOne(p => p.PlatformModel)
          .WithMany(p => p.CommandModels)
          .HasForeignKey(p => p.PlatformId);
    }
  }
}
