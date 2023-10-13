using SFC.Infrastructure.Interfaces.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC.Notifications.Features.GetAllSendNotificationsCount.Contract
{
    public class GetAllSendNotificationsCountRequest : IRequest<GetAllSendNotificationsCountResponse>
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
