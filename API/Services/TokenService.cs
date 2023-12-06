<<<<<<< HEAD
﻿using System.IdentityModel.Tokens.Jwt;
=======
﻿using System;
>>>>>>> 0a9a98588bb7951bf504d2963d7f39a572322670
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
<<<<<<< HEAD
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class TokenService : ITokenService
{
    readonly SymmetricSecurityKey _privateKey;
    public TokenService(IConfiguration config)
    {
        _privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]!));
    }
=======
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace API.Service;

public class TokenService : ITokenService
{

    readonly SymmetricSecurityKey _privateKey;
    public TokenService(IConfiguration configuration)
    {
        _privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]!));
    }

>>>>>>> 0a9a98588bb7951bf504d2963d7f39a572322670
    public string CreateToken(AppUser user)
    {
        var claims = new List<Claim> {
            new(JwtRegisteredClaimNames.NameId, user.UserName!)
        };
<<<<<<< HEAD
        var credentials = new SigningCredentials(_privateKey, SecurityAlgorithms.HmacSha256Signature);
=======

        var credentials = new SigningCredentials(_privateKey, SecurityAlgorithms.HmacSha256Signature);

>>>>>>> 0a9a98588bb7951bf504d2963d7f39a572322670
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(3),
            SigningCredentials = credentials
        };
<<<<<<< HEAD
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
=======

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);


        return tokenHandler.WriteToken(token);
    }
}

internal class JwtSecurityTokenHandler
{
    public JwtSecurityTokenHandler()
    {
    }

    internal object CreateToken(SecurityTokenDescriptor tokenDescriptor)
    {
        throw new NotImplementedException();
    }

    internal string WriteToken(object token)
    {
        throw new NotImplementedException();
    }
>>>>>>> 0a9a98588bb7951bf504d2963d7f39a572322670
}