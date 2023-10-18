using SFC.Infrastructure.Features.Tracing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection.PortableExecutable;
using System.Text;

namespace SFC.Tests.AdminApi
{
  internal class SequenceDiagramGenerator
  {
    internal static void Generate(string filePath, string title, List<Trace> t)
    {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine(@"@startuml");
      sb.AppendLine($"header {title}");
      foreach (var v in t)
      {
        if (string.IsNullOrEmpty(v.CallingModuleName))
        {
          sb.AppendLine($"User -> {v.CalledModuleName} : <<{v.CallType}>> {v.CallName}");
        }
        else if(string.IsNullOrEmpty(v.CalledModuleName))
        {
          sb.AppendLine($"{v.CallingModuleName} -> User: <<{v.CallType}>> {v.CallName}");
        }
        else
        {
          sb.AppendLine($"{v.CallingModuleName} -> {v.CalledModuleName} : <<{v.CallType}>> {v.CallName}");
        }
      }

      sb.AppendLine(@"@enduml");
      File.WriteAllText(filePath, sb.ToString());      
    }

    internal static void GenerateDocumentationFile(string path)
    {
      var fileNames = Directory.GetFiles(path);
      StringBuilder sb = new StringBuilder();
      foreach (var file in fileNames.Where(f=>Path.GetExtension(f).ToLower() == ".puml"))
      {
        sb.AppendLine($"![alternative text](http://www.plantuml.com/plantuml/proxy?cache=no&src=https://raw.githubusercontent.com/mr0zek/SmogFightClub/master/docs/ArchitectureDocumentation/{Path.GetFileName(file)})");
      }
      File.WriteAllText(path+"\\sequenceDiagrams.md", sb.ToString());
    }
  }
}