using SFC.Infrastructure.Interfaces.Communication;
using SFC.Infrastructure.Interfaces.Documentation;
using SFC.Infrastructure.Interfaces.TimeDependency;

namespace SFC.GiosGateway.Features
{
  [Crontab("0 0 * * * ")]  
  public class SynchronizeReadings : IEventHandler<TimeEvent>
  {
    [ExitPointTo("GIOS", CallType.Query)]
    public void Handle(TimeEvent @event)
    {
      throw new NotImplementedException();
    }
  }
}