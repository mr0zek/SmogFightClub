using SFC.SharedKernel;
using System;

namespace SFC.Sensors.Features.SensorQuery.Contract
{
    public interface ISensorsPerspective
    {
        SensorsReadModel GetAll(LoginName getLoginName);
        SensorReadModel Get(Guid id, LoginName getLoginName);
    }
}