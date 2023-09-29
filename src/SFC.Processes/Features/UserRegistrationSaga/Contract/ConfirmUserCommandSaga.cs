using SFC.Infrastructure.Interfaces;

namespace SFC.Processes.Features.UserRegistrationSaga.Contract
{
  public class ConfirmUserCommandSaga : ICommand
  {
    public string ConfirmationId { get; set; }
  }
}