using System;

namespace SFC.Processes.Features.UserRegistration
{
  public class UserRegistrationSagaData
  {
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Email { get; set; }
    public string LoginName { get; set; }
    public string PasswordHash { get; set; }
    public string ZipCode { get; set; }
    public string BaseUrl { get; set; }
  }
}