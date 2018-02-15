﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using SimplePageFactory.Pages.Sections;

namespace SimplePageFactory.Pages
{
    public abstract class BasePage
    {
        protected IWebDriver driver;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public MainMenuSection MainMenu => new MainMenuSection(driver);
        public UpperMenuSection UpperMenu => new UpperMenuSection(driver);
    }
}