namespace SFC.Infrastructure.Features.Tracing
{
  internal class Call
  {
    public Call(string moduleName, string callName)
    {
      ModuleName = moduleName;
      CallName = callName;
    }

    public string ModuleName { get; }
    public string CallName { get; }
  }
}