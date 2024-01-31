﻿using System;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class MessagesController : BaseApiController
{
    private readonly IUserRepository _userRepository;
    private readonly IMessageRepository _messageRepository;
    private readonly IMapper _mapper;

    public MessagesController(IUserRepository userRepository, 
        IMessageRepository messageRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _messageRepository = messageRepository;
        _mapper = mapper;
    }
    [HttpPost]
public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto){
  if (createMessageDto.RecipientUsername is null) return NotFound();
  var username = User.GetUsername();
  if (username is null) return NotFound();
  if (username == createMessageDto?.RecipientUsername?.ToLower()) return BadRequest("can't send to yourself!");

  var sender = await _userRepository.GetUserByUserNameAsync(username); //คนส่ง
  var recipient = await _userRepository.GetUserByUserNameAsync(createMessageDto!.RecipientUsername); //คนรับ
  if (recipient is null || sender is null) return NotFound(); //เช็คว่าไม่เป็นnull
  var message = new Message
  {
      Sender = sender,
      Recipient = recipient,
      Content = createMessageDto.Content,
      SenderUsername = sender.UserName,
      RecipientUsername = recipient.UserName,
  };

  _messageRepository.AddMessage(message);
  if (await _messageRepository.SaveAllAsync()) return Ok(_mapper.Map<MessageDto>(message));
  return BadRequest("Something has gone wrong!");
}
  [HttpGet]
  public async Task<ActionResult<PageList<MessageDto>>> GetUserMessages([FromQuery] MessageParams messageParams)
  {
    messageParams.Username = User.GetUsername();
    if (messageParams.Username is null) return NotFound();

    var messages = await _messageRepository.GetUserMessages(messageParams);

    var paginationHeader = new PaginationHeader(messages.CurrentPage, 
                            messages.PageSize, 
                            messages.TotalCount, 
                            messages.TotalPages);

    Response.AddPaginationHeader(paginationHeader);

    return messages;
  }
  [HttpGet("thread/{username}")]
  public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageThread(string username)
  {
      var thisUserName = User.GetUsername();
      if (thisUserName is null) return NotFound();

      var messages = await _messageRepository.GetMessageThread(thisUserName, username);
      return Ok(messages);
  }
}