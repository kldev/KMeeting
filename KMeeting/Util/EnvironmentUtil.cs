using System;

namespace KMeeting.Util
{
    public static class EnvironmentUtil
    {
        public static string GetEnv(string name)
        {
            return Environment.GetEnvironmentVariable(name) ?? string.Empty;
        }
    }
}
