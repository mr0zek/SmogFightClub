namespace SFC.Sensors.Features.RegisterMeasurement
{
  internal class Sensor
  {
    public Sensor(string guid, string zipCode)
    {
      Guid = guid;
      ZipCode = zipCode;
    }

    public string Guid { get; set; }
    public string ZipCode { get; set; }
  }
}