using SFC.Infrastructure.Interfaces.Communication;
using SFC.SharedKernel;
using System.Collections.Generic;

namespace SFC.Notifications.Features.GetAllSendNotificationsCount.Contract
{
    public class GetAllSendNotificationsCountResponse : IResponse
  {
    public GetAllSendNotificationsCountResponse(IEnumerable<SendNotification> result)
    {
      Result = result;
    }

    public class SendNotification : IResponse
    {
      public LoginName LoginName { get; set; }
      public int Count { get; set; }
    }

    public IEnumerable<SendNotification> Result { get;}
  }
}