namespace MediatR.Asynchronous
{
  public interface IMessagesProcessorStatusReporter
  {
    void ReportStatus(MessagesProcesorStatus status);
  }
}