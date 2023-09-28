using SFC.Infrastructure.Interfaces;
using System;

namespace SFC.Tests.Mocks
{
  internal class TestDateTimeProvider : IDateTimeProvider
  {
    public static DateTime DateToReturn = DateTime.Now;
    public DateTime Now()
    {
      return DateToReturn;
    }
  }
}