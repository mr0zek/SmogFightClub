namespace SFC.Infrastructure.Interfaces.Documentation
{
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
  public class ExitPointToAttribute : Attribute
  {
    public ExitPointToAttribute(string calledName,CallType callType, string? description = null)
    {
      CalledName = calledName;
      CallType = callType;
      Description = description;
    }

    public string CalledName { get; }
    public CallType CallType { get; }
    public string? Description { get; }
  }
}