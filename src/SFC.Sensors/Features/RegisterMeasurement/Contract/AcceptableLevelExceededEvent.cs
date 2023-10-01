using SFC.Infrastructure.Interfaces;

namespace SFC.Sensors.Features.RegisterMeasurement.Contract
{
  public class AcceptableLevelExceededEvent : IEvent
  {
    public AcceptableLevelExceededEvent(string zipCode)
    {
      ZipCode = zipCode;
    }

    public string ZipCode { get; }
  }
}