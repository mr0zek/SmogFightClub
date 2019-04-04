using SFC.SharedKernel;

namespace SFC.Sensors.Features.RegisterMeasurement.Query
{
  public interface ISensorsPerspective
  {
    SensorsReadModel GetAll(LoginName getLoginName);
    SensorReadModel Get(string id, LoginName getLoginName);
  }
}