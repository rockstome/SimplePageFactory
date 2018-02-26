using Microsoft.Win32;
using System;

namespace SimplePageFactory.Helpers
{
    /// <summary>
    /// <see cref="ExtentHelper"/> class provides methods for easier configuration. 
    /// </summary>
    public static class ExtentHelper
    {
        private static string HKLM_GetString(string path, string key)
        {
            try
            {
                RegistryKey rk = Registry.LocalMachine.OpenSubKey(path);
                if (rk == null) return "";
                return (string)rk.GetValue(key);
            }
            catch { return ""; }
        }

        public static string OSFriendlyName()
        {
            string ProductName = HKLM_GetString(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ProductName");
            string CSDVersion = HKLM_GetString(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CSDVersion");
            string SystemArchitecture = Environment.Is64BitOperatingSystem ? "64-bit" : "32-bit";
            if (ProductName != "")
            {
                return (ProductName.StartsWith("Microsoft") ? "" : "Microsoft ") + ProductName +
                            (CSDVersion != "" ? " " + CSDVersion : "") + " " + SystemArchitecture;
            }
            return "";
        }
    }
}
