using System.Collections.Generic;
using SFC.Notifications.Features.SendNotification;
using SFC.SharedKernel;

namespace SFC.Tests
{
  public class TestSmtpClient : ISmtpClient
  {
    public List<SmtpClientEmail> SentEmails { get; set; }
    public void Send(Email email, string title, string body)
    {
      SentEmails.Add(new SmtpClientEmail(email, title, body));
    }
  }
}