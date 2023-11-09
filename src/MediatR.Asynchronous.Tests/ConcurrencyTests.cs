using MediatR.Asynchronous.MsSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;

namespace MediatR.Asynchronous.Tests
{
  /// <summary>
  /// Test scenarios
  /// 1. Performace test
  /// 2. Muliple instances of processor
  /// </summary>
  public class ConcurrencyTests
  {
    private IAsyncMediator _mediator;
    private INotificationAsyncProcessor _processor;

    public ConcurrencyTests()
    {
      var confBuilder = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json");
      var configuration = confBuilder.Build();
      var connectionString = configuration["ConnectionStrings:DefaultConnection"] ?? throw new NullReferenceException("ConnectionString");

      var services = new ServiceCollection();

      services.AddSingleton(TextWriter.Null);

      services.AddMediatR(cfg =>
      {
        cfg.RegisterServicesFromAssemblyContaining(typeof(Ping));
      });

      services.AddMediatRAsynchronous(cfg =>
      {
        cfg.InboxRepository = sp => new InboxRepository(connectionString);
        cfg.OutboxRepository = sp => new OutboxRepository(connectionString);
      });

      var provider = services.BuildServiceProvider();

      _mediator = provider.GetRequiredService<IAsyncMediator>();
      _processor = provider.GetRequiredService<INotificationAsyncProcessor>();

      ResetDatabase.Reset(connectionString);
      DatabaseMigrator.Run(connectionString);

      _processor.Start("testModuleName");      
    }

    ~ConcurrencyTests()
    {
      _processor.Stop();
      _processor.WaitForShutdown();
    }

    [Fact]
    public async Task SendingRequests()
    {
      int count = 1000;
      for (int i = 0; i < count; i++)
      {
        await _mediator.Send(new Ping(i, "testmessage"));
      }
      
      while(PingHandler.Requests.Count != count)
      {
        Thread.Sleep(10);
      }
      for(int i = 0;i< PingHandler.Requests.Count;i++)
      {
        Assert.Equal(i, PingHandler.Requests[i]);
      }
    }

    [Fact]
    public async Task PublishingNotifications()
    {
      int count = 1000;
      for (int i = 0; i < count; i++)
      {
        await _mediator.Publish(new Pinged(i));
      }

      while (PingedHandler.Requests.Count != count)
      {
        Thread.Sleep(10);
      }
      for (int i = 0; i < PingedHandler.Requests.Count; i++)
      {
        Assert.Equal(i, PingedHandler.Requests[i]);
      }
    }
  }
}