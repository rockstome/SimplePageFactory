using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimplePageFactory.Pages
{
    static class PageLogger
    {
        /// <summary>
        /// Gets all defined in this method JavaScript errors.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <returns>Formatted string with error</returns>
        public static string GetJsErrors(IWebDriver driver)
        {
            var errorStrings = new List<string>
            {
                "SyntaxError",
                "EvalError",
                "ReferenceError",
                "RangeError",
                "TypeError",
                "URIError",
                "InternalError"
            };

            var jsErrors = driver.Manage().Logs.GetLog(LogType.Browser).Where(x => errorStrings.Any(e => x.Message.Contains(e)));

            string record = "";

            if (jsErrors.Any())
            {
                record = "On page: " + driver.Url + Environment.NewLine +
                    "JavaScript error(s):" + Environment.NewLine +
                    jsErrors.Aggregate("", (s, entry) => s + entry.Message + Environment.NewLine);
            }

            return record;
        }

        /// <summary>
        /// Save to file if <paramref name="contents"/> is not empty.
        /// </summary>
        /// <param name="contents">Contents to be saved to file.</param>
        /// <param name="path">Absolute path to file</param>
        public static void SaveToFile(string contents, string path)
        {
            if(!String.IsNullOrEmpty(contents))
                System.IO.File.AppendAllText(path, contents);
        }
    }
}
