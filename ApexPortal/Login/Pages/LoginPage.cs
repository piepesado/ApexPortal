using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using ApexPortal.Login.Feature;

namespace ApexPortal.Login.Pages
{
    public class LoginPage : BasePage
    {
        private const string APEX_DEV_URL = "http://dev-coltui.travelnxt.com/login";

        //Objects
        [FindsBy(How = How.Id, Using = "username")]
        private IWebElement _userName;

        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement _passWord;

        [FindsBy(How = How.Id, Using = "orgid")]
        private IWebElement _cid;

        [FindsBy(How = How.Id, Using = "loginapi")]
        private IWebElement _loginButton;

        //Object Properties

        //public IWebElement UserName { get => _userName; set => _userName = value; }
        //public IWebElement PassWord { get => _passWord; set => _passWord = value; }
        //public IWebElement LoginButton { get => _loginButton; set => _loginButton = value; }
        //public IWebElement Cid { get => _cid; set => _cid = value; }

        public string Username
        {
            get
            {
                return _userName.Text;
            }
            set
            {
                _userName.SendKeys(value);
            }
        }

        public string Password
        {
            get
            {
                return _passWord.Text;
            }
            set
            {
                _passWord.SendKeys(value);
            }
        }

        public string Cid
        {
            get
            {
                return _cid.Text;
            }
            set
            {
                _cid.SendKeys(value);
            }
        }

        //Actions
        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        public static LoginPage NavigateTo(IWebDriver driver)
        {
            if(driver == null)
            {
                throw new ArgumentNullException(nameof(driver));
            }

            driver.Navigate().GoToUrl(APEX_DEV_URL);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Title.Equals("APEX Portal::Login"));
            return new LoginPage(driver);
        }

        //create Home page
        public HomePage Login()
        {
            _loginButton.Click();
            return new HomePage(_driver);
        }
    }
}
