namespace SFC.Infrastructure.Features.Tracing
{
  public interface ITraceRepository
  {
    void BeginRequest(string correlationId);
    void EndRequest(string correlationId);

    void AddCall(Trace trace);
  }
}