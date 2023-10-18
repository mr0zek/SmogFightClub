namespace SFC.Infrastructure.Features.Tracing
{
  public class Trace
  {
    public Trace(string correlationId, string callName, string callingModuleName, string calledModuleName, string callType)
    {
      CorrelationId = correlationId;
      CallName = callName;
      CallingModuleName = callingModuleName;
      CalledModuleName = calledModuleName;
      CallType = callType;
    }

    public string CorrelationId { get; }
    public string CallName { get; }

    public string CallingModuleName { get; set; }
    public string CalledModuleName { get; set; }
    public string CallType { get; }
  }
}