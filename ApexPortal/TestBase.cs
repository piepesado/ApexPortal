using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Configuration;
using OpenQA.Selenium.Remote;

namespace AutomationFramework
{
    public class TestBase
    {
        #region variables

        // Declare a WebDriver for the tests
        protected IWebDriver driver;
        // Define the Base URL (from the App.config)
        protected string baseUrl = ConfigurationManager.AppSettings["baseUrl"];
        // Define the Browsers (from the App.config)
        protected static string browsersToRun = ConfigurationManager.AppSettings["browsers"];        

        #endregion variables

        /// <summary>
        /// Sets up the appropriate WebDriver
        /// </summary>
        /// <param name="browserName">The name of the browser the test requires</param>
        public void Setup(string browserName)
        {
            // Set WebDriver based on browerName parameter
            if (browserName.Equals("ie"))
            {
                // Use Remote IE
                try
                {
                    DesiredCapabilities capabilities = DesiredCapabilities.InternetExplorer();
                    InternetExplorerOptions options = new InternetExplorerOptions();
                    capabilities.SetCapability(options.EnsureCleanSession.ToString(), true);
                    driver = new RemoteWebDriver(capabilities);
                }
                // Use local IE
                catch
                {
                    InternetExplorerOptions options = new InternetExplorerOptions();
                    options.EnsureCleanSession = true;
                    driver = new InternetExplorerDriver(options);
                }
            }
            else if (browserName.Equals("chrome"))
            {
                // Use Remote Chrome
                try
                {
                    DesiredCapabilities capabilities = DesiredCapabilities.Chrome();
                    driver = new RemoteWebDriver(capabilities);
                }
                // Use Local Chrome
                catch
                {
                    // Create an options object to specify command line arguments for the Chrome web driver
                    ChromeOptions options = new ChromeOptions();
                    // Disable extensions
                    options.AddArgument(@"--disable-extensions");
                    // Create the driver with the defined options (above)
                    driver = new ChromeDriver(options);
                }
            }
            else if (browserName.Equals("chromeDRM"))
            {
                // Create an options object to specify command line arguments for the Chrome web driver
                ChromeOptions options = new ChromeOptions();
                // Use a real profile so that DRM loads
                string localAppData = Environment.GetEnvironmentVariable("LocalAppData");
                options.AddArgument(@"user-data-dir=" + localAppData + @"\Google\Chrome\User Data\");
                options.AddArgument(@"--profile-directory=Default");
                // Allow components to update (helps with DRM)
                options.AddExcludedArgument(@"disable-component-update");
                // Disable extensions
                options.AddArgument(@"--disable-extensions");
                // Use Remote Chrome
                try
                {
                    DesiredCapabilities capabilities = DesiredCapabilities.Chrome();
                    capabilities.SetCapability(ChromeOptions.Capability, options);
                    IWebDriver driver = new RemoteWebDriver(capabilities);
                }
                // Use Local Chrome
                catch
                {
                    // Create the driver with the defined options (above)
                    driver = new ChromeDriver(options);
                }
            }
            else if (browserName.Equals("edge"))
            {
                // Use Remote Edge
                try
                {
                    DesiredCapabilities capabilities = DesiredCapabilities.Edge();
                    capabilities.SetCapability(CapabilityType.BrowserName, "MicrosoftEdge");
                    capabilities.SetCapability(CapabilityType.Platform, "WINDOWS");
                    driver = new RemoteWebDriver(capabilities);
                }
                // Use Local Edge
                catch
                {
                    driver = new EdgeDriver();
                }
            }
            else if (browserName.Equals("firefox"))
            {
                // Use Remote Firefox
                try
                {
                    DesiredCapabilities capabilities = DesiredCapabilities.Firefox();
                    driver = new RemoteWebDriver(capabilities);
                }
                // Use Local Firefox
                catch
                {
                    try // Load Firefox From Default Location
                    {
                        driver = new FirefoxDriver();
                    }
                    catch // Load Firefox From Specific Location (for Windows 10)
                    {
                        FirefoxOptions options = new FirefoxOptions();
                        options.BrowserExecutableLocation = @"C:\Program Files\Mozilla Firefox\firefox.exe";
                        driver = new FirefoxDriver(options);
                    }
                }
            }
            else if (browserName.Equals("safari"))
            {
                // Use Remote Safari
                DesiredCapabilities capabilities = DesiredCapabilities.Safari();
                capabilities.SetCapability(CapabilityType.BrowserName, "safari");
                capabilities.SetCapability(CapabilityType.Platform, "MAC");
                driver = new RemoteWebDriver(capabilities);
            }
            else
            {
                throw new NotImplementedException(browserName + "not setup in TestBase.cs");
            }
            // Set window to full screen
            driver.Manage().Window.Maximize();
            // Clear all cookies (not respected by IE or Edge)
            if ((browserName != "ie") && (browserName != "edge") && (browserName != "safari"))
            {
                driver.Manage().Cookies.DeleteAllCookies();
            }
            // Set max global timeout
            //driver.Manage().Timeouts().ImplicitWait(SeleniumUtils.implicitTimeout);
        }

        /// <summary>
        /// Closes the driver and any associated browser windows at the end of a test
        /// </summary>
        [TearDown]
        public void Cleanup()
        {
            Console.WriteLine("Test complete.");
            driver.Quit();
        }

        /// <summary>
        /// Gets each browser from the App.config
        /// </summary>
        /// <returns>Each browser from the App.config</returns>
        public static IEnumerable<string> GetBrowsers()
        {
            // If no browsers were spcified, default to Chrome
            if (browsersToRun.Length == 0)
            {
                browsersToRun = "chrome";
            }
            // Split the list of browsers
            string[] browsers = browsersToRun.Split(',');
            // Return each browser
            foreach (string browser in browsers)
            {
                yield return browser;
            }
        }
    }
}
