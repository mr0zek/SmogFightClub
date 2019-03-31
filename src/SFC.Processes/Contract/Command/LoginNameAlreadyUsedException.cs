using System;

namespace SFC.Processes.Features.UserRegistration
{
  public class LoginNameAlreadyUsedException : Exception
  {
    public string LoginName { get; }

    public LoginNameAlreadyUsedException(string loginName)
    {
      LoginName = loginName;
    }
  }
}