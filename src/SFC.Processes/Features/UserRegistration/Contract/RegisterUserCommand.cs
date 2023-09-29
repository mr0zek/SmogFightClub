﻿using SFC.Infrastructure.Interfaces;
using SFC.SharedKernel;

namespace SFC.Processes.Features.UserRegistration.Contract
{
  public class RegisterUserCommand : ICommand
  {
    public LoginName LoginName { get; set; }
    public Email Email { get; set; }
    public string Password { get; set; }
    public ZipCode ZipCode { get; set; }
    public string BaseUrl { get; set; }
    public string Id { get; set; }
  }
}