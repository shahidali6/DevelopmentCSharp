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
            string visualSVNServerHookPath = @"%VISUALSVN_SERVER%\bin\VisualSVNServerHooks.exe";
            string commitNotification = "commit-notification \"% 1\" -r %2";
            string filterKeyword = " --filter ";
            string filterPath = "/dsfdsf";
            string smtpKeyword = " --smtp-server ";
            string smtpServerAddress = "mail2.powersoft19.com";
            string fromKeyword = " --from ";
            string toKeyword = " --to ";
            string noDifferenceKeyword = " --no-diffs ";
            string fromEmail = "mossadmin240@powersoft19.com";
            string toEmail = "msaddique@powersoft19.com";
            string semiColon = ";";


            using (StreamWriter sw = new StreamWriter("hookFile.txt"))
            {
                foreach (var company in listToReturn)
                {
                    foreach (var project in company.projects)
                    {
                        StringBuilder emailList = new StringBuilder();
                        foreach (var users in project.groups)
                        {
                            emailList.Append(users.group_name + semiColon);
                        }
                        sw.WriteLine(visualSVNServerHookPath + lineTerminaltor);
                        sw.WriteLine(commitNotification + lineTerminaltor);
                        sw.WriteLine(fromKeyword + fromEmail + toKeyword + emailList + lineTerminaltor);
                        sw.WriteLine(filterKeyword + "\"" + project.project_name + "\"" + smtpKeyword + smtpServerAddress + noDifferenceKeyword + Environment.NewLine);
                    }
                }
            }
            //foreach (var company in listToReturn)
            //    {


            //        sw.WriteLine(visualSVNServerHookPath + lineTerminaltor);
            //        sw.WriteLine(commitNotification + lineTerminaltor);
            //        sw.WriteLine(fromKeyword + fromEmail + toKeyword + toEmail + lineTerminaltor);
            //        sw.WriteLine(filterKeyword+filterPath+smtpKeyword+smtpServerAddress+noDifferenceKeyword+Environment.NewLine);
            //    }
        }
    }
}