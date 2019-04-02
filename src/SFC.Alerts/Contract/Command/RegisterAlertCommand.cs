using SFC.SharedKernel;

namespace SFC.Alerts.Contract.Command
{
  public class RegisterAlertCommand
  {
    public LoginName LoginName { get; set; }
    public ZipCode ZipCode { get; set; }
  }
}