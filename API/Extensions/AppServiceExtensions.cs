<<<<<<< HEAD
﻿using System.Reflection.Metadata.Ecma335;
using System.Text;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
=======
﻿using API.Data;
using API.Interfaces;
using API.Service;
using Microsoft.EntityFrameworkCore;
>>>>>>> 0a9a98588bb7951bf504d2963d7f39a572322670

namespace API.Extensions;

public static class AppServiceExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration conf)
    {
<<<<<<< HEAD
        services.AddDbContext<DataContext>(otp =>
        {
            otp.UseSqlite(conf.GetConnectionString("DefaultConnection"));
        });
        services.AddCors();
        services.AddScoped<ITokenService, TokenService>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);


        return services;
    }

    private static void AddJwtBearer(Action<object> value)
    {
        throw new NotImplementedException();
    }
=======
        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlite(conf.GetConnectionString("DefaultConnection"));
        });
        services.AddCors();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
>>>>>>> 0a9a98588bb7951bf504d2963d7f39a572322670
}