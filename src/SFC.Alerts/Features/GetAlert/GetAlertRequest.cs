using SFC.Infrastructure.Interfaces.Communication;
using SFC.SharedKernel;
using System;

namespace SFC.Alerts.Features.GetAlert
{
    public class GetAlertRequest : IRequest<GetAlertResponse>
  {
    public GetAlertRequest(Guid id, LoginName loginName)
    {
      Id = id;
      LoginName = loginName;
    }

    public Guid Id { get; set; }

    public LoginName LoginName { get; set; }
  }
}
