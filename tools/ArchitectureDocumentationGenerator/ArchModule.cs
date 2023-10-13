namespace ArchitectureDocumentationGenerator
{
  internal class ArchModule
  {
    public ArchModule(string name, string type) 
    {
      Name = name;
      Type = type;
    }

    public string Name { get; internal set; }
    public List<ArchModuleReference> References { get; internal set; } = new List<ArchModuleReference>();
    public string Type { get; }

    internal void AddReference(string name, ReferenceType referenceType)
    {
      var r = References.FirstOrDefault(f => f.Name == name);
      if (r == null)
      {
        References.Add(new ArchModuleReference(name, referenceType));
      }
      else
      {
        if (!r.ReferenceTypes.Contains(referenceType))
        {
          r.ReferenceTypes.Add(referenceType);
        }
      }
    }

    internal void RemoveInvalidReferences(IEnumerable<string> acceptable)
    {
      References = References.Where(f => acceptable.Any(x => x == f.Name)).ToList();
    }
  }
}
