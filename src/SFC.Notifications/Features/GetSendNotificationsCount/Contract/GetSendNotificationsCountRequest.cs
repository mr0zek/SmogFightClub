using SFC.Infrastructure.Interfaces.Communication;
using SFC.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC.Notifications.Features.GetSendNotificationsCount.Contract
{

  public class GetSendNotificationsCountRequest : IRequest<GetSendNotificationsCountResponse>
  {
    public string NotificationType { get; set; }
    public LoginName[] LoginNames { get; set; }

    public GetSendNotificationsCountRequest(string notificationType, LoginName[] loginNames)
    {
      NotificationType = notificationType;
      LoginNames = loginNames;
    }
  }
}
