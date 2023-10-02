using SFC.SharedKernel;

namespace SFC.Infrastructure.Interfaces
{
  public interface ISmtpClient
  {
    void Send(Email email, string title, string body);
  }
}