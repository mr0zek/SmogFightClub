namespace SFC.Infrastructure.Features.Tracing
{
  internal class Call
  {
    public Call(string moduleName, string callName, string type)
    {
      ModuleName = moduleName;
      CallName = callName;
      Type = type;
    }

    public string ModuleName { get; }
    public string CallName { get; }
    public string Type { get; }
  }
}