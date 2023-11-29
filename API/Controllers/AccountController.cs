using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DIOs;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Company.ClassLibrary1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController : BaseApiController
{
    private readonly DataContext _dataContext;
    private readonly ITokenService _tokenServices;

    public AccountController(DataContext dataContext, ITokenService tokenServices)
    {
        _dataContext = dataContext;
        _tokenServices = tokenServices;
    }

    [HttpPost("register")] //ApiController automatically binds the object
    public async Task<ActionResult<UserDTO>> Register(RegisterDto registerDto)
    {
        if (await isUserExists(registerDto.UserName!))
            return BadRequest("username is already exists");

        using var hmacSHA256 = new HMACSHA256();
        var user = new AppUser
        {
            UserName = registerDto.UserName.Trim().ToLower(),
            PasswordHash = hmacSHA256.ComputeHash(Encoding.UTF8.GetBytes(registerDto.password.Trim())),
            PasswordSalt = hmacSHA256.Key
        };

        _dataContext.Users.Add(user);
        await _dataContext.SaveChangesAsync();

        return new UserDTO
        {
            UserName = user.UserName,
            Token = _tokenServices.CreateToken(user)
        };
    }
    [HttpPost("login")]
    public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDto)
    {
        var user = await _dataContext.Users.SingleOrDefaultAsync(user =>
                            user.UserName == loginDto.UserName);

        if (user is null) return Unauthorized("invalid username");

        using var hmacSHA256 = new HMACSHA256(user.PasswordSalt!);

        var computedHash = hmacSHA256.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password!.Trim()));
        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash?[i]) return Unauthorized("invalid password");
        }
        return new UserDTO
        {
            UserName = user.UserName,
            Token = _tokenServices.CreateToken(user)
        };
    }

    private async Task<bool> isUserExists(string username)
    {
        return await _dataContext.Users.AnyAsync(user => user.UserName == username.ToLower());
    }
}