namespace SFC.Infrastructure.Features.Tracing
{
  public class Trace
  {
    public Trace(string correlationId, string callName, string callingModuleName, string calledModuleName)
    {
      CorrelationId = correlationId;
      CallName = callName;
      CallingModuleName = callingModuleName;
      CalledModuleName = calledModuleName;
    }

    public string CorrelationId { get; }
    public string CallName { get; }

    public string CallingModuleName { get; set; }
    public string CalledModuleName { get; set; }
  }
}