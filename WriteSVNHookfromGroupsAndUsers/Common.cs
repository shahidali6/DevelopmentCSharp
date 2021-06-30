using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp4ReadGroups
{
    public struct GroupStructure
    {
        public string groupName;
        public List<string> listOfUsers;
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

            textWriter.Formatting = Formatting.Indented;

            textWriter.WriteStartDocument();
            textWriter.WriteStartElement("Groups");
            //textWriter.WriteComment("First Comment XmlTextWriter Sample Example");

            foreach (var groupName in listOfGroupsUsers)
            {
                //textWriter.WriteStartElement("Groups");
                string groupWithoutSpaces = groupName.groupName.Replace(" ", string.Empty);
                textWriter.WriteStartElement(groupWithoutSpaces);
                textWriter.WriteComment("Actual Group Name: " + groupName.groupName);

                foreach (var user in groupName.listOfUsers)
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
}