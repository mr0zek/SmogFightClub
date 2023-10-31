namespace SFC.Infrastructure.Interfaces.Tracing
{
  public interface IRequestLifecycle
  {
    Task BeginRequest(string correlationId);
    Task EndRequest(string correlationId);

    Task AddModuleCall(ModuleCall trace);
  }
}