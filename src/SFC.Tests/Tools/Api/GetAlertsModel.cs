﻿using System.Collections.Generic;

namespace SFC.Tests.Tools.Api
{
    public class GetAlertsModel
    {
        public IEnumerable<GetAlertResult>? Alerts { get; set; }
    }
}