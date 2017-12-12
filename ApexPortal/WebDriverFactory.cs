using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirDemo.Specs
{
    static class WebDriverFactory
    {
        /// <summary>
        /// Create an instance of a web driver.
        /// </summary>
        /// <returns></returns>
        internal static IWebDriver Create()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("window-size=720x1280");
            // options.AddArgument("headless"); // This would be added if running on a server through Jenkins.

            IWebDriver driver = new ChromeDriver(options);
            return driver;
        }
    }
}
