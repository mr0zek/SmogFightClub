using System;
using System.Runtime.Serialization;

namespace SFC.Infrastructure.Features.TimeDependency
{
  [Serializable]
  internal class TimeConfigurationException : Exception
  {
    public TimeConfigurationException()
    {
    }

    public TimeConfigurationException(string message) : base(message)
    {
    }

    public TimeConfigurationException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected TimeConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
  }
}