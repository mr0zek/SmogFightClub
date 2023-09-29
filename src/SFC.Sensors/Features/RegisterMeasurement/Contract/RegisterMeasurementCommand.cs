using SFC.Infrastructure.Interfaces;
using SFC.SharedKernel;
using System;
using System.Collections.Generic;

namespace SFC.Sensors.Features.RegisterMeasurement.Contract
{
  public class RegisterMeasurementCommand : ICommand
  {
    public Guid SensorId { get; set; }
    public Dictionary<ElementName, decimal> Elements { get; set; } = new Dictionary<ElementName, decimal>();
    public DateTime Date { get; set; }
    public Guid Id { get; set; }

    public class ElementName : ValueObject, IResponse
    {
      public const int MaxLength = 10;

      private readonly string _value;

      public ElementName(string elementName)
      {
        if (string.IsNullOrEmpty(elementName) || elementName.Length > MaxLength)
          throw new ArgumentException();

        _value = elementName;
      }

      public override string ToString()
      {
        return _value;
      }

      public static implicit operator ElementName(string elementName)
      {
        return new ElementName(elementName);
      }

      public static implicit operator string(ElementName elementName)
      {
        return elementName.ToString();
      }

      protected override IEnumerable<object> GetEqualityComponents()
      {
        yield return _value;
      }
    }
  }
}