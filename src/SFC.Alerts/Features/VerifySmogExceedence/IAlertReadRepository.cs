using SFC.SharedKernel;
using System.Collections.Generic;

namespace SFC.Alerts.Features.VerifySmogExceedence
{
    internal interface IAlertReadRepository
    {
        IEnumerable<Alert> GetByZipCode(string zipCode);
    }
}