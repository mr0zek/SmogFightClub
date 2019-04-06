using SFC.SharedKernel;

namespace SFC.Tests
{
  public class SmtpClientEmail
  {
    public Email Email { get; }
    public string Title { get; }
    public string Body { get; }

    public SmtpClientEmail(Email email, string title, string body)
    {
      Email = email;
      Title = title;
      Body = body;
    }
  }
}