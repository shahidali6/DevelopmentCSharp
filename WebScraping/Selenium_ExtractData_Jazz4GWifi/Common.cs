using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Selenium_ExtractData_Jazz4GWifi
{
    internal class Common
    {
        public static void LoginJazz4GPortal(IWebDriver driver)
        {
            //User credentials
            string userName = "admin";
            string password = "shahidali6@";

            //XPATHs for data extraction
            string xPathUserName = "//*[@id=\"username\"]";
            string xPathPassword = "//*[@id=\"password\"]";
            string xPathLoginButton = "//*[@id=\"pop_login\"]";
            try
            {
                driver.FindElement(By.XPath(xPathUserName)).SendKeys(userName);
                driver.FindElement(By.XPath(xPathPassword)).SendKeys(password);
                driver.FindElement(By.XPath(xPathLoginButton)).Click();
                Thread.Sleep(1000);
            }
            catch (Exception)
            {

            }
        }
    }
}
