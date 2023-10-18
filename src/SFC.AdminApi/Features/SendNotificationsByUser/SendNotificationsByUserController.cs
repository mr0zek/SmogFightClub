using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Infrastructure.Interfaces.Documentation;
using SFC.Notifications.Features.GetAllSendNotificationsByUser.Contract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC.AdminApi.Features.SendNotificationsByUser
{
  [Authorize]
  [ApiVersion("1.0")]
  [Route("api/v{version:apiVersion}/admin/[controller]")]
  [ApiController]  
  public class SendNotificationsByUserController : Controller
  {
    private readonly IQuery _query;

    public SendNotificationsByUserController(IQuery query)
    {
      _query = query;
    }

    [EntryPointFor("Admin", CallerType.Human, CallType.Query)]
    [HttpGet]
    public ActionResult<GetAllSendNotificationsByUserResponse> Get([FromQuery] SendNotificationsByUserModel query)
    {
      return _query.Query(new GetAllSendNotificationsByUserRequest(query.Skip, query.Take));
    }
  }
}
