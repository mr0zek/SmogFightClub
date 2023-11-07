using SFC.Infrastructure.Interfaces.Communication;
using SFC.SharedKernel;
using System.Collections.Generic;

namespace SFC.Notifications.Features.GetAllSendNotificationsByUser.Contract
{
  public class GetAllSendNotificationsByUserResponse : IResponse
  {
    public GetAllSendNotificationsByUserResponse(IEnumerable<SendNotification> result)
    {
      Result = result;
    }

    public class SendNotification : IResponse
    {
      public string LoginName { get; set; }
      
      public int Count { get; set; }

      public SendNotification(string loginName, int count)
      {
        LoginName = loginName;
        Count = count;
      }

    }

    public IEnumerable<SendNotification> Result { get; set; }
  }
}