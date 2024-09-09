using MediatR.Asynchronous.MsSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MediatR.Asynchronous.Tests
{
  /// <summary>
  /// Test scenarios
  /// 1. Performace test
  /// 2. Muliple instances of processor
  /// </summary>
  public class ConcurrencyTests
  {
    private List<IAsyncProcessor> _processor = new List<IAsyncProcessor>();
    IServiceProvider _serviceProvider;

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

      _serviceProvider = services.BuildServiceProvider();

      for (int i = 0; i < 10; i++)
      {
        _processor.Add(_serviceProvider.GetRequiredService<IAsyncProcessor>());
      }

      ResetDatabase.Reset(connectionString);
      DatabaseMigrator.Run(connectionString);

      _processor.ForEach(f=>f.Start());      
    }

    ~ConcurrencyTests()
    {
      _processor.ForEach(f => f.Stop());
      _processor.ForEach(f => f.WaitForShutdown());
    }

    [Fact]
    public async Task SendingRequests()
    {
      int count = 10;
      int processes = 32;
      Enumerable.Range(0, processes)
       .AsParallel()
       .ForAll(async index =>
       {
         using (var scope = _serviceProvider.CreateScope())
         {
           for (int i = 0; i < count; i++)
           {
             await scope.ServiceProvider.GetRequiredService<IAsyncMediator>().Send(new Ping(index * count + i, "testmessage"));
           }
          }
        });          
            
      while(PingHandler.Requests.Count != count * processes)
      {
        Thread.Sleep(10);
      }
      for(int i = 0;i< PingHandler.Requests.Count;i++)
      {
        Assert.True(PingHandler.Requests.Any(f => f == i), $"Message {i} not found");
      }
    }

    [Fact]
    public async Task PublishingNotifications()
    {
      int count = 10;
      int processes = 32;
      Enumerable.Range(0, processes)
       .AsParallel()
       .ForAll(async index =>
       {
         using (var scope = _serviceProvider.CreateScope())
         {
           for (int i = 0; i < count; i++)
           {
             await scope.ServiceProvider.GetRequiredService<IAsyncMediator>().Publish(new Pinged(index * count+i));
           }
         }
       });

      while (PingedHandler.Requests.Count != count*processes)
      {
        Thread.Sleep(10);
      }
      for (int i = 0; i < PingedHandler.Requests.Count; i++)
      {
        Assert.True(PingedHandler.Requests.Any(f => f == i), $"Message {i} not found");
      }
    }
  }
}