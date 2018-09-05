using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace JiraNUnit
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> result = new List<string>();

            var dllsFullName = GetDllsFullName();
            foreach (var dll in dllsFullName)
            {
                result.AddRange(GetTestsFullName(dll));
            }

            HashSet<string> r = new HashSet<string>(result);

            var final = BuildPlaylist(r);

            File.WriteAllText("final.playlist", final);
        }

        private static string BuildPlaylist(HashSet<string> testsSet)
        {
            StringBuilder s = new StringBuilder();
            s.AppendLine(@"<Playlist Version=""1.0"">");
            foreach (var test in testsSet)
            {
                s.AppendLine($@"    <Add Test=""{test}""/>");
            }

            s.AppendLine(" </Playlist>");
            return s.ToString();
        }

        private static List<string> GetDllsFullName()
        {
            var solution = Directory.GetCurrentDirectory() + "\\..\\..\\..";
            DirectoryInfo di = new DirectoryInfo(solution);
            var dlls = di.GetFiles("*.dll", SearchOption.AllDirectories);
            return dlls
                .Where(dll => dll.Name == "Libraries.dll" || dll.Name == "MainPage.Dashboard.dll")
                .Select(dll => dll.FullName)
                .ToList();
        }

        private static List<string> GetTestsFullName(string dllFullName) {

            List<string> list = new List<string>();

            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "C:\\Users\\tomas\\source\\repos\\MN.Retail.Tests.Functional\\packages\\NUnit.ConsoleRunner.3.8.0\\tools\\nunit3-console.exe",
                    //Arguments = @"--explore C:\Users\tomas\source\repos\MN.Retail.Tests.Functional\MainPage.Dashboard\bin\Debug\MainPage.Dashboard.dll",
                    Arguments = "--explore " + dllFullName,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            proc.Start();
            while (!proc.StandardOutput.EndOfStream)
            {
                string line = proc.StandardOutput.ReadLine();
                if (line.Contains("MNTT"))
                {
                    list.Add(line);
                }
            }
            return list;
        }
    }
}
