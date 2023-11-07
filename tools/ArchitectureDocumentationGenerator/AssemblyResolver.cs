using Mono.Cecil;

namespace ArchitectureDocumentationGenerator
{
  internal class AssemblyResolver : BaseAssemblyResolver
  {
    private readonly Func<string, bool> _assemblyNamesChecker;
    
    private readonly IDictionary<string, AssemblyDefinition> _cache = new Dictionary<string, AssemblyDefinition>(StringComparer.Ordinal);   

    public AssemblyResolver(Func<string, bool> assemblyNamesChecker)
    {
      _assemblyNamesChecker = assemblyNamesChecker;
    }

    public override AssemblyDefinition? Resolve(AssemblyNameReference name, ReaderParameters parameters)
    {
      try
      {
        if (!_assemblyNamesChecker(name.Name))
        {
          return null;
        }

        var assemblyDefinition2 = base.Resolve(name);

        return assemblyDefinition2;
      }
      catch
      {        
        return null;
      }
    }

    protected override AssemblyDefinition? SearchDirectory(AssemblyNameReference name, IEnumerable<string> directories, ReaderParameters parameters)
    {
      string[] strArray1;
      if (!name.IsWindowsRuntime)
        strArray1 = new string[2] { ".exe", ".dll" };
      else
        strArray1 = new string[2] { ".winmd", ".dll" };
      string[] strArray2 = strArray1;
      AssemblyDefinition? assemblyDefinition = null;

      foreach (string directory in directories)
      {
        foreach (string str1 in strArray2)
        {
          string str2 = Path.Combine(directory, name.Name + str1);
          if (File.Exists(str2))
          {
            try
            {
              var assembly = GetAssembly(str2, parameters);
              if (assembly != null && assembly.Name.Version == name.Version)
              {
                return assembly;
              }

              if (assemblyDefinition == null)
              {
                assemblyDefinition = assembly;
              }
            }
            catch
            {
            }
          }
        }
      }

      return assemblyDefinition;
    }

    private AssemblyDefinition GetAssembly(string file, ReaderParameters parameters)
    {
      try
      {
        if (parameters.AssemblyResolver == null)
          parameters.AssemblyResolver = (IAssemblyResolver)this;
        return ModuleDefinition.ReadModule(file, parameters).Assembly;
      }
      catch (Exception)
      {
        throw;
      }
    }

    public override AssemblyDefinition? Resolve(AssemblyNameReference name)
    {
      try
      {

        if (!_assemblyNamesChecker(name.Name))
        {
          return null;
        }

        if (_cache.TryGetValue(name.FullName, out var assemblyDefinition1))
        {
          return assemblyDefinition1;
        }

        AssemblyDefinition? assemblyDefinition2 = base.Resolve(name, new ReaderParameters() { AssemblyResolver = this });

        if (assemblyDefinition2 != null)
        {
          _cache[name.FullName] = assemblyDefinition2;
        }

        return assemblyDefinition2;
      }
      catch (Exception)
      {
        return null;
      }
    }

    protected override void Dispose(bool disposing)
    {
      foreach (AssemblyDefinition assemblyDefinition in (IEnumerable<AssemblyDefinition>)this._cache.Values)
      {
        assemblyDefinition.Dispose();
      }

      _cache.Clear();
      base.Dispose(disposing);
    }
  }
}
