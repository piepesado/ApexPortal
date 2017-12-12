using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Configuration;
using System.Drawing.Imaging;
using System.Text;

namespace AutomationFramework
{
    public class SeleniumUtils
    {
        #region variables

        private static string screenshotFolder = ConfigurationManager.AppSettings["screenshotFolder"];
        public static TimeSpan implicitTimeout = TimeSpan.FromSeconds(0);
        public static TimeSpan explicitTimeout = TimeSpan.FromSeconds(60);

        #endregion variables

        #region private methods (not used as test steps)

        /// <summary>
        /// Executes the given script (with no return value).
        /// </summary>
        /// <param name="driver">The current Test's WebDriver</param>
        /// <param name="script">The script to execute</param>
        private static void ExecuteJavaScript(IWebDriver driver, string script)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript(script);
        }

        /// <summary>
        /// Executes the given script (with no return value) using the given element as an arguement.
        /// </summary>
        /// <param name="driver">The current Test's WebDriver</param>
        /// <param name="script">The script to run</param>
        /// <param name="element">The element used as an argument in the script</param>
        private static void ExecuteJavaScript(IWebDriver driver, string script, IWebElement element)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript(script, element);
        }

        /// <summary>
        /// Takes a screenshot of the WebDriver's current page.
        /// </summary>
        /// <param name="driver">The current Test's WebDriver</param>
        private static string TakeScreenshot(IWebDriver driver)
        {
            // Get the current TimeStamp
            string timeStamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff");
            // Build a file name (NOTE: The folder must already exist and allow writes to it)
            string fileName = screenshotFolder + timeStamp + ".jpg";
            // Take a screenshot
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(fileName, ImageFormat.Png);
            // Return the filename of the screenshot
            return fileName;
        }

        /// <summary>
        ///  Waits (until explicitTimeout) for the given element to exist.
        /// </summary>
        /// <remarks>
        /// Do not mix implicit and explicit waits. Doing so can cause unpredictable wait times.
        /// For example setting an implicit wait of 10 seconds and an explicit wait of 15 seconds,
        /// could cause a timeout to occur after 20 seconds.
        /// Source - http://www.seleniumhq.org/docs/04_webdriver_advanced.jsp#explicit-and-implicit-waits
        /// </remarks>
        /// <param name="driver">The current Test's WebDriver</param>
        /// <param name="element">The element to click</param>
        private static void WaitForElementToExist(IWebDriver driver, IWebElement element)
        {
            // Ensure we only do an explicit wait if the implicit timeout is not set
            if (implicitTimeout.Seconds.Equals(0))
            {
                // Create a wait timer with the explicit timeout
                IWait<IWebDriver> wait = new WebDriverWait(driver, explicitTimeout);
                // Wait for the document's ready state to be 'complete'
                wait.Until(driver1 => ( element.GetAttribute("outerHTML") != null ));
            }
        }

        #endregion private methods (not used as test steps)

        #region public methods (used as test steps)

        /// <summary>
        /// Clicks the given element.
        /// </summary>
        /// <param name="driver">The current Test's WebDriver</param>
        /// <param name="element">The element to click</param>
        public static void ClickElement(IWebDriver driver, IWebElement element)
        {
            // Explicitly wait for the element to exist if implicit wait is not set
            WaitForElementToExist(driver, element);
            // Create a log message
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SeleniumUtils.ClickElement(driver, element)");
            try { sb.AppendLine("    [INFO] Element to click: " + element.GetAttribute("outerHTML")); } catch { /* do nothing */ } 
            try
            {
                // Click the element (via JavaScript)
                ExecuteJavaScript(driver, "arguments[0].click()", element);
                // Write result to log
                sb.AppendLine("    [SUCCESS] Element clicked.");
            }
            catch (Exception e)
            {
                // Take screenshot of current page
                string fileName = TakeScreenshot(driver);
                // Add result to log message
                sb.AppendLine("    [ERROR] Exception: " + e.Message);
                sb.AppendLine("    [DEBUG] Screenshot: " + fileName);
                // Fail current Test
                Assert.Fail(sb.ToString());
            }
            finally
            {
                // Write log message to console
                Console.WriteLine(sb.ToString());
            }
        }

        /// <summary>
        /// Enters the given text into the given element.
        /// </summary>
        /// <param name="driver">The current Test's WebDriver</param>
        /// <param name="element">The element to send keystrokes to</param>
        /// <param name="text">The text to input</param>
        public static void InputText(IWebDriver driver, IWebElement element, string text)
        {
            // Explicitly wait for the element to exist if implicit wait is not set
            WaitForElementToExist(driver, element);
            // Create a log message
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SeleniumUtils.InputText(driver, element, text)");
            try { sb.AppendLine("    [INFO] Element to click: " + element.GetAttribute("outerHTML")); } catch { /* do nothing */ }
            sb.AppendLine("    [INFO] Text to type: " + text);
            try
            {
                // Clear the input of any existing data
                element.Clear();
                // Send the keystrokes to the input
                element.SendKeys(text);
            }
            catch (Exception e)
            {
                // Take screenshot of current page
                string fileName = TakeScreenshot(driver);
                // Add result to log message
                sb.AppendLine("    [ERROR] Exception: " + e.Message);
                sb.AppendLine("    [DEBUG] Screenshot: " + fileName);
                // Fail current Test
                Assert.Fail(sb.ToString());
            }
            finally
            {
                // Write log message to console
                Console.WriteLine(sb.ToString());
            }
        }

        /// <summary>
        /// Navigates to the given URL.
        /// </summary>
        /// <remarks>
        /// Setting the OpenQA.Selenium.IWebDriver.Url property will load a new web page
        /// in the current browser window. This is done using an HTTP GET operation, and
        /// the method will block until the load is complete. This will follow redirects
        /// issued either by the server or as a meta-redirect from within the returned HTML.
        /// Should a meta-redirect "rest" for any duration of time, it is best to wait until
        /// this timeout is over, since should the underlying page change while your test
        /// is executing the results of future calls against this interface will be against
        /// the freshly loaded page.
        /// Source - https://github.com/SeleniumHQ/selenium/blob/master/dotnet/src/webdriver/IWebDriver.cs
        /// </remarks>
        /// <param name="driver">The current Test's WebDriver</param>
        /// <param name="element">The element to click</param>
        public static void NavigateToUrl(IWebDriver driver, string url)
        {
            // Create a log message
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SeleniumUtils.NavigateToUrl(driver, url)");
            sb.AppendLine("    [INFO] URL to Navigate: " + url);
            try
            {
                // Navigate to the given URL
                driver.Url = url;
                // Add result to log message
                sb.AppendLine("    [SUCCESS] Current URL: " + driver.Url);
            }
            catch (Exception e)
            {
                // Take screenshot of current page
                string fileName = TakeScreenshot(driver);
                // Add result to log message
                sb.AppendLine("    [ERROR] Exception: " + e.Message);
                sb.AppendLine("    [DEBUG] Screenshot: " + fileName);
                // Fail current Test
                Assert.Fail(sb.ToString());
            }
            finally
            {
                // Write log message to console
                Console.WriteLine(sb.ToString());
            }
        }
        
        #endregion public methods (used as test steps)
    }
}
