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
        [Given(@"that I navigate to the APEX Portal Url")]
        public void GivenThatINavigateToTheAPEXPortalUrl()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"I enter jimmie\.carr@travelleaders\.com as the username")]
        public void GivenIEnterJimmie_CarrTravelleaders_ComAsTheUsername()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"I enter the password")]
        public void GivenIEnterThePassword()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"I enter (.*) as the CID")]
        public void GivenIEnterAsTheCID(int p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I click on Login button")]
        public void WhenIClickOnLoginButton()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"I should land on Apex hompage for Agency Agent role")]
        public void ThenIShouldLandOnApexHompageForAgencyAgentRole()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
