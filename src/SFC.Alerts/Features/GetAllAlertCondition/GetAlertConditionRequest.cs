using SFC.Infrastructure.Interfaces;
using SFC.SharedKernel;

namespace SFC.Alerts.Features.GetAllAlertCondition
{
  public class GetAlertConditionRequest : IRequest<GetAlertConditionResponse>
  {
    public GetAlertConditionRequest(string id, LoginName loginName)
    {
      Id = id;
      LoginName = loginName;
    }

    public string Id { get; set; }

    public LoginName LoginName { get; set; }
  }

}
