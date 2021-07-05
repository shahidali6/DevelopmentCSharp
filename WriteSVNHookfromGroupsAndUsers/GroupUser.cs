using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4ReadGroups
{
    class GroupUser
    {
        public List<GroupStructure> ExtractUsersOfGroups(List<string> listOfGroupNames)
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
                listOfGroupsUsers.listOfGroupUsers = listOfMembersOnly;

                listOfGroupsUsersInternal.Add(listOfGroupsUsers);
            }
            return listOfGroupsUsersInternal;
        }

        private List<string> GetGroupMembers(string sGroupName)
        {
            List<string> listOfUSerssss = new List<string>();

            listOfUSerssss = GetGroup(sGroupName);

            return listOfUSerssss;
        }

        private List<string> GetGroup(string sGroupName)
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
        private PrincipalContext GetPrincipalContext()
        {
            PrincipalContext oPrincipalContext = new PrincipalContext(ContextType.Machine);
            return oPrincipalContext;
        }
        public List<string> ExtractNameOfGroups()
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






    }
}
