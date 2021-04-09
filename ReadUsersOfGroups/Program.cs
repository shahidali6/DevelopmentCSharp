using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4ReadGroups
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryEntry machine = new DirectoryEntry("WinNT://" + Environment.MachineName + ",Computer");
            foreach (DirectoryEntry child in machine.Children)
            {
                
                if (child.SchemaClassName == "Group")
                {
                    Console.WriteLine("Group: " +child.Name);
                    continue;
                }
                if (child.SchemaClassName == "User")
                {
                    Console.WriteLine("User: " + child.Name);
                    continue;
                }
                Console.WriteLine(child.Name);
            }
            // ArrayList myGroups = GetGroupMembers("Administrators");
            ArrayList myGroups = GetGroupMembers("Administrators");
            foreach (string item in myGroups)
            {
                Console.WriteLine("Group Member: "+item);
            }
            Console.ReadLine();
        }
        public static ArrayList GetGroupMembers(string sGroupName)
        {
            ArrayList myItems = new ArrayList();
            GroupPrincipal oGroupPrincipal = GetGroup(sGroupName);
            var test = oGroupPrincipal.UserPrincipalName;
            PrincipalSearchResult<Principal> oPrincipalSearchResult = oGroupPrincipal.GetMembers();

            foreach (Principal oResult in oPrincipalSearchResult)
            {
                myItems.Add(oResult.Name);
            }
            return myItems;
        }

        public static GroupPrincipal GetGroup(string sGroupName)
        {
            PrincipalContext oPrincipalContext = GetPrincipalContext();

            GroupPrincipal oGroupPrincipal = GroupPrincipal.FindByIdentity(oPrincipalContext, sGroupName);
            return oGroupPrincipal;
        }

        public static PrincipalContext GetPrincipalContext()
        {
            PrincipalContext oPrincipalContext = new PrincipalContext(ContextType.Machine);
            return oPrincipalContext;
        }

    }
}
