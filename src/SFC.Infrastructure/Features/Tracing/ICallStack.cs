﻿using System.Collections.Generic;

namespace SFC.Infrastructure.Features.Tracing
{
  public interface ICallStack
  {
    void StartCall(string moduleName, string callName, string type);
    void FinishCall(string callName);
  }
}