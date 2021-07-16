using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4ReadGroups
{
    class Hook
    {
        public void WriteHookFile(List<Company> listToReturn, string xMLFileName, string postHookFilename)
        {
            string lineTerminaltor = " ^";
            string visualSVNServerHookPath = "\"%VISUALSVN_SERVER%\\bin\\VisualSVNServerHooks.exe\"";
            string commitNotification = "    commit-notification \"%1\" -r %2";
            string filterKeyword = "    --filter ";
            string smtpKeyword = " --smtp-server ";
            string smtpServerAddress = "mail2.powersoft19.com";
            string fromKeyword = "    --from ";
            string toKeyword = " --to ";
            string noDifferenceKeyword = " --no-diffs ";
            string fromEmail = "mossadmin240@powersoft19.com";
            string semiColon = ";";
            string powerDomain = "POWER\\";
            string powersoft19Dotcom = "@powersoft19.com";
            string[] mustEmailAddress = { "asadiq@powersoft19.com", "msaddique@powersoft19.com" };


            string hookFile = "hookFile.txt";
            string commitFile = "post-commit.cmd";

            if (File.Exists(hookFile))
            {
                File.Delete(hookFile);
            }
                foreach (var company in listToReturn)
                {
                string cleanCompanyName = company.company_name.Replace("\"", string.Empty);
                string path = "Q:\\temp\\"+cleanCompanyName+"\\hooks\\" ;

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                using (StreamWriter sw = new StreamWriter(path+commitFile, false))
                {
                    foreach (var project in company.projects)
                    {
                        StringBuilder emailList = new StringBuilder();
                        foreach (var users in project.groups)
                        {
                            if (users.group_name.ToLower().Contains(powerDomain.ToLower()))
                            {
                                users.group_name = users.group_name.Replace(powerDomain, string.Empty);
                                users.group_name = users.group_name.Replace("\"", string.Empty);
                                users.group_name = users.group_name + powersoft19Dotcom;
                            }
                            emailList.Append(users.group_name + semiColon);
                        }

                        foreach (var email in mustEmailAddress)
                        {
                            if (!emailList.ToString().Contains(email))
                            {
                                emailList.Append(email + semiColon);
                            }
                        }

                        sw.WriteLine(visualSVNServerHookPath + lineTerminaltor);
                        sw.WriteLine(commitNotification + lineTerminaltor);
                        sw.WriteLine(fromKeyword + fromEmail + toKeyword + emailList + lineTerminaltor);
                        sw.WriteLine(filterKeyword + project.project_name + smtpKeyword + smtpServerAddress + noDifferenceKeyword + Environment.NewLine);
                    }
                }
            }
        }
    }
}