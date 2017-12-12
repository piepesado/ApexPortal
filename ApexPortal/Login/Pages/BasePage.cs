using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;

namespace ApexPortal.Login.Feature
{
    /// <summary>
    /// The base class implementation of a page.
    /// </summary>
    public class BasePage
    {
        protected IWebDriver _driver;

        /// <summary>
        /// Base constructor
        /// </summary>
        /// <param name="driver"></param>
        public BasePage(IWebDriver driver)
        {
            _driver = driver ?? throw new ArgumentNullException(nameof(driver));
            PageFactory.InitElements(_driver, this);
        }

        /// <summary>
        /// Wait for an elemnet to be visible.
        /// </summary>
        /// <param name="element"></param>
        /// <exception cref="NoSuchElementException"></exception>"
        public void WaitForElementVisible(IWebElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            wait.Until(d => element.Displayed);
        }
    }
}
