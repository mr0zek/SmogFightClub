using System.Collections.Generic;
using SFC.Notifications.Features.SendNotification;
using SFC.SharedKernel;

namespace SFC.Tests
{
  public class TestSmtpClient : ISmtpClient
  {
    public static List<SmtpClientEmail> SentEmails { get; set; } = new List<SmtpClientEmail>();
    public void Send(Email email, string title, string body)
    {
      SentEmails.Add(new SmtpClientEmail(email, title, body));
    }

    public static void Clear()
    {
      SentEmails.Clear();
    }
  }
}