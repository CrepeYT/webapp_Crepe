
using System.Security.Cryptography;
using System.Text;
using api;
using API.Controllers;
using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController : BaseApiController
{
  private readonly DataContext _dataContext;
  private readonly ITokenService _tokenService;
  private readonly IMapper _mapper;

  public AccountController(DataContext dataContext, ITokenService tokenService, IMapper mapper)
  {
    _dataContext = dataContext;
    _tokenService = tokenService;
    _mapper = mapper;

  }

  [HttpPost("register")]
  public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
  {
    if (await isUserExists(registerDto.Username!))
      return BadRequest("Username is taken");
    var user = _mapper.Map<AppUser>(registerDto);
    using var hMACSHA256 = new HMACSHA256();

    user.UserName = registerDto.Username!.Trim().ToLower();
    user.PasswordHash = hMACSHA256.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password!.Trim()));
    user.PasswordSalt = hMACSHA256.Key;

    _dataContext.Users.Add(user);
    await _dataContext.SaveChangesAsync();

    return new UserDto
    {
      Username = user.UserName,
      Token = _tokenService.CreateToken(user),
      Aka = user.Aka,
      Gender = user.Gender
    };
  }

  [HttpPost("login")]
  public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
  {
    var user = await _dataContext.Users.Include(photo => photo.Photos).SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

    if (user == null)
      return Unauthorized("Invalid username");


    using var hmac = new HMACSHA256(user.PasswordSalt);

    var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

    for (int i = 0; i < computedHash.Length; i++)
    {
      if (computedHash[i] != user.PasswordHash[i])
        return Unauthorized("Invalid password");
    }

    return new UserDto
    {
      Username = user.UserName,
      Token = _tokenService.CreateToken(user),
      PhotoUrl = user.Photos.FirstOrDefault(photo => photo.IsMain)?.Url,
      Aka = user.Aka,
      Gender = user.Gender
    };
  }

  private async Task<bool> isUserExists(string username)
  {
    return await _dataContext.Users.AnyAsync(x => x.UserName == username.ToLower());
  }
}
