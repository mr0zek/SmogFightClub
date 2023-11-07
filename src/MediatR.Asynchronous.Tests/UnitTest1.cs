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
  public class UnitTest1
  {
    private IAsyncMediator _mediator;
    private IMessagesAsyncProcessor _processor;
    private readonly Ping _request = new Ping { Message = "Hello World" };
    private readonly Pinged _notification = new Pinged();

    public UnitTest1()
    {
      var confBuilder = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json");
      var configuration = confBuilder.Build();
      var connectionString = configuration["ConnectionStrings:DefaultConnection"];
      
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
      _processor = provider.GetRequiredService<IMessagesAsyncProcessor>();
      _processor.Start("testModuleName");
    }
    
    ~UnitTest1()
    {
      _processor.Stop();
      _processor.WaitForShutdown();
    }

    [Fact]
    public async Task SendingRequests()
    {
      await _mediator.Send(_request);
      _processor.WaitForIdle();
    }

    [Fact]
    public async Task PublishingNotifications()
    {
      await _mediator.Publish(_notification);
      _processor.WaitForIdle();
    }
  }
}