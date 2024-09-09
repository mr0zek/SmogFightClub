using MediatR.NotificationPipeline;
using System.Transactions;

namespace MediatR.Asynchronous
{
  public class NotificationPipelineTransaction<T> : INotificationPipelineBehavior<T> where T : INotification
  {
    private readonly IInboxRepository _inbox;
    private readonly IDateTimeProvider _dateTimeProvider;

    public NotificationPipelineTransaction(IInboxRepository inbox, IDateTimeProvider dateTimeProvider)
    {
      _inbox = inbox;
      _dateTimeProvider = dateTimeProvider;
    }

    public async Task Handle(T notification, EventHandlerDelegate next, INotificationHandler<T> handler, CancellationToken cancellationToken)
    {
      var txOptions = new TransactionOptions();
      using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, txOptions, TransactionScopeAsyncFlowOption.Enabled))
      {
        var id = (int)notification.GetType().InvokeMember(
          "get_Id", 
          System.Reflection.BindingFlags.GetProperty, 
          null, 
          notification, 
          new object[] { })!;
        if (!await _inbox.SetProcessed(id, _dateTimeProvider.Now(), handler.GetType().FullName!))
        {
          throw new InvalidOperationException("Concurrency exception");
        }

        next();

        ts.Complete();
      }
    }
  }
}