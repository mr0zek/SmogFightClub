using SFC.Infrastructure.Interfaces;
using SFC.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC.Notifications.Features.GetSendNotificationsCountQuery.Contract
{

  public class GetSendNotificationsCountRequest : IRequest<IEnumerable<NotificationsCountResult>>
  {
    public GetSendNotificationsCountRequest(string notificationType, LoginName[] loginNames)
    {
      NotificationType = notificationType;
      LoginNames = loginNames;
    }

    public string NotificationType { get; set; }
    public LoginName[] LoginNames { get; set; }
  }
}
