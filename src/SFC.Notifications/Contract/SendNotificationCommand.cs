namespace SFC.Notifications.Contract
{
  public class SendNotificationCommand
  {
    public string LoginName { get; set; }
    public string Body { get; set; }
    public string Title { get; set; }
  }
}