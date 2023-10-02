using SFC.Infrastructure.Interfaces;
using SFC.SharedKernel;

namespace SFC.Alerts.Features.GetAlert
{
  public class GetAlertRequest : IRequest<GetAlertResponse>
  {
    public GetAlertRequest(string id, LoginName loginName)
    {
      Id = id;
      LoginName = loginName;
    }

    public string Id { get; set; }

    public LoginName LoginName { get; set; }
  }
}
