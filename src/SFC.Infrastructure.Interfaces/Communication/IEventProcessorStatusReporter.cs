namespace SFC.Infrastructure.Interfaces.Communication
{
  public interface IEventProcessorStatusReporter
  {
    void ReportStatus(EventProcesorStatus status);
  }
}