namespace ArchitectureDocumentationGenerator
{
  public class ArchModuleReference
  {
    public ArchModuleReference(string name, ReferenceType referenceType)
    {
      Name = name;
      ReferenceTypes = new HashSet<ReferenceType> { referenceType };
    }

    public string Name { get; }
    public HashSet<ReferenceType> ReferenceTypes { get; }
  }
}