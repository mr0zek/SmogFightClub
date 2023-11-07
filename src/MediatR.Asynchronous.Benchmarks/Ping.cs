﻿namespace MediatR.Asynchronous.Benchmarks
{
  public class Ping : IRequest
  {
    public string Message { get; set; }
  }

  public class PingHandler : IRequestHandler<Ping>
  {
    public Task Handle(Ping request, CancellationToken cancellationToken) => Task.CompletedTask;
  }
}