using SFC.SharedKernel;

namespace SFC.Notifications
{
  internal interface ISmtpClient
  {
    void Send(Email email, string title, string body);
  }
}