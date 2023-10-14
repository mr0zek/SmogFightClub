using SFC.Infrastructure.Interfaces.Communication;
using SFC.Infrastructure.Interfaces.TimeDependency;

namespace SFC.GiosGateway.Features
{
  [Crontab("* */0 * * * ")]
  public class SynchronizeReadings : IEventHandler<TimeEvent>
  {
    public void Handle(TimeEvent @event)
    {
      throw new NotImplementedException();
    }
  }
}