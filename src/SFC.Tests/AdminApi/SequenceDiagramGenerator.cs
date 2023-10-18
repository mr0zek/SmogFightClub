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
      sb.AppendLine($"center header <font size=25>{title}</font>");
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
  }
}