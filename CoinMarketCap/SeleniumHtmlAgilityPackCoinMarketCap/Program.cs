using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using OpenQA.Selenium;

namespace ConsoleApp5SeleniumHtmlAgilityPackCoinMarketCap
{
    class Program
    {
        static void Main(string[] args)
        {

            var driver = new ChromeDriver();

            //driver.Navigate().GoToUrl("http://www.google.com");

            //Thread.Sleep(5000); /html/body/div/div[1]/div[2]/div/div[1]/div/div[3]/button

            //driver.Navigate().GoToUrl("http://172.19.24.75:8080/");
            driver.Navigate().GoToUrl("https://coinmarketcap.com/all/views/all/");
            //Thread.Sleep(10000);

            //IWebElement textBoxUserName0 = driver.FindElement(By.Id("usernameinput"));
            //IWebElement textBoxUserName1 = driver.FindElement(By.CssSelector("#usernameinput"));


            //IWebElement textBoxUserName2 = driver.FindElement(By.XPath("/html/body/div/div[1]/div[2]/div/div[1]/div/div[3]/button"));
            driver.FindElement(By.XPath("/html/body/div/div[1]/div[2]/div/div[1]/div/div[3]/button")).Click();

            //textBoxUserName2.Click();
            IWebElement temp = driver.FindElementById("header-top");
            IWebElement element = driver.FindElement(By.Id("usernameinput"));

            driver.FindElementByXPath("//div/div/form/fieldset/div/input").SendKeys("msaddique");

           // ChromeWebElement test = (ChromeWebElement)driver.FindElementByName("os_username");


            driver.FindElementById("usernameinput").SendKeys("msaddique");
            driver.FindElementById("os_password").SendKeys("shahid");

            driver.Close();
            driver.Quit();
            
        }
    }
}
