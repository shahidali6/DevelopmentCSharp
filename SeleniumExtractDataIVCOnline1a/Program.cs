using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ConsoleApp4SeleniumExtractDataIVCOnline
{
    class Program
    {
        private static string mySQLConnectionString { get; } = "datasource=localhost;port=3306;username=root;password=;";
        static IWebDriver driver = new ChromeDriver();
        static void Main(string[] args)
        {
            // Launch the ToolsQA WebSite
            //driver.Url = ("https://toolsqa.com/Automation-practice-form/");
            driver.Url = (@"J:\Dropbox\Project\Selenium\20214720_2147_Page_5880.html");

            CompanyData data = new CompanyData();

            //string xpath_name = "/html/body/form/div[3]/div[1]/main/div/div/div/div/div[1]/div/div/div/table/tbody/tr[1]/td/table/tbody/tr[1]/td/div[2]/div[1]/table/tbody/tr[1]/td[2]/h1/span";
            string xpath_name = "//*[@id=\"dnn_ctr465_View_CompanyCard_HeaderCard1_lFullName\"]";
            //string xpath_website = "/html/body/form/div[3]/div[1]/main/div/div/div/div/div[1]/div/div/div/table/tbody/tr[1]/td/table/tbody/tr[1]/td/div[2]/div[1]/table/tbody/tr[2]/td/table/tbody/tr[1]/td/span[1]/a";
            string xpath_website = "//*[@id=\"dnn_ctr465_View_CompanyCard_HeaderCard1_hlWebSite\"]/a";
            //string xpath_type = "/html/body/form/div[3]/div[1]/main/div/div/div/div/div[1]/div/div/div/table/tbody/tr[1]/td/table/tbody/tr[1]/td/div[2]/div[1]/table/tbody/tr[2]/td/table/tbody/tr[2]/td/span";
            string xpath_type = "//*[@id=\"dnn_ctr465_View_CompanyCard_HeaderCard1_lCompSubType\"]";
            //string xpath_sector = "/html/body/form/div[3]/div[1]/main/div/div/div/div/div[1]/div/div/div/table/tbody/tr[1]/td/table/tbody/tr[2]/td[1]/div/div[1]/table[1]/tbody/tr[1]/td/table/tbody/tr[2]/td[2]/table/tbody/tr[1]/td[2]/span";
            string xpath_sector = "//*[@id=\"dnn_ctr465_View_CompanyCard_GeneralData1_lSector\"]";
            //string xpath_sub_sector = "/html/body/form/div[3]/div[1]/main/div/div/div/div/div[1]/div/div/div/table/tbody/tr[1]/td/table/tbody/tr[2]/td[1]/div/div[1]/table[1]/tbody/tr[1]/td/table/tbody/tr[2]/td[2]/table/tbody/tr[2]/td[2]/span";
            string xpath_sub_sector = "//*[@id=\"dnn_ctr465_View_CompanyCard_GeneralData1_lSubSector\"]";
            //string xpath_company_stage = "/html/body/form/div[3]/div[1]/main/div/div/div/div/div[1]/div/div/div/table/tbody/tr[1]/td/table/tbody/tr[2]/td[1]/div/div[1]/table[1]/tbody/tr[1]/td/table/tbody/tr[2]/td[2]/table/tbody/tr[3]/td[2]/span";
            string xpath_company_stage = "//*[@id=\"dnn_ctr465_View_CompanyCard_GeneralData1_lStage\"]";
            //string xpath_established = "/html/body/form/div[3]/div[1]/main/div/div/div/div/div[1]/div/div/div/table/tbody/tr[1]/td/table/tbody/tr[2]/td[1]/div/div[1]/table[1]/tbody/tr[1]/td/table/tbody/tr[2]/td[2]/table/tbody/tr[4]/td[2]/span";
            string xpath_established = "//*[@id=\"dnn_ctr465_View_CompanyCard_GeneralData1_lEstYear\"]";
            //string xpath_number_employees = "/html/body/form/div[3]/div[1]/main/div/div/div/div/div[1]/div/div/div/table/tbody/tr[1]/td/table/tbody/tr[2]/td[1]/div/div[1]/table[1]/tbody/tr[1]/td/table/tbody/tr[2]/td[2]/table/tbody/tr[5]/td[2]/span";
            string xpath_number_employees = "//*[@id=\"dnn_ctr465_View_CompanyCard_GeneralData1_lNoemp\"]";
            //string xpath_reg_number = "/html/body/form/div[3]/div[1]/main/div/div/div/div/div[1]/div/div/div/table/tbody/tr[1]/td/table/tbody/tr[2]/td[1]/div/div[1]/table[1]/tbody/tr[1]/td/table/tbody/tr[2]/td[2]/table/tbody/tr[6]/td[2]/span";
            string xpath_reg_number = "//*[@id=\"dnn_ctr465_View_CompanyCard_GeneralData1_Lblreg\"]";
            //string xpath_description = "/html/body/form/div[3]/div[1]/main/div/div/div/div/div[1]/div/div/div/table/tbody/tr[1]/td/table/tbody/tr[2]/td[1]/div/div[1]/table[2]/tbody/tr[1]/td/table/tbody/tr[2]/td[2]/span";
            string xpath_description = "//*[@id=\"dnn_ctr465_View_CompanyCard_GeneralData1_lDisc\"]";
            //string xpath_technology = "/html/body/form/div[3]/div[1]/main/div/div/div/div/div[1]/div/div/div/table/tbody/tr[1]/td/table/tbody/tr[2]/td[1]/div/div[1]/table[3]/tbody/tr[1]/td/table/tbody/tr[2]/td[2]/span";
            string xpath_technology = "//*[@id=\"dnn_ctr465_View_CompanyCard_GeneralData1_lTech\"]";
            //string xpath_target_markets = "/html/body/form/div[3]/div[1]/main/div/div/div/div/div[1]/div/div/div/table/tbody/tr[1]/td/table/tbody/tr[2]/td[1]/div/div[1]/table[4]/tbody/tr[1]/td/table/tbody/tr[2]/td[2]/span";
            string xpath_target_markets = "//*[@id=\"dnn_ctr465_View_CompanyCard_GeneralData1_lTarCos\"]";
            string xpath_tags = "/html/body/form/div[3]/div[1]/main/div/div/div/div/div[1]/div/div/div/table/tbody/tr[1]/td/table/tbody/tr[2]/td[2]/table/tbody/tr[1]/td/table/tbody/tr[2]/td[2]/span/a[1]/div";
            //string xpath_address = "/html/body/form/div[3]/div[1]/main/div/div/div/div/div[1]/div/div/div/table/tbody/tr[1]/td/table/tbody/tr[2]/td[2]/div[4]/table/tbody/tr[1]/td/table/tbody/tr[2]/td[2]/div/table/tbody/tr/td/table/tbody/tr[2]/td/table/tbody/tr[1]/td[2]/span";
            string xpath_address = "//*[@id=\"dnn_ctr465_View_CompanyCard_ContactInfo1_RptContactInfo_lAdd_0\"]";
            //string xpath_city = "/html/body/form/div[3]/div[1]/main/div/div/div/div/div[1]/div/div/div/table/tbody/tr[1]/td/table/tbody/tr[2]/td[2]/div[4]/table/tbody/tr[1]/td/table/tbody/tr[2]/td[2]/div/table/tbody/tr/td/table/tbody/tr[2]/td/table/tbody/tr[2]/td[2]/span";
            string xpath_city = "//*[@id=\"dnn_ctr465_View_CompanyCard_ContactInfo1_RptContactInfo_lCity_0\"]";
            //string xpath_country = "/html/body/form/div[3]/div[1]/main/div/div/div/div/div[1]/div/div/div/table/tbody/tr[1]/td/table/tbody/tr[2]/td[2]/div[4]/table/tbody/tr[1]/td/table/tbody/tr[2]/td[2]/div/table/tbody/tr/td/table/tbody/tr[2]/td/table/tbody/tr[3]/td[2]/span";
            string xpath_country = "//*[@id=\"dnn_ctr465_View_CompanyCard_ContactInfo1_RptContactInfo_lCountry_0\"]";
            //string xpath_zip = "/html/body/form/div[3]/div[1]/main/div/div/div/div/div[1]/div/div/div/table/tbody/tr[1]/td/table/tbody/tr[2]/td[2]/div[4]/table/tbody/tr[1]/td/table/tbody/tr[2]/td[2]/div/table/tbody/tr/td/table/tbody/tr[2]/td/table/tbody/tr[4]/td[2]/span";
            string xpath_zip = "//*[@id=\"dnn_ctr465_View_CompanyCard_ContactInfo1_RptContactInfo_lZip_0\"]";
            //string xpath_contact_person = "/html/body/form/div[3]/div[1]/main/div/div/div/div/div[1]/div/div/div/table/tbody/tr[1]/td/table/tbody/tr[2]/td[2]/div[4]/table/tbody/tr[1]/td/table/tbody/tr[2]/td[2]/div/table/tbody/tr/td/table/tbody/tr[2]/td/table/tbody/tr[6]/td[2]/span";
            string xpath_contact_person = "//*[@id=\"dnn_ctr465_View_CompanyCard_ContactInfo1_RptContactInfo_link_0\"]";
            //string xpath_contact_email = "/html/body/form/div[3]/div[1]/main/div/div/div/div/div[1]/div/div/div/table/tbody/tr[1]/td/table/tbody/tr[2]/td[2]/div[4]/table/tbody/tr[1]/td/table/tbody/tr[2]/td[2]/div/table/tbody/tr/td/table/tbody/tr[2]/td/table/tbody/tr[7]/td[2]/a";
            string xpath_contact_email = "//*[@id=\"dnn_ctr465_View_CompanyCard_ContactInfo1_RptContactInfo_htContactEmail_0\"]";
            //string xpath_company_email = "/html/body/form/div[3]/div[1]/main/div/div/div/div/div[1]/div/div/div/table/tbody/tr[1]/td/table/tbody/tr[2]/td[2]/div[4]/table/tbody/tr[1]/td/table/tbody/tr[2]/td[2]/div/table/tbody/tr/td/table/tbody/tr[2]/td/table/tbody/tr[8]/td[2]/a";
            string xpath_company_email = "//*[@id=\"dnn_ctr465_View_CompanyCard_ContactInfo1_RptContactInfo_hlEmail_0\"]";
            //string xpath_telephone = "/html/body/form/div[3]/div[1]/main/div/div/div/div/div[1]/div/div/div/table/tbody/tr[1]/td/table/tbody/tr[2]/td[2]/div[4]/table/tbody/tr[1]/td/table/tbody/tr[2]/td[2]/div/table/tbody/tr/td/table/tbody/tr[2]/td/table/tbody/tr[9]/td[2]/span";
            string xpath_telephone = "//*[@id=\"dnn_ctr465_View_CompanyCard_ContactInfo1_RptContactInfo_lTel_0\"]";

            //*[@id="dnn_ctr465_View_CompanyCard_ContactInfo1_RptContactInfo_lTel_0"]

            IWebElement name = driver.FindElement(By.XPath(RemoveNumberFromXPathString(xpath_name)));
            data.name = ((RemoteWebElement)name).Text;

            IWebElement website = driver.FindElement(By.XPath(RemoveNumberFromXPathString(xpath_website)));
            data.website = ((RemoteWebElement)website).Text;

            IWebElement type = driver.FindElement(By.XPath(RemoveNumberFromXPathString(xpath_type)));
            data.company_type = ((RemoteWebElement)type).Text;

            IWebElement sector = driver.FindElement(By.XPath(RemoveNumberFromXPathString(xpath_sector)));
            data.sector = ((RemoteWebElement)sector).Text;

            IWebElement sub_sector = driver.FindElement(By.XPath(RemoveNumberFromXPathString(xpath_sub_sector)));
            data.sub_sector = ((RemoteWebElement)sub_sector).Text;

            IWebElement company_stage = driver.FindElement(By.XPath(RemoveNumberFromXPathString(xpath_company_stage)));
            data.company_stage = ((RemoteWebElement)company_stage).Text;

            IWebElement established = driver.FindElement(By.XPath(RemoveNumberFromXPathString(xpath_established)));
            data.established = int.Parse(((RemoteWebElement)established).Text);

            IWebElement number_employees = driver.FindElement(By.XPath(RemoveNumberFromXPathString(xpath_number_employees)));
            data.number_employees = int.Parse(((RemoteWebElement)number_employees).Text);

            IWebElement reg_number = driver.FindElement(By.XPath(RemoveNumberFromXPathString(xpath_reg_number)));
            data.reg_number = int.Parse(((RemoteWebElement)reg_number).Text);

            IWebElement description = driver.FindElement(By.XPath(RemoveNumberFromXPathString(xpath_description)));
            data.description = ((RemoteWebElement)description).Text;

            IWebElement technology = driver.FindElement(By.XPath(RemoveNumberFromXPathString(xpath_technology)));
            data.technology = ((RemoteWebElement)technology).Text;

            IWebElement target_markets = driver.FindElement(By.XPath(RemoveNumberFromXPathString(xpath_target_markets)));
            data.target_markets = ((RemoteWebElement)target_markets).Text;

            IWebElement tags = driver.FindElement(By.XPath(RemoveNumberFromXPathString(xpath_tags)));
            data.tags = ((RemoteWebElement)tags).Text;

            IWebElement address = driver.FindElement(By.XPath(RemoveNumberFromXPathString(xpath_address)));
            data.address = ((RemoteWebElement)address).Text;

            IWebElement city = driver.FindElement(By.XPath(RemoveNumberFromXPathString(xpath_city)));
            data.city = ((RemoteWebElement)city).Text;

            IWebElement country = driver.FindElement(By.XPath(RemoveNumberFromXPathString(xpath_country)));
            data.country = ((RemoteWebElement)country).Text;

            IWebElement zip = driver.FindElement(By.XPath(RemoveNumberFromXPathString(xpath_zip)));
            data.zip = int.Parse(((RemoteWebElement)zip).Text);

            IWebElement contact_person = driver.FindElement(By.XPath(RemoveNumberFromXPathString(xpath_contact_person)));
            data.contact_person = ((RemoteWebElement)contact_person).Text;

            IWebElement contact_email = driver.FindElement(By.XPath(RemoveNumberFromXPathString(xpath_contact_email)));
            data.contact_email = ((RemoteWebElement)contact_email).Text;

            IWebElement company_email = driver.FindElement(By.XPath(RemoveNumberFromXPathString(xpath_company_email)));
            data.company_email = ((RemoteWebElement)company_email).Text;

            IWebElement telephone = driver.FindElement(By.XPath(xpath_telephone));
            data.telephone = ((RemoteWebElement)telephone).Text;

            Console.WriteLine(AllFields.address.ToString() + " [=========] " + data.address);
            Console.WriteLine(AllFields.city.ToString() + " [=========] " + data.city);
            Console.WriteLine(AllFields.company_email.ToString() + " [=========] " + data.company_email);
            Console.WriteLine(AllFields.company_stage.ToString() + " [=========] " + data.company_stage);
            Console.WriteLine(AllFields.contact_email.ToString() + " [=========] " + data.contact_email);
            Console.WriteLine(AllFields.contact_person.ToString() + " [=========] " + data.contact_person);
            Console.WriteLine(AllFields.country.ToString() + " [=========] " + data.country);
            Console.WriteLine(AllFields.description.ToString() + " [=========] " + data.description);
            Console.WriteLine(AllFields.established.ToString() + " [=========] " + data.established);
            Console.WriteLine(AllFields.name.ToString() + " [=========] " + data.name);
            Console.WriteLine(AllFields.number_employees.ToString() + " [=========] " + data.number_employees);
            Console.WriteLine(AllFields.reg_number.ToString() + " [=========] " + data.reg_number);
            Console.WriteLine(AllFields.sector.ToString() + " [=========] " + data.sector);
            Console.WriteLine(AllFields.sub_sector.ToString() + " [=========] " + data.sub_sector);
            Console.WriteLine(AllFields.tags.ToString() + " [=========] " + data.tags);
            Console.WriteLine(AllFields.target_markets.ToString() + " [=========] " + data.target_markets);
            Console.WriteLine(AllFields.technology.ToString() + " [=========] " + data.technology);
            Console.WriteLine(AllFields.telephone.ToString() + " [=========] " + data.telephone);
            Console.WriteLine(AllFields.company_type.ToString() + " [=========] " + data.company_type);
            Console.WriteLine(AllFields.website.ToString() + " [=========] " + data.website);
            Console.WriteLine(AllFields.zip.ToString() + " [=========] " + data.zip);

            bool status = InsertDataToDB(data.address, data.website, data.company_type, data.sector, data.sub_sector, data.company_stage, data.established, data.number_employees, data.reg_number, data.description, data.technology, data.target_markets, data.tags, data.address, data.city, data.country, data.zip, data.contact_person, data.contact_email, data.company_email, data.telephone);
        }

        private static string RemoveNumberFromXPathString(string xpathString)
        {
            //return Regex.Replace(xpathString, @"[\d-]", string.Empty);
            return Regex.Replace(xpathString, @"([[\d]+])", string.Empty);
        }

        private static void InsertDataToDB()
        {
            try
            {
                //This is my insert query in which i am taking input from the user through windows forms
                string Query = "insert into ivconline.company(name,website,company_type,sector,sub_sector,company_stage,established,number_employees,reg_number,description,technology,target_markets,tags,address,city,country,zip,contact_person,contact_email,company_email,telephone) values(1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1)";
                //This is  MySqlConnection here i have created the object and pass my connection string.
                MySqlConnection mySQLConnection = new MySqlConnection(mySQLConnectionString);
                ////This is command class which will handle the query and connection object.
                if (mySQLConnection.Ping())
                {
                    MySqlCommand mySQLCommand = new MySqlCommand(Query, mySQLConnection);
                    MySqlDataReader mySQLDataReader;
                    mySQLConnection.Open();
                    mySQLDataReader = mySQLCommand.ExecuteReader();     // Here our query will be executed and data saved into the database.
                    Console.WriteLine("Save Data");
                    while (mySQLDataReader.Read())
                    {

                    }
                    mySQLConnection.Close();
                }
                else
                {
                    Console.WriteLine("Server Down");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Occureed: " + ex);
            }
        }
        private static bool InsertDataToDB(string nameValue, string websiteValue, string company_typeValue, string sectorValue, string sub_sectorValue, string company_stageValue, int establishedValue, int number_employeesValue, int reg_numberValue, string descriptionValue, string technologyValue, string target_marketsValue, string tagsValue, string addressValue, string cityValue, string countryValue, int zipValue, string contact_personValue, string contact_emailValue, string company_emailValue, string telephone)
        {
            try
            {
                bool connectionStatus = false;
                char comma = ',';
                string Query = "INSERT INTO ivconline.company(" + AllFields.name.ToString() + comma +
                    AllFields.name.ToString() + comma +
                    AllFields.website.ToString() + comma +
                    AllFields.company_type.ToString() + comma +
                    AllFields.sector.ToString() + comma +
                    AllFields.sub_sector.ToString() + comma +
                    AllFields.company_stage.ToString() + comma +
                    AllFields.established.ToString() + comma +
                    AllFields.number_employees.ToString() + comma +
                    AllFields.reg_number.ToString() + comma +
                    AllFields.description.ToString() + comma +
                    AllFields.technology.ToString() + comma +
                    AllFields.target_markets.ToString() + comma +
                    AllFields.tags.ToString() + comma +
                    AllFields.address.ToString() + comma +
                    AllFields.city.ToString() + comma +
                    AllFields.country.ToString() + comma +
                    AllFields.zip.ToString() + comma +
                    AllFields.contact_person.ToString() + comma +
                    AllFields.contact_email.ToString() + comma +
                    AllFields.company_email.ToString() + comma +
                    AllFields.telephone.ToString() + ") VALUES (" +
                    nameValue + comma +
                    websiteValue + comma +
                    company_typeValue + comma +
                    sectorValue + comma +
                    sub_sectorValue + comma +
                    company_stageValue + comma +
                    establishedValue + comma +
                    number_employeesValue + comma +
                    reg_numberValue + comma +
                    descriptionValue + comma +
                    technologyValue + comma +
                    target_marketsValue + comma +
                    tagsValue + comma +
                    addressValue + comma +
                    cityValue + comma +
                    countryValue + comma +
                    zipValue + comma +
                    contact_personValue + comma +
                    contact_emailValue + comma +
                    company_emailValue + comma +
                    telephone + ")";

                //This is  MySqlConnection here i have created the object and pass my connection string.
                MySqlConnection mySQLConnection = new MySqlConnection(mySQLConnectionString);
                ////This is command class which will handle the query and connection object.
                //if (mySQLConnection.Ping())
                //{
                    connectionStatus = true;
                    MySqlCommand mySQLCommand = new MySqlCommand(Query, mySQLConnection);
                    MySqlDataReader mySQLDataReader;
                    mySQLConnection.Open();
                    mySQLDataReader = mySQLCommand.ExecuteReader();     // Here our query will be executed and data saved into the database.
                    Console.WriteLine("Save Data");
                    while (mySQLDataReader.Read())
                    {

                    }
                    mySQLConnection.Close();
                //}
                //else
                //{
                    connectionStatus = false;
                    Console.WriteLine("Server Down");
                //}
                return connectionStatus;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Occureed: " + ex);
                return false;
            }
        }
        struct CompanyData
        {
            public string name;
            public string website;
            public string company_type;
            public string sector;
            public string sub_sector;
            public string company_stage;
            public int established;
            public int number_employees;
            public int reg_number;
            public string description;
            public string technology;
            public string target_markets;
            public string tags;
            public string address;
            public string city;
            public string country;
            public int zip;
            public string contact_person;
            public string contact_email;
            public string company_email;
            public string telephone;
        }
        public enum AllFields
        {
            name,
            website,
            company_type,
            sector,
            sub_sector,
            company_stage,
            established,
            number_employees,
            reg_number,
            description,
            technology,
            target_markets,
            tags,
            address,
            city,
            country,
            zip,
            contact_person,
            contact_email,
            company_email,
            telephone,
        }
    }
}
