using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace SVNHookGenerator
{
    public struct GroupStructure
    {
        public string groupName;
        public List<string> listOfGroupUsers;
        public List<string> listOfDomainUsers;
    }
    public struct VisualSVNHookStructure
    {
        public string applicationPath;
        public string notificationDetail;
        public string fromKeyword;
        public string fromEmail;
        public string toKeyword;
        public List<string> toEmail;
        public string filterKeyword;
        public string filterPath;
        public string smtpKeyword;
        public string smtpAddress;
        public string noDiffereceKeyword;
    }
    public struct CSVFileStructure
    {
        public string companyName;
        public List<ProjectName> projectName;
    }
    public struct ProjectName
    {
        public string projectName;
        public List<UsersName> userName;
    }
    public struct UsersName
    {
        public string userName;
    }


    public class XMLFile
    {
        public void WriteXMLFile(List<GroupStructure> listOfGroupsUsers, string path)
        {
            string folderName = Path.GetDirectoryName(path);
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }
            // Create a new file in C:\\ dir  
            XmlTextWriter textWriter = new XmlTextWriter(path, Encoding.UTF8);

            textWriter.Formatting = System.Xml.Formatting.Indented;

            textWriter.WriteStartDocument();
            textWriter.WriteStartElement("Groups");
            //textWriter.WriteComment("First Comment XmlTextWriter Sample Example");

            foreach (var groupName in listOfGroupsUsers)
            {
                //textWriter.WriteStartElement("Groups");
                string groupWithoutSpaces = groupName.groupName.Replace(" ", string.Empty);
                textWriter.WriteStartElement(groupWithoutSpaces);
                textWriter.WriteComment("Actual Group Name: " + groupName.groupName);

                foreach (var user in groupName.listOfGroupUsers)
                {
                    //textWriter.WriteStartElement("User");
                    string userLower = user.ToLower();
                    if (userLower.Contains("mossadmin240") ||
                        userLower.Contains("sjpsrvadmin"))
                    {
                        continue;
                    }
                    else
                    {
                        textWriter.WriteStartElement("User");
                        textWriter.WriteString(user);
                        textWriter.WriteEndElement();
                    }
                }
                textWriter.WriteEndElement();
            }
            textWriter.WriteEndElement();
            textWriter.WriteEndDocument();
            textWriter.Close();
        }
    }
    class CSVFileReader
    {
        public List<CSVFileStructure> ReadCSVFile(string csvFilename)
        {
            List<CSVFileStructure> listToReturn = new List<CSVFileStructure>();

            var SplittedCSVFile = File.ReadAllLines(csvFilename).Select(line => line.Split(',')).ToList();

            CSVFileStructure cSVFile = new CSVFileStructure();


            foreach (var item in SplittedCSVFile)
            {
                if (item[0] == "\"Repository\"" ||
                    item[0] == "\"*\"" ||
                    item[1] == "\"/\"")
                {
                    continue;
                }


                //listToReturn.Where(x => x.companyName == item[0]).Count();

                if (listToReturn.Where(t => t.companyName == "lahore").FirstOrDefault().companyName != "lahore")
                { }


                if (listToReturn.Where(x => x.companyName == item[0]).Count() > 0) continue;

                for (int i = 0; i < item.Length; i++)
                {
                    // CSVFileStructure cSVFile = new CSVFileStructure();
                    switch (i)
                    {
                        case 0:
                            if (listToReturn.Where(x => x.companyName == item[0]).Count() > 0) continue;

                            cSVFile.companyName = item[i].ToString();
                            break;
                        case 1:
                            //cSVFile.projectName.projectName = item[i].ToString();
                            break;
                        case 2:
                            //cSVFile.projectName.userName.userName = item[i].ToString();
                            break;
                        default:
                            break;
                    }
                }

                listToReturn.Add(cSVFile);
            }

            return listToReturn;
        }

        public List<CSVFileStructure> ReadCSVFileNew(string csvFilename)
        {
            List<CSVFileStructure> listToReturn = new List<CSVFileStructure>();

            var SplittedCSVFile = File.ReadAllLines(csvFilename).Select(line => line.Split(',')).ToList();

            CSVFileStructure cSVFile = new CSVFileStructure();

            foreach (var item in SplittedCSVFile)
            {
                cSVFile.companyName = item[0];
                //cSVFile.projectName.projectName = item[1];
                //cSVFile.projectName.userName.userName = item[2];
            }

            foreach (var item in SplittedCSVFile)
            {
                //listToReturn.Where(x => x.companyName == item[0]).Count();

                if (listToReturn.Where(t => t.companyName == "lahore").FirstOrDefault().companyName != "lahore")
                { }


                if (listToReturn.Where(x => x.companyName == item[0]).Count() > 0) continue;

                for (int i = 0; i < item.Length; i++)
                {
                    // CSVFileStructure cSVFile = new CSVFileStructure();
                    switch (i)
                    {
                        case 0:
                            if (listToReturn.Where(x => x.companyName == item[0]).Count() > 0) continue;
                            //cSVFile.companyName = item[i].ToString();
                            break;
                        case 1:
                            //cSVFile.projectName.projectName = item[i].ToString();
                            break;
                        case 2:
                            //cSVFile.projectName.userName.userName = item[i].ToString();
                            break;
                        default:
                            break;
                    }
                }

                listToReturn.Add(cSVFile);
            }

            return listToReturn;
        }

        public List<CSVFileStructure> ReadCSVFileNew1(string csvFilename)
        {
            List<CSVFileStructure> listToReturn = new List<CSVFileStructure>();

            var SplittedCSVFile = File.ReadAllLines(csvFilename).Select(line => line.Split(',')).ToList();

            CSVFileStructure cSVFile = new CSVFileStructure();

            foreach (var item in SplittedCSVFile)
            {
                cSVFile.companyName = item[0];
                //cSVFile.projectName.Add. = item[1];
                //cSVFile.projectName.userName.userName = item[2];
            }

            foreach (var item in SplittedCSVFile)
            {
                //listToReturn.Where(x => x.companyName == item[0]).Count();

                if (listToReturn.Where(t => t.companyName == "lahore").FirstOrDefault().companyName != "lahore")
                { }


                if (listToReturn.Where(x => x.companyName == item[0]).Count() > 0) continue;

                for (int i = 0; i < item.Length; i++)
                {
                    // CSVFileStructure cSVFile = new CSVFileStructure();
                    switch (i)
                    {
                        case 0:
                            if (listToReturn.Where(x => x.companyName == item[0]).Count() > 0) continue;
                            //cSVFile.companyName = item[i].ToString();
                            break;
                        case 1:
                            //cSVFile.projectName.projectName = item[i].ToString();
                            break;
                        case 2:
                            //cSVFile.projectName.userName.userName = item[i].ToString();
                            break;
                        default:
                            break;
                    }
                }

                listToReturn.Add(cSVFile);
            }

            return listToReturn;
        }
    }

    class CSVToJSON
    {
        public void CSVToJSONFunc(string filePath)
        {
            var jsonString = ConvertCsvFileToJsonObject();
            File.WriteAllText(@"D:\output.json", jsonString);
            //ConvertCsvFileToJsonObject();
            string ConvertCsvFileToJsonObject()
            {
                //string path = "C:\\Dev\\CSVtoJSON\\csvtojson.csv";
                var csv = new List<string[]>();
                var lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                    csv.Add(line.Split(','));

                var properties = lines[0].Split(',');

                var listObjResult = new List<Dictionary<string, string>>();

                for (int i = 1; i < lines.Length; i++)
                {
                    var objResult = new Dictionary<string, string>();
                    for (int j = 0; j < properties.Length; j++)
                        objResult.Add(properties[j], csv[i][j]);

                    listObjResult.Add(objResult);
                }

                return JsonConvert.SerializeObject(listObjResult);
            }
        }
    }

    class JSONwriterClass
    {
        public void JSONWriteFunc(string filePath)
        {

            var SplittedCSVFile = File.ReadAllLines(filePath).Select(line => line.Split(',')).ToList();
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Newtonsoft.Json.Formatting.Indented;

                writer.WriteStartObject();

                List<string> listCompanyName = new List<string>();

                foreach (var item in SplittedCSVFile)
                {
                    if (!listCompanyName.Contains(item[0]))
                    {
                        listCompanyName.Add(item[0]);

                        writer.WritePropertyName(item[0]);
                        writer.WriteStartArray();
                        ////writer.WritePropertyName(item[1]);
                        //writer.WriteStartArray();
                        //writer.WriteValue(item[2]);
                        //writer.WriteEnd();
                        ////writer.WriteValue("Intel");
                        ////writer.WritePropertyName(item[0]);
                        //writer.WriteValue("Intel");
                        ////writer.WriteEnd();
                    }
                    //writer.WritePropertyName(item[1]);
                    writer.WriteStartArray();
                    writer.WriteValue(item[2]);
                    writer.WriteEnd();
                    //writer.WriteValue("Intel");
                    //writer.WritePropertyName(item[0]);
                    writer.WriteValue("Intel");
                    //writer.WriteEnd();

                }


                writer.WritePropertyName("CPU");
                writer.WriteValue("Intel");
                writer.WritePropertyName("PSU");
                writer.WriteValue("500W");
                writer.WritePropertyName("Drives");
                writer.WriteStartArray();
                writer.WriteValue("DVD read/writer");
                writer.WriteComment("(broken)");
                writer.WriteValue("500 gigabyte hard drive");
                writer.WriteValue("200 gigabyte hard drive");
                writer.WriteEnd();

                writer.WriteEndObject();
            }

            File.WriteAllText("ssddf.json", sb.ToString());
        }
    }
    class ReferenceTaswar
    {
        public List<Company> Function(string fileAndPath)
        {
            try
            {
                List<ProjectData> projectsData = File.ReadAllLines(fileAndPath).Skip(1).Select(v => ProjectData.FromCsv(v)).ToList();

                List<string> distinct_companies = projectsData.Select(t => t.Comapny).Distinct().ToList();
                List<Company> lstCompany = new List<Company>();
                foreach (String company in distinct_companies)
                {
                    if (company.Contains("*"))
                        continue;
                    List<string> distinct_projects = projectsData.Where(t => t.Comapny == company).Select(t => t.Projects).Distinct().ToList();
                    List<projects> lstProject = new List<projects>();
                    foreach (string project in distinct_projects)
                    {
                        List<string> distinct_groups = projectsData.Where(t => t.Comapny == company && t.Projects == project).Select(t => t.Groups).Distinct().ToList();
                        List<groups> lstGroup = new List<groups>();
                        foreach (string group in distinct_groups)
                        {                          
                            lstGroup.Add(new groups { group_name = group });
                        }
                        lstProject.Add(new projects { project_name = project, groups = lstGroup });
                    }
                    lstCompany.Add(new Company { company_name = company, projects = lstProject });
                }
                Console.WriteLine("Done!");
                return lstCompany;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex, "Error in application", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null; ;
            }
        }
    }
    public class ProjectData
    {
        public string Comapny { get; set; }
        public string Projects { get; set; }
        public string Groups { get; set; }
        public static ProjectData FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            ProjectData dailyValues = new ProjectData();
            dailyValues.Comapny = values[0];
            dailyValues.Projects = values[1];
            dailyValues.Groups = values[2];
            return dailyValues;
        }
    }
    public class Company
    {
        public string company_name { get; set; }
        public List<projects> projects { get; set; }
    }
    public class projects
    {
        public string project_name { get; set; }
        public List<groups> groups { get; set; }
    }
    public class groups
    {
        public string group_name { get; set; }
    }
}
