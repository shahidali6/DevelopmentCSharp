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
        static List<string> listOfUsers = new List<string>();
        static List<GroupStructure> listOfGroupsUsers = new List<GroupStructure>();

        static void Main(string[] args)
        {
            string roamingFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.None);
            string appPath = Path.Combine(roamingFolderPath, "VisualSVNHook\\");

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
                //textWriter.WriteStartElement("Groups");
                string groupWithoutSpaces = groupName.groupName.Replace(" ", string.Empty);
                textWriter.WriteStartElement(groupWithoutSpaces);

                foreach (var user in groupName.listOfUsers)
                {
                    textWriter.WriteStartElement("User");
                    textWriter.WriteString(user);
                    textWriter.WriteEndElement();
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

                //Console.WriteLine("Group Name: "+group);
                ArrayList myGroups = GetGroupMembers(group);
                foreach (string item in myGroups)
                {
                    if (!item.Contains(" "))
                    {
                        listOfMembersOnly.Add(item);
                    }
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
        public static ArrayList GetGroupMembers(string sGroupName)
        {
            ArrayList myItems = new ArrayList();
            GroupPrincipal oGroupPrincipal = GetGroup(sGroupName);
            var test = oGroupPrincipal.UserPrincipalName;
            PrincipalSearchResult<Principal> oPrincipalSearchResult = oGroupPrincipal.GetMembers();

            foreach (Principal oResult in oPrincipalSearchResult)
            {
                myItems.Add(oResult.SamAccountName);
            }
            return myItems;
        }

        public static GroupPrincipal GetGroup(string sGroupName)
        {
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
                    Console.WriteLine("SamAccountName :" + item.SamAccountName);
                }
            }
            return oGroupPrincipal;
        }

        public static PrincipalContext GetPrincipalContext()
        {
            PrincipalContext oPrincipalContext = new PrincipalContext(ContextType.Machine);
            return oPrincipalContext;
        }
    }
}
