using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using SimplePageFactory.Pages.Sections;
using System;
using System.IO;

namespace SimplePageFactory.Pages
{
    public abstract class BasePage
    {
        protected abstract string Url { get; }
        protected IWebDriver driver;

        protected string projDir;
        protected string fullName;
        protected string logFile;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);

            projDir = Directory.GetParent(GetType().Assembly.Location).Parent.Parent.FullName;
            fullName = GetType().FullName + ".txt";
            logFile = Path.Combine(projDir, "Results\\JsReports", fullName);

            var errors = PageLogger.GetJsErrors(driver);
            PageLogger.SaveToFile(errors, fullName);
        }

        public MainMenuSection MainMenu => new MainMenuSection(driver);
        public UpperMenuSection UpperMenu => new UpperMenuSection(driver);
    }
}