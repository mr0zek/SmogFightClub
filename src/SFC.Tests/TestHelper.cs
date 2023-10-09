using System;

namespace SFC.Tests
{
    internal class TestHelper
    {
        internal static string GenerateUrl()
        {
            return $"http://127.0.0.1:{Random.Shared.NextInt64(5000, 7000)}";
        }
    }
}