using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVNHookGenerator
{
    class SVNHook
    {
        public bool WriteHookFile(List<Company> listToReturn, string xMLFileName, string postHookFilename)
        {
            try
            {
                if (listToReturn == null)
                {
                    return false;
                }

                string lineTerminaltor = " ^";
                string visualSVNServerHookPath = "\"%VISUALSVN_SERVER%\\bin\\VisualSVNServerHooks.exe\"";
                string commitNotification = "   commit-notification \"%1\" -r %2";
                string filterKeyword = "   --filter ";
                string smtpKeyword = "   --smtp-server ";
                string smtpServerAddress = "mail2.powersoft19.com";
                string fromKeyword = "   --from ";
                string toKeyword = " --to ";
                string noDifferenceKeyword = "   --no-diffs";
                string fromEmail = "\"mossadmin240@powersoft19.com\"";
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

                List<char> listOfAllInvalidChars = new List<char>();

                // Get a list of invalid path characters.
                // Get a list of invalid file characters.
                //combine both arrays to list

                listOfAllInvalidChars = Path.GetInvalidPathChars().Concat(Path.GetInvalidFileNameChars()).Distinct().ToList();

                foreach (var company in listToReturn)
                {
                    company.company_name = company.company_name.Replace("\"", "");
                    if (CheckInvalidcharacter(listOfAllInvalidChars, company.company_name))
                        continue;

                    string cleanCompanyName = company.company_name.Replace("\"", string.Empty);
                    string path = "F:\\Repositories\\" + cleanCompanyName + "\\hooks\\";

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (StreamWriter sw = new StreamWriter(path + commitFile, false))
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

                            emailList = emailList.Remove(emailList.Length - 1, 1);

                            sw.WriteLine(visualSVNServerHookPath + lineTerminaltor);
                            sw.WriteLine(commitNotification + lineTerminaltor);
                            sw.WriteLine(fromKeyword + fromEmail + toKeyword + "\""+ emailList +"\""+ lineTerminaltor);
                            if (project.project_name != "\"/\"")
                            {
                                sw.WriteLine(filterKeyword + project.project_name + lineTerminaltor);
                            }
                            sw.WriteLine(smtpKeyword + smtpServerAddress + lineTerminaltor);
                            sw.WriteLine(noDifferenceKeyword + Environment.NewLine);
                        }
                    }
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static bool CheckInvalidcharacter(List<char> listOfAllInvalidChars, string companyName)
        {
            foreach (var character in listOfAllInvalidChars)
            {
                if (companyName.Contains(character))
                {
                    return true;
                }
            }
            return false;
        }
    }
}