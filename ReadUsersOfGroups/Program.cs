using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4ReadGroups
{
    class Program
    {
        static List<string> listOfSystemGroups = new List<string>();
        static List<string> listOfUsers = new List<string>();
        List<GroupStructure> listOfGroupsUsers = new List<GroupStructure>();

        static void Main(string[] args)
        {
            string roamingFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.None);
            string appPath = Path.Combine(roamingFolderPath, "VisualSVNHook\\");

            listOfSystemGroups = ExtractNameOfGroups();

            listOfGroupsUsers =  ExtractUsersOfGroups(listOfSystemGroups);
        }

        private static List<GroupStructure> ExtractUsersOfGroups(List<GroupStructure> listOfGroupNames)
        {
            // ArrayList myGroups = GetGroupMembers("Administrators");
            foreach (var group in listOfGroupNames)
            {
                Console.WriteLine("Group Name: "+group);
                ArrayList myGroups = GetGroupMembers(group);
                foreach (string item in myGroups)
                {
                    Console.WriteLine("Group Member: " + item);
                } 
            }
            Console.ReadLine();
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
                    Console.WriteLine("Group: " + child.Name);
                    foreach (var childchild in child.Children)
                    {
                        Console.WriteLine("Group: childchild" + childchild);
                    }
                    continue;
                }
                if (child.SchemaClassName == "User")
                {
                    listOfUsers.Add(child.Name);
                    Console.WriteLine("User: " + child.Name);
                    continue;
                }
                //Console.WriteLine(child.Name);
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

           var listofMembers =  oGroupPrincipal.Members;

            Console.WriteLine("============================");

            foreach (var item in listofMembers)
            {
                if (item.ContextType.ToString() == "Domain")
                {
                    Console.WriteLine("Name :" + item.Name);
                    if (item.SamAccountName.Contains(" "))
                    {
                        Console.WriteLine("SamAccountName :" + item.SamAccountName+" Space===================");
                    }
                    else
                    {
                    Console.WriteLine("SamAccountName :" + item.SamAccountName);

                    }
                    Console.WriteLine("Context :" + item.Context);
                    Console.WriteLine("ContextType :" + item.ContextType);
                    Console.WriteLine("DisplayName :" + item.DisplayName);
                    Console.WriteLine("DistinguishedName :" + item.DistinguishedName);
                    Console.WriteLine("============================");
                    List<Principal> nsdfsdf = new List<Principal>();

                }

                //Console.WriteLine(item.);
                //var namama = ((UserPrincipal)item).EmailAddress;
                //if (((UserPrincipal)item).EmailAddress != null)
                //{
                //    //var namama = ((UserPrincipal)item).EmailAddress;
                //}              

            }

            //((UserPrincipal)(new SystemCore_EnumerableDebugView<Principal>(oGroupPrincipal.Members).Items[2])).EmailAddress;
            return oGroupPrincipal;
        }

        public static PrincipalContext GetPrincipalContext()
        {
            PrincipalContext oPrincipalContext = new PrincipalContext(ContextType.Machine);
            return oPrincipalContext;
        }

    }
}
