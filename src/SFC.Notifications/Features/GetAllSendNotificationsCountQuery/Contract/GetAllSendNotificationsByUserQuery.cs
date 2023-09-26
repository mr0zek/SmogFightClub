using SFC.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC.Notifications.Features.GetAllSendNotificationsCountQuery.Contract
{
  public class GetAllSendNotificationsCountRequest : IRequest<IEnumerable<NotificationsCountResult>>
  {

    public GetAllSendNotificationsCountRequest(int skip, int take)
    {
      Skip = skip;
      Take = take;
    }

    public int Skip { get; set; }
    public int Take { get; set; }
  }
}
