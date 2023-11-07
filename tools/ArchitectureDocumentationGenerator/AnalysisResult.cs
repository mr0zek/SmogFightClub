using Mono.Cecil;

namespace ArchitectureDocumentationGenerator
{
  internal class AnalysisResult
  {
    public List<ArchModule> Modules { get; } = new List<ArchModule>();

    public string? ModuleDefinitionAttribute { get; internal set; }
    public string? EventMarkerInterface { get; internal set; }
    public string? CommandMarkerInterface { get; internal set; }
    public string? RequestMarkerInterface { get; internal set; }
    public string? EventBusMarkerInterface { get; internal set; }
    public string? CommandBusMarkerInterface { get; internal set; }
    public string? QueryBusMarkerInterface { get; internal set; }
    public string? EventHandlerInterface { get; internal set; }
    public string? EntryPointForAttribute { get; internal set; }
    public string? ExitPointToAttribute { get; internal set; }

    public void AddModule(ArchModule module)
    {
      if (Modules.Any(f => f.Name == module.Name))
      {
        return;
      }
      Modules.Add(module);
    }

    public void RemoveInvalidReferences()
    {
      foreach (var m in Modules)
      {
        m.RemoveInvalidReferences(Modules.Select(f => f.Name));
      }
    }
  }
}
