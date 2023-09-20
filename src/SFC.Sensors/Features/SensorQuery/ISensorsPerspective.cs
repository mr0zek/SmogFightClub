using SFC.SharedKernel;

namespace SFC.Sensors.Features.SensorQuery
{
  public interface ISensorsPerspective
  {
    SensorsReadModel GetAll(LoginName getLoginName);
    SensorReadModel Get(string id, LoginName getLoginName);
  }
}