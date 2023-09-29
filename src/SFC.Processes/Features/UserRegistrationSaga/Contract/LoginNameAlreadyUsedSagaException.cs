using System;

namespace SFC.Processes.Features.UserRegistrationSaga.Contract
{
  public class LoginNameAlreadyUsedSagaException : Exception
  {
    public string LoginName { get; }

    public LoginNameAlreadyUsedSagaException(string loginName)
    {
      LoginName = loginName;
    }
  }
}