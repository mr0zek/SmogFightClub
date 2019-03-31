namespace SFC.Notifications
{
  internal interface IEmailRepository
  {
    void Set(string loginName, string email);
  }
}
