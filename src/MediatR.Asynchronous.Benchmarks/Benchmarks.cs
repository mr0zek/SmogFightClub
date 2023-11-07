using BenchmarkDotNet.Attributes;
using MediatR.Asynchronous.MsSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatR.Asynchronous.Benchmarks
{
  public class Benchmarks
  {
    private IAsyncMediator? _mediator;
    private IMessagesAsyncProcessor? _processor;
    private readonly Ping _request = new Ping { Message = "Hello World" };
    private readonly Pinged _notification = new Pinged();

    [GlobalSetup]
    public void GlobalSetup()
    {
      var confBuilder = new ConfigurationBuilder()
       .AddJsonFile("appsettings.json");
      var configuration = confBuilder.Build();
      string connectionString = configuration["ConnectionStrings:DefaultConnection"] ?? throw new NullReferenceException("connectionString is null");

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

    [GlobalCleanup]
    public void GlobalCleanup() 
    {
      _processor?.Stop();
      _processor?.WaitForShutdown();
    }

    [Benchmark]
    public async Task SendingRequests()
    {
      await (_mediator ?? throw new NullReferenceException()).Send(_request);
      _processor?.WaitForIdle();
    }

    [Benchmark]
    public async Task PublishingNotifications()
    {
      await (_mediator ?? throw new NullReferenceException()).Publish(_notification);
      _processor?.WaitForIdle();
    }
  }
}
