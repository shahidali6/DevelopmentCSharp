using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Mail;

namespace C_Sharp
{
    public class PostCommit
    {
        private static string svnpath = Environment.GetEnvironmentVariable("VISUALSVN_SERVER");
        private static string addedLegend = "<li style=\"padding: 3px;\"><span style=\"padding: 3px; margin: 1px 4px 1px 3px; width: 75px; background-color: #198754; color: white; border-radius: 5px;\"> Added</span>";
        private static string modifiedLegend = "<li style=\"padding: 3px;\"><span style=\"padding: 3px; margin: 1px 4px 1px 3px; width: 75px; background-color: #FFC107; border-radius: 5px;\"> Modified</span>";
        private static string deletedLegend = "<li style=\"padding: 3px;\"><span style=\"padding: 3px; margin: 1px 4px 1px 3px; width: 75px; background-color: #DC3545; color: white; border-radius: 5px;\"> Deleted</span>";
        private static int Main(string[] args)
        {
            WriteDebuggLog("Path: " + svnpath);

            int loopcounter = 0;
            foreach (var arg in args)
            {
                WriteDebuggLog("args" + loopcounter + ": " + arg);
                loopcounter++;
            }

            if (args.Length < 2)
            {
                WriteDebuggLog("Invalid arguments sent - <REPOSITORY> <REV> required");
                return 1;
            }
            if (string.IsNullOrEmpty(svnpath))
            {
                WriteDebuggLog("VISUALSVN_SERVER environment variable does not exist. VisualSVN installed?");
                return 1;
            }

            //Get the required information using SVNLook.
            string author = SVNLook("author", args);
            WriteDebuggLog("SVNLook(\"author\", args): " + author.Trim());

            string message = SVNLook("log", args);
            WriteDebuggLog("SVNLook(\"log\", args): " + message.Trim());

            string changed = SVNLook("changed", args);
            WriteDebuggLog("SVNLook(\"changed\", args): " + changed.Trim());

            //Get the branch from the first change in the list.
            ///string[] changeList = changed.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            string[] changeList = changed.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            List<string> listOfChanges = new List<string>();
            listOfChanges.Add("<ul>");
            loopcounter = 0;
            foreach (var change in changeList)
            {
                if (change.Length > 0)
                {
                    //listOfChanges.Add("<li>" + change + "</li>");

                    string firstLetter = change.ToLower().Remove(1);
                    string removeStrating4Letters = change.Remove(0,4);

                    switch (firstLetter)
                    {
                        case "u":
                            listOfChanges.Add(modifiedLegend + removeStrating4Letters + "</li>");
                            break;
                        case "a":
                            listOfChanges.Add(addedLegend + removeStrating4Letters + "</li>");
                            break;
                        case "d":
                            listOfChanges.Add(deletedLegend + removeStrating4Letters + "</li>");
                            break;
                        default:
                            break;
                    }
                }
                WriteDebuggLog("change " + loopcounter + ": " + change);
            }
            loopcounter++;
            listOfChanges.Add("</ul>");

            string newOutput = ListToString(listOfChanges);

            string changeFirst = changeList[0].Remove(0, 4);

            WriteDebuggLog("First change: " + changeFirst);


            int changeFirstSlash = changeFirst.IndexOf("/");
            if (changeFirstSlash > 0)
            {
                string repoBranch = changeFirst.Substring(0, changeFirstSlash);
            }

            //Get the name of the repository from the first argument, which is the repo path.
            string repoName = args[0].ToString().Substring(args[0].LastIndexOf(@"\") + 1);

            //Get the email template and fill it in. This template can be anywhere, and can be a .HTML file
            //for more control over the structure.
            string emailTemplatePath = @"d:\svnnotification.html";
            //string emailTemplate = string.Format(File.ReadAllText(emailTemplatePath), author, message, changed);
            string emailTemplate = string.Format(File.ReadAllText(emailTemplatePath), author, message, newOutput);

            //Construct the email that will be sent. You can use the .IsBodyHtml property if you are
            //using an HTML template.
            string subject = string.Format("[Commit Notification] commit number {0} for {1}", args[1], repoName);
            MailMessage mm = new MailMessage("msaddique@powersoft19.com", "msaddique@powersoft19.com");

            mm.IsBodyHtml = true;
            mm.Body = emailTemplate;
            mm.Subject = subject;

            //Define your mail client. I am using Gmail here as the SMTP server, but you could
            //use IIS or Amazon SES or whatever you want.
            SmtpClient mailClient = new SmtpClient("mail2.powersoft19.com");
            mailClient.Port = 587;
            mailClient.Credentials = new System.Net.NetworkCredential("msaddique", "shahid@lahore");
            mailClient.EnableSsl = true;

            mailClient.Send(mm);
            WriteDebuggLog("Reach at the end of Code" + Environment.NewLine + "===================================================================");
            return 0;
        }

        private static string ListToString(List<string> listOfChanges)
        {
            string newString = string.Empty;
            foreach (var item in listOfChanges)
            {
                newString += item;
            }
            return newString;
        }

        private static void WriteDebuggLog(string content)
        {
            string roamingFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData, Environment.SpecialFolderOption.None);
            //string roamingFolderPath = @"d:\";
            string appPath = Path.Combine(roamingFolderPath, "VisualSVNHook\\");

            if (!Directory.Exists(appPath))
            {
                Directory.CreateDirectory(appPath);
            }

            string fileName = DateTime.Now.ToString("yyyyMMdd");

            string timeStamp = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
            File.AppendAllText(appPath + fileName + ".txt", timeStamp + ": " + content + Environment.NewLine);
        }

        /// <summary>
        /// Runs a command on svnlook.exe to get information
        /// about a particular repo and revision.
        /// </summary>
        /// <param name="command">The svnlook command e.g. log, author, message.</param>
        /// <param name="args">The arguments passed in to this exe (repo name and rev number).</param>
        /// <returns>The output of svnlook.exe</returns>
        private static string SVNLook(string command, string[] args)
        {
            StringBuilder output = new StringBuilder();
            Process procMessage = new Process();

            //Start svnlook.exe in a process and pass it the required command-line args.
            string formatedString = String.Format(@"{0} ""{1}"" -r ""{2}""", command, args[0], args[1]);
            //procMessage.StartInfo = new ProcessStartInfo(svnpath + @"bin\svnlook.exe", String.Format(@"{0} ""{1}"" -r ""{2}""", command, args[0], args[1]));
            procMessage.StartInfo = new ProcessStartInfo(svnpath + @"bin\svnlook.exe", formatedString);
            WriteDebuggLog("Formated String: " + formatedString);
            procMessage.StartInfo.RedirectStandardOutput = true;
            procMessage.StartInfo.UseShellExecute = false;
            procMessage.Start();

            //While reading the output of svnlook, append it to the stringbuilder then
            //return the output.
            while (!procMessage.HasExited)
            {
                output.Append(procMessage.StandardOutput.ReadToEnd());
            }

            return output.ToString();
        }
    }
}
