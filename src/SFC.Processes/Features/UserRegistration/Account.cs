using SFC.SharedKernel;
using System;

namespace SFC.Processes.Features.UserRegistration
{
  internal class Account
  {
    public Account() { }
    public Account(Guid id, Email email, LoginName loginName, ZipCode zipCode, string passwordHash)
    {
      Email = email;
      Id = id;
      LoginName = loginName;
      ZipCode = zipCode; 
      PasswordHash = passwordHash;
    }

    public Email Email { get; set; }
    public Guid Id { get; set; }
    public LoginName LoginName { get; set; }
    public ZipCode ZipCode { get; set; }    
    public string PasswordHash { get; set;}
  }
}