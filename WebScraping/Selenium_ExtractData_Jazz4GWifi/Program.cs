using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace Selenium_ExtractData_Jazz4GWifi
{
    internal class Program
    {
        static IWebDriver driver = new ChromeDriver();
        static string fileName = "SMSJazz4G.csv";
        static bool messageSelected = false;
        static void Main(string[] args)
        {
            //XPATHs for data extraction
            string xPathNextPage = "//*[@id=\"nextPage\"]";
            string xPathSMSButton = "//*[@id=\"sms\"]";
            string xPathTableRows = "//*[@id=\"sms_table\"]/tbody/tr/td";
            string xPathTotalPages = "//*[@id=\"curSmsPage\"]";

            // Launch the Jazz 4G Wifi Portal
            driver.Url = ("http://jazz.wifi/index.html");

            Thread.Sleep(1000);

            driver.FindElement(By.XPath(xPathSMSButton)).Click();
            Thread.Sleep(1000);

            Common.LoginJazz4GPortal(driver);

            File.Delete(fileName);
            bool nextFound = true;
            var listOfAllItems = new List<string>();
            var listOfItemsCSV = new List<string>();
            var listOfTableItems = new List<string>();


            int loopEnd = 0;
            while (nextFound)
            {
                //Search whole table
                var table = driver.FindElements(By.XPath(xPathTableRows));

                //HTML table to string
                HTMLTableToStringList(table, out listOfTableItems);
                //listOfTableItems.Clear();
                foreach (var item in listOfTableItems)
                {
                    listOfAllItems.Add(item);
                }
                SelectMessageToBeDeleted("message.txt", ref listOfTableItems);

                if (messageSelected)
                {
                    DeleteSelectedMessage();
                    messageSelected = false;
                }
                listOfTableItems.Clear();
                try
                {
                    //Get the total pages number
                    var returnnn = driver.FindElement(By.XPath(xPathTotalPages)).Text;

                    //Splitted current and total pages to get current state
                    var result = returnnn.Split('/');
                    if (result[0] == result[1])
                        nextFound = false;

                    //find next page link and click on it
                    driver.FindElement(By.XPath(xPathNextPage)).Click();
                }
                catch (Exception)
                {
                    nextFound = false;
                }
                //Delay to refresh the page
                Thread.Sleep(1000);
            }
            //WriteCSVFileUsingStringList(fileName, listOfItems);
            string CSVRow = string.Empty;
            for (int i = 1; i < listOfAllItems.Count; i++)
            {
                if ((i % 4) == 0)
                {
                    listOfItemsCSV.Add(CSVRow);
                    CSVRow = string.Empty;
                }
                else
                {
                    CSVRow += listOfAllItems[i] + ",";
                }
            }
            WriteCSVFileUsingStringList("csv" + fileName, listOfItemsCSV);
            driver.Close();
        }

        private static void SelectMessageToBeDeleted(string fileName, ref List<string> listOfItems)
        {
            var allLines = File.ReadAllLines(fileName);

            int lineNumberFinder = 0;
            foreach (string item in listOfItems)
            {
                lineNumberFinder++;
                foreach (var line in allLines)
                {
                    if (item.Contains(line))
                    {
                        int lineNumer = GetLineNumber(lineNumberFinder);
                        selectmessageCheckBox(lineNumer);
                        messageSelected = true;
                    }
                }
            }
        }

        private static int GetLineNumber(int lineNumberFinder)
        {
            int lineNumber = (lineNumberFinder + 1) / 4;
            //if (lineNumber>20)
            //{
            //    lineNumber = lineNumber % 20;
            //}
            return lineNumber;
        }

        private static void selectmessageCheckBox(int lineNumber)
        {
            lineNumber += 1;
            string xPathCheckBoxofMessage = "//*[@id=\"sms_table\"]/tbody/tr[" + lineNumber + "]/td[1]/input";
            Thread.Sleep(1000);
            driver.FindElement(By.XPath(xPathCheckBoxofMessage)).Click();
            Thread.Sleep(1000);
        }

        private static void DeleteSelectedMessage()
        {
            //*[@id="pop_confirm"]
            string xPathDeleteConfirm = "//*[@id=\"pop_confirm\"]";
            string xPathDeleteButton = "//*[@id=\"del_msg_btn\"]";
            driver.FindElement(By.XPath(xPathDeleteButton)).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath(xPathDeleteConfirm)).Click();
            Thread.Sleep(5000);
        }

        private static void HTMLTableToStringList(ReadOnlyCollection<IWebElement> table, out List<string> listOfItems)
        {
            var listOfItemsInternal = new List<string>();
            foreach (var item in table)
            {
                string itemString = string.Empty;
                itemString = item.Text;
                itemString = RemoveNewLinesAndMultipleSpaces(itemString);
                listOfItemsInternal.Add(itemString);
            }
            listOfItems = listOfItemsInternal;
        }

        private static void WriteCSVFileUsingStringList(string fileName, List<string> listOfItems)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            File.AppendAllLines(fileName, listOfItems, System.Text.Encoding.UTF8);
        }

        private static string RemoveNewLinesAndMultipleSpaces(string itemString)
        {
            itemString = itemString.Replace(Environment.NewLine, " ");
            var lines = itemString.Split(' ');
            string finalString = string.Empty;

            foreach (var line in lines)
            {
                string processedLine = line.Trim();
                processedLine = processedLine.Replace(',', '-');
                if (!string.IsNullOrWhiteSpace(processedLine))
                {
                    finalString += processedLine + " ";
                }
            }
            return finalString;
        }
    }
}