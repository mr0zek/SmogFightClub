using SFC.Infrastructure.Interfaces.Documentation;
using SFC.SharedKernel;

namespace SFC.Infrastructure.Interfaces.Smtp
{  
  public interface ISmtpClient
  {
    [ExitPointTo("SmtpProvider", CallType.Command)]
    void Send(Email email, string title, string body);
  }
}