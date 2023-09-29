using SFC.Infrastructure.Interfaces;

namespace SFC.Processes.Features.UserRegistration.Contract
{
  public class ConfirmUserCommand : ICommand
  {
    public string ConfirmationId { get; set; }
  }
}