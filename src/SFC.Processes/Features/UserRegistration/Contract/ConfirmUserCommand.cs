using SFC.Infrastructure.Interfaces.Communication;
using System;

namespace SFC.Processes.Features.UserRegistration.Contract
{
    public class ConfirmUserCommand : ICommand
  {
    public ConfirmUserCommand(Guid confirmationId)
    {
      ConfirmationId = confirmationId;
    }

    public Guid ConfirmationId { get; set; }
  }
}