using api;
using API.Data;
using API.Helpers;
using API.Interfaces;
using API.SignalR;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class AppServiceExtensions
{
  public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration conf)
  {
    services.AddDbContext<DataContext>(opt =>
    {
      opt.UseSqlite(conf.GetConnectionString("SqliteConnection"));
    });
    services.AddCors();
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    services.Configure<CloudinarySettings>(conf.GetSection("CloudinarySettings"));
    services.AddScoped<IImageService, ImageService>();
    services.AddScoped<LogUserActivity>();
    services.AddScoped<ITokenService, TokenService>();
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IlikesRepository, LikesRepository>();
    services.AddScoped<IMessageRepository, MessageRepository>();
     services.AddSignalR();
      services.AddSingleton<PresenceTracker>();

    return services;
  }
}