using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using NUnit.Framework;
using ApexPortal.Login.Pages;

namespace ApexPortal.Login.Steps
{
    [Binding]
    public class LoginSteps
    {
        private IWebDriver _driver;
        private LoginPage _loginPage;
        private HomePage _homePage;

        [Given(@"that I navigate to the APEX Portal Url")]
        public void GivenThatINavigateToTheAPEXPortalUrl()
        {
            _driver = WebDriverFactory.Create();
            _loginPage = LoginPage.NavigateTo(_driver);
        }
        
        [Given(@"I enter (.*) as the username")]
        public void GivenIEnterUsername(string userName)
        {
            _loginPage.Username = userName;
        }
        
        [Given(@"I enter (.*) as the password")]
        public void GivenIEnterThePassword(string passWord)
        {
            _loginPage.Password = passWord;
        }
        
        [Given(@"I enter (.*) as the CID")]
        public void GivenIEnterAsTheCID(string cid)
        {
            _loginPage.Cid = cid;
        }
        
        [When(@"I click on Login button")]
        public void WhenIClickOnLoginButton()
        {
            _loginPage.Login();
        }
        
        [Then(@"I should land on Apex hompage for Agency Agent role")]
        public void ThenIShouldLandOnApexHompageForAgencyAgentRole()
        {
            _homePage.EnsurePageIsLoaded();
            Assert.AreEqual("APEX Portal", _driver.Title);
        }

        [AfterScenario]
        public void CleanUp()
        {
            if(_driver != null)
            {
                _driver.Close();
                _driver.Dispose();
            }
        }
    }
}
