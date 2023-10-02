using SFC.SharedKernel;
using System.Collections.Generic;

namespace SFC.Alerts.Infrastructure
{
    internal interface IAlertRepository
    {
        void Add(ZipCode zipCode, LoginName loginName);
        bool Exists(ZipCode zipCode, LoginName loginName);
        IEnumerable<Alert> GetByZipCode(string zipCode);
    }
}