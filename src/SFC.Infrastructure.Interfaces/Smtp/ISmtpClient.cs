using SFC.SharedKernel;

namespace SFC.Infrastructure.Interfaces.Smtp
{  
  public interface ISmtpClient
  {
    Task Send(Email email, string title, string body);
  }
}