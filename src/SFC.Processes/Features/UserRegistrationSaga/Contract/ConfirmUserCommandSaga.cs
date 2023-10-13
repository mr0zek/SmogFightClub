using SFC.Infrastructure.Interfaces.Communication;

namespace SFC.Processes.Features.UserRegistrationSaga.Contract
{
    public class ConfirmUserCommandSaga : ICommand
  {
    public string ConfirmationId { get; set; }
  }
}