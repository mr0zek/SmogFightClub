using Mono.Cecil;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Infrastructure.Interfaces.Documentation;
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

      AnalysisResult result = new AnalysisResult
      {
        ModuleDefinitionAttribute = typeof(ModuleDefinitionAttribute).FullName,
        CommandMarkerInterface = typeof(ICommand).FullName,
        EventMarkerInterface = typeof(IEvent).FullName,
        RequestMarkerInterface = typeof(IRequest<>).FullName,
        EventBusMarkerInterface = typeof(IEventBus).FullName,
        CommandBusMarkerInterface = typeof(ICommandBus).FullName,
        QueryBusMarkerInterface = typeof(IQuery).FullName,
        EventHandlerInterface = typeof(IEventHandler<>).FullName,
        EntryPointForAttribute = typeof(EntryPointForAttribute).FullName,
        ExitPointToAttribute = typeof(ExitPointToAttribute).FullName
      };

      foreach (var file in files)
      {
        AnalyseAssembly(file, result);
      }
      result.AddModule(new ArchModule("Time", "Time"));

      result.RemoveInvalidReferences();

      StringBuilder sb = new StringBuilder();
      sb.AppendLine(@"
@startuml
!theme aws-orange

skinparam ranksep 100
skinparam nodesep 100");

      foreach (var m in result.Modules.Where(m => m.Type == "Human"))
      {
        sb.AppendLine($":{m.Name}:");      
      }
      foreach (var m in result.Modules.Where(m => m.Type == "ExternalSystem"))
      {
         sb.AppendLine($"[{m.Name}]");
      }
      
      sb.AppendLine(@"rectangle SFC #line.dashed {");

      foreach (var g in result.Modules.Where(m=>m.Type != "Human" && m.Type != "ExternalSystem").GroupBy(f => f.Type))
      {
        if (g.Key == "Time")
        {
          sb.AppendLine($":Time:");
        }        
        else
        {
          sb.AppendLine($"frame {g.Key} {{");
          foreach (var module in g)
          {
            sb.AppendLine($"[{module.Name}]");
          }
          sb.AppendLine("}");
        }
      }

      sb.AppendLine("}");

      foreach (var module in result.Modules)
      {
        foreach (var reference in module.References)
        {
          sb.AppendLine($"[{module.Name}] -d-> [{reference.Name}] : [{string.Join(", ", reference.ReferenceTypes.Select(r => r.ToString()[0]))}]");
        }
      }

      sb.AppendLine(@"

legend right
    |Communication type | Description|
    |[C]                | [C]ommand  |
    |[E]                | [E]vent    |
    |[Q]                | [Q]uery    |
endlegend

@enduml");

      Directory.CreateDirectory(outputPath);
      File.WriteAllText(Path.Combine(outputPath, "ComponentDiagram.puml"), sb.ToString());
    }

    private static void AnalyseAssembly(string file, AnalysisResult result)
    {
      var assembly = AssemblyDefinition.ReadAssembly(file);


      var t = assembly.MainModule.GetTypes().FirstOrDefault(f => f.CustomAttributes.Any(f =>
        f.AttributeType.FullName == result.ModuleDefinitionAttribute));

      if (t != null)
      {
        var attr = t.CustomAttributes.First(f => f.AttributeType.FullName == result.ModuleDefinitionAttribute);
        var type = attr.ConstructorArguments[0].Value.ToString();
        var module = new ArchModule(assembly.Name.Name, type);

        foreach (var f in assembly.MainModule.GetTypes())
        {
          foreach (var h in f.Interfaces.Where(c => c.InterfaceType.GetElementType().FullName == result.EventHandlerInterface))
          {
            if (h.InterfaceType is GenericInstanceType)
            {
              string name = ((GenericInstanceType)h.InterfaceType).GenericArguments[0].Scope.Name;
              if (((GenericInstanceType)h.InterfaceType).GenericArguments[0].Name == "TimeEvent")
              {
                name = "Time";
              }
              module.AddReference(name, ReferenceType.Event);
            }
          }
          
          foreach (var m in f.Methods)
          {
            var callers = m.CustomAttributes
              .Where(x => x.AttributeType.FullName == result.EntryPointForAttribute)
              .Select(f => f.ConstructorArguments);
            foreach (var item in callers)
            {
              var mod = result.Modules.FirstOrDefault(a => a.Name == item[0].Value.ToString());
              if (mod == null)
              {
                result.AddModule(mod = new ArchModule(item[0].Value.ToString(), ((CallerType)item[1].Value).ToString()));
              }
              mod.AddReference(module.Name, (ReferenceType)Enum.Parse(typeof(ReferenceType), ((CallType)item[2].Value).ToString()));
            }
          }

          foreach (var m in f.Methods)
          {
            if (m.Body == null)
            {
              continue;
            }
            
            var exitPoints = m.CustomAttributes.Where(x => x.AttributeType.FullName == result.ExitPointToAttribute);
            foreach (var exitPoint in exitPoints)
            {
              string externalSystem = exitPoint.ConstructorArguments[0].Value.ToString();
              ReferenceType calltype = (ReferenceType)Enum.Parse(typeof(ReferenceType), ((CallType)exitPoint.ConstructorArguments[1].Value).ToString());
              var mod = result.Modules.FirstOrDefault(a => a.Name == externalSystem);
              if (mod == null)
              {
                result.AddModule(mod = new ArchModule(externalSystem, "ExternalSystem"));
              }
              module.AddReference(mod.Name, calltype);

            }

            foreach (var i in m.Body.Instructions)
            {
              if (i.OpCode.Code == Mono.Cecil.Cil.Code.Callvirt)
              {
                if (i.Operand is GenericInstanceMethod)
                {
                  if (!(i.Operand as GenericInstanceMethod).DeclaringType.Scope.Name.Contains("SFC"))
                  {
                    continue;
                  }
                  if ((i.Operand as GenericInstanceMethod).DeclaringType.FullName == result.CommandBusMarkerInterface)
                  {
                    string name = (i.Operand as GenericInstanceMethod).GenericArguments[0].Scope.Name;
                    module.AddReference(name, ReferenceType.Command);
                  }                  
                  if ((i.Operand as GenericInstanceMethod).DeclaringType.FullName == result.QueryBusMarkerInterface)
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
