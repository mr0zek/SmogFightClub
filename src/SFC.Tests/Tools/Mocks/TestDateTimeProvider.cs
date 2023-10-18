﻿using SFC.Infrastructure.Interfaces.TimeDependency;
using System;

namespace SFC.Tests.Tools.Mocks
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