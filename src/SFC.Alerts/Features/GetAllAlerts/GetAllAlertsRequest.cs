using SFC.Infrastructure.Interfaces.Communication;
using SFC.SharedKernel;

namespace SFC.Alerts.Features.GetAllAlerts
{
    public class GetAllAlertsRequest : IRequest<GetAllAlertsResponse>
  {
    public GetAllAlertsRequest(LoginName loginName)
    {
      LoginName = loginName;
    }

    public LoginName LoginName { get; internal set; }
  }
}
