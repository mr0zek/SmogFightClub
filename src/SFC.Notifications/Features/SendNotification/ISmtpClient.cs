using SFC.SharedKernel;

namespace SFC.Notifications.Features.SendNotification
{
  internal interface ISmtpClient
  {
    void Send(Email email, string title, string body);
  }
}