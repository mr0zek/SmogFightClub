using System.Collections;
using SFC.SharedKernel;

namespace SFC.Sensors.Contract.Query
{
  public interface ISensorsPerspective
  {
    SensorsReadModel GetAll(LoginName getLoginName);
    SensorReadModel Get(string id, LoginName getLoginName);
  }
}