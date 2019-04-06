using SFC.SharedKernel;

namespace SFC.Notifications.Features.SendNotification
{
  public interface ISmtpClient
  {
    void Send(Email email, string title, string body);
  }
}