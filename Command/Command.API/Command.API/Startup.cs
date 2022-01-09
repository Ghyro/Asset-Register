using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using Microsoft.EntityFrameworkCore;

namespace Command.API
{
  using Command.API.AsyncDataMessaging;
  using Command.API.Database;
  using Command.API.Infrastructure.Interfaces;  

  public class Startup
	{
    public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
      services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMen"));
      services.AddHostedService<MessageBusSub>();
      services.AddSingleton<IEventProcessor, EventProcessor.EventProcessor>();
      services.AddScoped<IRepository, Repository>();
      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
      services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Command.API", Version = "v1" });
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Command.API v1"));
			}

      Console.WriteLine("Command API Service started...");

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
