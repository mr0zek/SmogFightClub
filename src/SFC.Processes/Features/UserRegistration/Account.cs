using SFC.SharedKernel;

namespace SFC.Processes.Features.UserRegistration
{
  internal class Account
  {
    public Account() { }
    public Account(string id, Email email, LoginName loginName, ZipCode zipCode, PasswordHash passwordHash)
    {
      Email = email;
      Id = id;
      LoginName = loginName;
      ZipCode = zipCode; 
      PasswordHash = passwordHash;
    }

    public Email Email { get; set; }
    public string Id { get; set; }
    public LoginName LoginName { get; set; }
    public ZipCode ZipCode { get; set; }    
    public PasswordHash PasswordHash { get; set;}
  }
}