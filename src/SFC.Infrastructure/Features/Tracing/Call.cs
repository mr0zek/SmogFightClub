namespace SFC.Infrastructure.Features.Tracing
{
  internal class Call
  {
    public Call(string calledModuleName, string callName, string type, string callingModuleName)
    {
      CalledModuleName = calledModuleName;
      CallName = callName;
      Type = type;
      CallingModuleName = callingModuleName;
    }

    public string CalledModuleName { get; }
    public string CallName { get; }
    public string Type { get; }
    public string CallingModuleName { get; }
  }
}