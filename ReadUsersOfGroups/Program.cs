using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp4ReadGroups
{
    class Program
    {
        static List<string> listOfSystemGroups = new List<string>();
        static List<GroupStructure> listOfGroupsUsers = new List<GroupStructure>();

        static void Main(string[] args)
        {
            string commonApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData, Environment.SpecialFolderOption.None);
            string appPath = Path.Combine(commonApplicationData, "VisualSVNHook\\");

            listOfSystemGroups = ExtractNameOfGroups();

            listOfGroupsUsers = ExtractUsersOfGroups(listOfSystemGroups);

            WriteXMLFile(listOfGroupsUsers, appPath + "Settings.xml");
        }

        private static void WriteXMLFile(List<GroupStructure> listOfGroupsUsers, string path)
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
                if (groupName.groupName.Contains("$"))
                {
                    continue;
                }

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

                    }
                    else
                    {
                        textWriter.WriteStartElement("User");
                        textWriter.WriteString(user);
                        textWriter.WriteEndElement();
                    }

                }
                textWriter.WriteEndElement();
                //textWriter.WriteEndElement();
            }
            textWriter.WriteEndElement();
            textWriter.WriteEndDocument();
            textWriter.Close();
        }

        private static List<GroupStructure> ExtractUsersOfGroups(List<string> listOfGroupNames)
        {
            List<GroupStructure> listOfGroupsUsersInternal = new List<GroupStructure>();

            // ArrayList myGroups = GetGroupMembers("Administrators");
            foreach (var group in listOfGroupNames)
            {
                GroupStructure listOfGroupsUsers = new GroupStructure();
                List<string> listOfMembersOnly = new List<string>();

                foreach (string item in GetGroupMembers(group))
                {
                    listOfMembersOnly.Add(item);
                }

                listOfGroupsUsers.groupName = group;
                listOfGroupsUsers.listOfUsers = listOfMembersOnly;

                listOfGroupsUsersInternal.Add(listOfGroupsUsers);
            }
            return listOfGroupsUsersInternal;
        }

        private static List<string> ExtractNameOfGroups()
        {
            List<string> listOfGroups = new List<string>();
            DirectoryEntry machine = new DirectoryEntry("WinNT://" + Environment.MachineName + ",Computer");
            foreach (DirectoryEntry child in machine.Children)
            {
                if (child.SchemaClassName == "Group")
                {
                    listOfGroups.Add(child.Name);
                    continue;
                }
            }
            return listOfGroups;
        }

        struct GroupStructure
        {
            public string groupName;
            public List<string> listOfUsers;
        }
        public static List<string> GetGroupMembers(string sGroupName)
        {
            List<string> listOfUSerssss = new List<string>();

            listOfUSerssss = GetGroup(sGroupName);

            return listOfUSerssss;
        }

        public static List<string> GetGroup(string sGroupName)
        {
            List<string> listOUsers = new List<string>();
            PrincipalContext oPrincipalContext = GetPrincipalContext();

            GroupPrincipal oGroupPrincipal = GroupPrincipal.FindByIdentity(oPrincipalContext, sGroupName);

            var listofMembers = oGroupPrincipal.Members;

            //Console.WriteLine("============================");

            foreach (var item in listofMembers)
            {
                if (item.ContextType.ToString() == "Domain")
                {
                    //Console.WriteLine("Name :" + item.Name);
                    if (item.SamAccountName.Contains(" "))
                    {
                        continue;
                    }
                    listOUsers.Add(item.SamAccountName + "@powersoft19.com");
                    ;                    //Console.WriteLine("SamAccountName :" + item.SamAccountName);
                }
            }
            return listOUsers;
        }

        public static PrincipalContext GetPrincipalContext()
        {
            PrincipalContext oPrincipalContext = new PrincipalContext(ContextType.Machine);
            return oPrincipalContext;
        }
    }
}
