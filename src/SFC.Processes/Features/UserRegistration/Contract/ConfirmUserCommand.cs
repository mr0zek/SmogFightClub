using SFC.Infrastructure.Interfaces;
using System;

namespace SFC.Processes.Features.UserRegistration.Contract
{
  public class ConfirmUserCommand : ICommand
  {
    public Guid ConfirmationId { get; set; }
  }
}