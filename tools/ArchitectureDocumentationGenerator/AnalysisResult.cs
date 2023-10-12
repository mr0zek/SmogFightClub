using Mono.Cecil;

namespace ArchitectureDocumentationGenerator
{
  internal class AnalysisResult
  {
    public List<ArchModule> Modules { get; } = new List<ArchModule>();

    public TypeReference ModuleDefinitionAttribute { get; internal set; }
    public TypeDefinition EventMarkerInterface { get; internal set; }
    public TypeDefinition CommandMarkerInterface { get; internal set; }
    public TypeDefinition RequestMarkerInterface { get; internal set; }
    public TypeDefinition EventBusMarkerInterface { get; internal set; }
    public TypeDefinition CommandBusMarkerInterface { get; internal set; }
    public TypeDefinition QueryBusMarkerInterface { get; internal set; }
    public TypeDefinition EventHandlerInterface { get; internal set; }

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
