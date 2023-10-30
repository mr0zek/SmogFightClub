using SFC.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFC.Alerts.Features.VerifySmogExceedence
{
    internal interface IAlertReadRepository
    {
        Task<IEnumerable<Alert>> GetByZipCode(string zipCode);
    }
}