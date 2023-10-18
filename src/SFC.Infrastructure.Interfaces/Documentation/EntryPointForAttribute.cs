using System;

namespace SFC.Infrastructure.Interfaces.Documentation
{
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
  public class EntryPointForAttribute : Attribute
  {
    public EntryPointForAttribute(string callerName, CallerType callerType, CallType callType)
    {
      CallerName = callerName;
      CallerType = callerType;
      CallType = callType;
    }

    public string CallerName { get; }
    public CallerType CallerType { get; }
    public CallType CallType { get; }
  }
}