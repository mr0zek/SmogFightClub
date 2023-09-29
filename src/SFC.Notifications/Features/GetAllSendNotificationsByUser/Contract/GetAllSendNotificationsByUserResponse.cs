using SFC.Infrastructure.Interfaces;
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
      public LoginName LoginName { get; set; }
      public int Count { get; set; }
    }

    public IEnumerable<SendNotification> Result { get; set; }
  }
}