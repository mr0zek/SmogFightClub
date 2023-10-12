using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureDocumentationGenerator
{
  public class ArchitectureGenerator
  {
    public ArchitectureGenerator() { }

    public static void GenerateComponentDiagrams(string assembliesPath, string outputPath)
    {
      Console.WriteLine("Analysing files from " + Path.GetFullPath(assembliesPath));
      Console.WriteLine($"Writing diagrams to : {Path.GetFullPath(outputPath)}");
      var extnsions = new string[] { ".dll" };
      var files = Directory.GetFiles(assembliesPath).Where(f => Path.GetFileName(f).StartsWith("SFC.")).Where(f => extnsions.Any(x => Path.GetExtension(f).ToLower() == x));
      var abstractions = AssemblyDefinition.ReadAssembly(files.First(f => f.Contains("Infrastructure.Interfaces")), new ReaderParameters()
      {
        AssemblyResolver = new AssemblyResolver(f => f.Contains("SFC"))
      }); 

      AnalysisResult result = new AnalysisResult();
      result.ModuleDefinitionAttribute = abstractions.MainModule.GetTypes().First(f => f.FullName.Contains("ModuleDefinitionAttribute"));
      result.CommandMarkerInterface = abstractions.MainModule.GetTypes().First(f => f.FullName.Contains("ICommand"));
      result.EventMarkerInterface = abstractions.MainModule.GetTypes().First(f => f.FullName.Contains("IEvent"));
      result.RequestMarkerInterface = abstractions.MainModule.GetTypes().First(f => f.FullName.Contains("IRequest"));
      result.EventBusMarkerInterface = abstractions.MainModule.GetTypes().First(f => f.FullName.Contains("IEventBus"));
      result.CommandBusMarkerInterface = abstractions.MainModule.GetTypes().First(f => f.FullName.Contains("ICommandBus"));
      result.QueryBusMarkerInterface = abstractions.MainModule.GetTypes().First(f => f.FullName.Contains("IQuery"));
      result.EventHandlerInterface = abstractions.MainModule.GetTypes().First(f => f.FullName.Contains("IEventHandler"));

      foreach (var file in files)
      {
        AnalyseAssembly(file, result);
      }

      result.RemoveInvalidReferences();

      StringBuilder sb = new StringBuilder();
      sb.AppendLine(@"
@startuml
!theme aws-orange

skinparam ranksep 100
skinparam nodesep 100

package SFC {");

      foreach (var g in result.Modules.GroupBy(f => f.Type))
      {
        sb.AppendLine($"frame {g.Key} {{");
        foreach (var module in g)
        {
          sb.AppendLine($"[{module.Name}]");
        }
        sb.AppendLine("}");
      }

      foreach (var module in result.Modules)
      {
        foreach (var reference in module.References)
        {
          sb.AppendLine($"[{module.Name}] --> [{reference.Name}] : [{string.Join(", ", reference.ReferenceTypes.Select(r => r.ToString()[0]))}]");
        }
      }

      sb.AppendLine(@"@enduml");

      Directory.CreateDirectory(outputPath);
      File.WriteAllText(Path.Combine(outputPath, "ComponentDiagram.puml"), sb.ToString());      
    }

    private static void AnalyseAssembly(string file, AnalysisResult result)
    {
      var assembly = AssemblyDefinition.ReadAssembly(file);
      var t = assembly.MainModule.GetTypes().FirstOrDefault(f => f.CustomAttributes.Any(f => f.AttributeType.Resolve().MetadataToken == result.ModuleDefinitionAttribute.MetadataToken));

      if (t != null)
      {
        var attr = t.CustomAttributes.First(f => f.AttributeType.Resolve().MetadataToken == result.ModuleDefinitionAttribute.MetadataToken);
        var type = attr.ConstructorArguments[0].Value.ToString();
        var module = new ArchModule(assembly.Name.Name, type);

        foreach (var f in assembly.MainModule.GetTypes())
        {
          foreach (var h in f.Interfaces.Where(c => c.InterfaceType.GetElementType().Resolve().MetadataToken == result.EventHandlerInterface.MetadataToken))
          {
            if (h.InterfaceType is GenericInstanceType)
            {
              string name = (h.InterfaceType as GenericInstanceType).GenericArguments[0].Scope.Name;
              module.AddReference(name, ReferenceType.Event);
            }
          }

          foreach (var m in f.Methods)
          {
            if (m.Body == null)
            {
              continue;
            }

            foreach (var i in m.Body.Instructions)
            {
              if (i.OpCode.Code == Mono.Cecil.Cil.Code.Callvirt)
              {
                if (i.Operand is GenericInstanceMethod)
                {
                  if(!(i.Operand as GenericInstanceMethod).DeclaringType.Scope.Name.Contains("SFC"))
                  {
                    continue;
                  }
                  if ((i.Operand as GenericInstanceMethod).DeclaringType.Resolve().MetadataToken == result.CommandBusMarkerInterface.MetadataToken)
                  {
                    string name = (i.Operand as GenericInstanceMethod).GenericArguments[0].Scope.Name;
                    module.AddReference(name, ReferenceType.Command);
                  }
                  if ((i.Operand as GenericInstanceMethod).DeclaringType.Resolve().MetadataToken == result.QueryBusMarkerInterface.MetadataToken)
                  {
                    string name = (i.Operand as GenericInstanceMethod).GenericArguments[0].Scope.Name;
                    module.AddReference(name, ReferenceType.Query);
                  }
                }
              }
            }
          }
        }

        result.AddModule(module);
      }
    }
  }
}
