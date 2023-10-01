using System;

namespace SFC.Tests
{
    internal class TestHelper
    {
        internal static string GenerateUrl()
        {
            return $"http://localhost:{Random.Shared.NextInt64(5000, 7000)}";
        }
    }
}