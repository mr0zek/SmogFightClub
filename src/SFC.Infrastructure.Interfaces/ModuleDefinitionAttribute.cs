﻿using System;

namespace SFC.Infrastructure.Interfaces
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