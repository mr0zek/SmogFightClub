using Autofac;
using Hangfire;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Infrastructure.Interfaces.TimeDependency;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SFC.Infrastructure.Features.TimeDependency
{
  internal class HangFireScheduler : IScheduler
  {
    private readonly IComponentContext _componentContext;

    public HangFireScheduler(IComponentContext componentContext)
    {
      _componentContext = componentContext;
    }
    public void RegisterRecurrentTasks()
    {
      var eventHandlers = _componentContext.Resolve<IEnumerable<IEventHandler<TimeEvent>>>();

      foreach (var eventHandler in eventHandlers)
      {
        var crontabAttribute = eventHandler
          .GetType()
          .CustomAttributes
          .FirstOrDefault(f => f.AttributeType == typeof(CrontabAttribute));
        if (crontabAttribute == null)
        {
          throw new TimeConfigurationException();
        }

        string crontab = crontabAttribute.ConstructorArguments[0].Value.ToString();

        var jobId = eventHandler.GetType().Name;
        RecurringJob.AddOrUpdate<HandlerActivator>(jobId, x => x.Run(eventHandler.GetType()), () => crontab);
      }
    }
  }
}
