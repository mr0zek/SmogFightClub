using SFC.Infrastructure.Interfaces.Communication;
using SFC.SharedKernel;
using System.Collections.Generic;

namespace SFC.Notifications.Features.GetSendNotificationsCount.Contract
{
    public class GetSendNotificationsCountResponse : IResponse
  {
    public GetSendNotificationsCountResponse(IEnumerable<SendNotificaton> result)
    {
      Result = result;
    }

    public class SendNotificaton : IResponse
    {
      public LoginName LoginName { get; set; }
      public int Count { get; set; }
    }

    public IEnumerable<SendNotificaton> Result { get; set; }    
  }
}
