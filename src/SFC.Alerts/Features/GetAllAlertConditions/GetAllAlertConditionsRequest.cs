using SFC.Infrastructure.Interfaces;
using SFC.SharedKernel;

namespace SFC.Alerts.Features.GetAllAlertConditions
{
  public class GetAllAlertConditionsRequest : IRequest<GetAllAlertConditionsResponse>
  {
    public GetAllAlertConditionsRequest(LoginName loginName)
    {
      LoginName = loginName;
    }

    public LoginName LoginName { get; internal set; }
  }
}
