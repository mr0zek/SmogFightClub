﻿namespace SFC.Infrastructure.Interfaces
{
  public class ConnectionString 
  {
    private readonly string connectionString;

    public ConnectionString(string connectionString)
    {
      this.connectionString = connectionString;
    }

    public override string ToString() => connectionString;
  }
}