using System.Collections.Generic;
using SFC.SharedKernel;

namespace SFC.Sensors.Features.RegisterMeasurement.Contract
{
    public class ElementName : ValueObject
    {
        public const int MaxLength = 10;

        private readonly string _value;

        public ElementName(string elementName)
        {
            if (string.IsNullOrEmpty(elementName) || elementName.Length > MaxLength)
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