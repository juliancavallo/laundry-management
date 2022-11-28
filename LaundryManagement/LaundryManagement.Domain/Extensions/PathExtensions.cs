using System;
using System.IO;

namespace LaundryManagement.Domain.Extensions
{
    public static class PathExtensions
    {
        public static string GetRelativePath(this string resourcePath)
        {
            var appPath = AppDomain.CurrentDomain.BaseDirectory;
            return Path.Combine(appPath, resourcePath);
        }
    }
}
