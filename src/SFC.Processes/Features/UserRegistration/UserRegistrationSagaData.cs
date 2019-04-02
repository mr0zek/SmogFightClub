using System;
using SFC.SharedKernel;

namespace SFC.Processes.Features.UserRegistration
{
  public class UserRegistrationSagaData
  {
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public Email Email { get; set; }
    public LoginName LoginName { get; set; }
    public string PasswordHash { get; set; }
    public ZipCode ZipCode { get; set; }
    public string BaseUrl { get; set; }
  }
}