using System;

namespace SFC.Sensors
{
  [AttributeUsage(AttributeTargets.All)]
  public class ModuleDefinitionAttribute : Attribute
  {
    public ModuleDefinitionAttribute(string moduleType)
    {
      ModuleType = moduleType;
    }

    public string ModuleType { get; }
  }
}