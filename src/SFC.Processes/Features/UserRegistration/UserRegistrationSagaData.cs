using System;
using SFC.SharedKernel;

namespace SFC.Processes.Features.UserRegistration
{
  public class UserRegistrationSagaData
  {
    public string Id { get; set; }
    public string Email { get; set; }
    public string LoginName { get; set; }
    public string PasswordHash { get; set; }
    public string ZipCode { get; set; }
    public string BaseUrl { get; set; }
  }
}