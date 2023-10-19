namespace SFC.Infrastructure.Interfaces.Tracing
{
  public interface IRequestLifecycle
  {
    void BeginRequest(string correlationId);
    void EndRequest(string correlationId);

    void AddModuleCall(ModuleCall trace);
  }
}