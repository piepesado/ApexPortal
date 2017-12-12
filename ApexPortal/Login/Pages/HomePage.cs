using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using ApexPortal.Login.Pages;
using ApexPortal.Login.Feature;

namespace ApexPortal.Login.Pages
{
    public class HomePage : BasePage
    {
        //Locate element to ensure that is displayed and is presented on home screen
        [FindsBy(How = How.Id, Using = "userDropdown")]
        private IWebElement _userDropDown;

        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        public void EnsurePageIsLoaded()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Title.StartsWith("home"));
        }

        public HomePage UserMenuExists()
        {
            _userDropDown.Click();
            return new HomePage(_driver);
        }



    }
}
