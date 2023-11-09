namespace MediatR.Asynchronous
{
  public interface IAsyncProcessorStatusReporter
  {
    void ReportStatus(AsyncProcesorStatus status);
  }
}