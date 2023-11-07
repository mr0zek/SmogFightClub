using SFC.Infrastructure.Interfaces.Communication;
using SFC.SharedKernel;
using System;

namespace SFC.Processes.Features.UserRegistration.Contract
{
    public class RegisterUserCommand : ICommand
  {
    public RegisterUserCommand(LoginName loginName, Email email, PasswordHash passwordHash, ZipCode zipCode, string baseUrl, Guid id)
    {
      LoginName = loginName;
      Email = email;
      PasswordHash = passwordHash;
      ZipCode = zipCode;
      BaseUrl = baseUrl;
      Id = id;
    }

    public LoginName LoginName { get; set; }
    public Email Email { get; set; }
    public PasswordHash PasswordHash { get; set; }
    public ZipCode ZipCode { get; set; }
    public string BaseUrl { get; set; }
    public Guid Id { get; set; }
  }
}