using SFC.SharedKernel;

namespace SFC.AdminApi.Features.Dashboard
{
  public class DashboardEntry
  {
    public LoginName LoginName { get; set; }
    public int AlertsSentCount { get; set; }
  }
}