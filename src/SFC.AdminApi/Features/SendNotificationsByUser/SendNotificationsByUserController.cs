﻿using Microsoft.AspNetCore.Mvc;
using SFC.Infrastructure.Interfaces;
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

    [HttpGet]
    public IActionResult Get([FromQuery] SendNotificationsByUserModel query)
    {
      return Json(_query.Query(new GetAllSendNotificationsByUserRequest(query.Top, query.Take)));
    }
  }
}
