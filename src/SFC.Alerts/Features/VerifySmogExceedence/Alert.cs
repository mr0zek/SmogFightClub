using SFC.SharedKernel;

namespace SFC.Alerts.Features.VerifySmogExceedence
{
  class Alert
  {
    public Alert(LoginName loginName)
    {
      LoginName = loginName;
    }

    public LoginName LoginName { get; set; }
  }
}