using api;
using api.Controllers;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
[AllowAnonymous]

public class UsersController : BaseApiController
{
    private readonly IUserRepository _userRepository;
    private IMapper _mapper;

    public UsersController(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        // var members = await _userRepository.GetUsersAsync();
        // return Ok(_mapper.Map<IEnumerable<MemberDto>>(members));
        return Ok(await _userRepository.GetMembersAsync());
    }

    [HttpGet("{id}")] //endpoint: /api/users/25  
    public async Task<ActionResult<MemberDto?>> GetUser(int id)
    {
        var members = await _userRepository.GetMembersAsync();
        return _mapper.Map<MemberDto>(members);
    }
    [HttpGet("username/{username}")]
    public async Task<ActionResult<MemberDto?>> GetUserByUserName(string username)
    {
        var members = await _userRepository.GetMemberAsync(username);
        return _mapper.Map<MemberDto>(members);
    }
}