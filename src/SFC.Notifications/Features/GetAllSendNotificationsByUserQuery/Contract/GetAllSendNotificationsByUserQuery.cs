using SFC.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC.Notifications.Features.GetAllSendNotificationsByUserQuery.Contract
{
  public class GetAllSendNotificationsByUserQuery : IRequest<IEnumerable<NotificationsCountResult>>
  {

    public GetAllSendNotificationsByUserQuery(int skip, int take)
    {
      Skip = skip;
      Take = take;
    }

    public int Skip { get; set; }
    public int Take { get; set; }
  }
}
