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
            string roamingFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.None);
            string appPath = Path.Combine(roamingFolderPath, "VisualSVNHook\\");

            GroupUser groupUser = new GroupUser();

            listOfSystemGroups = groupUser.ExtractNameOfGroups();

            listOfGroupsUsers = groupUser.ExtractUsersOfGroups(listOfSystemGroups);

            XMLFile xMLFile = new XMLFile();

            xMLFile.WriteXMLFile(listOfGroupsUsers, appPath + "Settings.xml");
        }
    }
}