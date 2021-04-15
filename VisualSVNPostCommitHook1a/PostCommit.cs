using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Xml;

namespace C_Sharp
{
    public class PostCommit
    {
        private static string repositoryName = string.Empty;
        private static readonly string svnpath = Environment.GetEnvironmentVariable("VISUALSVN_SERVER");
        private static readonly string addedLegend = "<span style=\"background-color: #198754; color: white;\"> Added</span>";
        private static readonly string modifiedLegend = "<span style=\"background-color: #FFC107;\"> Modified</span>";
        private static readonly string deletedLegend = "<span style=\"background-color: #DC3545; color: white;\"> Deleted</span>";

        //private static readonly string addedIcon = "<img src=\"https://img.icons8.com/color/18/000000/add--v1.png\" />";
        //private static readonly string deletedIcon = "<img src=\"https://img.icons8.com/flat-round/15/000000/delete-sign.png\" />";
        //private static readonly string modifiedIcon = "<img src=\"https://img.icons8.com/color/15/000000/edit--v1.png\"/>";
        private static readonly string addedIcon = "<span style=\"color: green;\">&#10004;</span>";
        private static readonly string deletedIcon = "<span style=\"color: red;\">&#10006;</span>";
        private static readonly string modifiedIcon = "<span style=\"color: orange;\">&#9888;</span>";

        private static int Main(string[] args)
        {
            ReadXMLFileToEmails();

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
                    listOfChanges.Add("<li>");

                    string firstLetter = change.ToLower().Remove(1);
                    string removeStrating4Letters = change.Remove(0, 4);

                    switch (firstLetter)
                    {
                        case "u":
                            listOfChanges.Add(removeStrating4Letters + AddHTMLSpaces(2) + modifiedIcon + "</li>");
                            break;
                        case "a":
                            listOfChanges.Add(removeStrating4Letters+ AddHTMLSpaces(2) + addedIcon + " </li>");
                            break;
                        case "d":
                            listOfChanges.Add("<strike>" + removeStrating4Letters + "</strike>" + AddHTMLSpaces(2) + deletedIcon + "</li>");
                            break;
                        default:
                            break;
                    }
                }
                WriteDebuggLog("change " + loopcounter + ": " + change);

                loopcounter++;
            }

            listOfChanges.Add("</ul>");

            string newOutput = ListToString(listOfChanges);
            WriteDebuggLog("ListToString: " + newOutput);

            string changeFirst = changeList[0].Remove(0, 4);

            WriteDebuggLog("First change: " + changeFirst);

            //Get the name of the repository from the first argument, which is the repo path.
            repositoryName = args[0].ToString().Substring(args[0].LastIndexOf(@"\") + 1);

            int changeFirstSlash = changeFirst.IndexOf("/");
            if (changeFirstSlash > 0)
            {
                repositoryName += " "+changeFirst.Substring(0, changeFirstSlash);
            }

            WriteDebuggLog("Repository Name: " + repositoryName);

            //Copy template file before executing
            string sourcePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            string emailTemplateName = "svnnotification.html";

            File.Copy(Path.Combine(sourcePath, emailTemplateName), Path.Combine(CommonFolderPath(), emailTemplateName), true);

            //Get the email template and fill it in. This template can be anywhere, and can be a .HTML file
            //for more control over the structure.
            string emailTemplatePath = Path.Combine(CommonFolderPath(), emailTemplateName);


            //string emailTemplate = string.Format(File.ReadAllText(emailTemplatePath), author, message, changed);
            string emailTemplate = string.Format(File.ReadAllText(emailTemplatePath), author, message, newOutput);

            //Construct the email that will be sent. You can use the .IsBodyHtml property if you are
            //using an HTML template.
            string subject = string.Format("[SVN Notification] Repo: {0} - Rev. {1}",repositoryName, args[1]);

            ReadXMLFileToEmails();
            MailMessage mm = new MailMessage("mossadmin240@powersoft19.com", "msaddique@powersoft19.com")
            {
                IsBodyHtml = true,
                Body = emailTemplate,
                Subject = subject
            };

            //Define your mail client. I am using Gmail here as the SMTP server, but you could
            //use IIS or Amazon SES or whatever you want.
            SmtpClient mailClient = new SmtpClient("mail2.powersoft19.com")
            {
                Port = 25
            };

            string fileName = Path.Combine(CommonFolderPath(), "pass.txt");
            //string pass = EncodePasswordToBase64("");

            //File.WriteAllText(fileName, pass);

            //mailClient.Credentials = new System.Net.NetworkCredential("msaddique", DecodeFrom64(File.ReadAllText(fileName)));
            mailClient.EnableSsl = true;

            mailClient.Send(mm);

            mm.Dispose();
            mailClient.Dispose();
            WriteDebuggLog("Reach at the end of Code" + Environment.NewLine + "===================================================================");
            return 0;
        }

        private static List<string> ReadXMLFileToEmails(string pathXMLFile)
        {
            List<string> listOfEmails = new List<string>();
            // Start with XmlReader object  
            //here, we try to setup Stream between the XML file nad xmlReader  
            using (XmlReader reader = XmlReader.Create(pathXMLFile))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        //return only when you have START tag  
                        switch (reader.Name.ToString())
                        {
                            case "Name":
                                Console.WriteLine("Name of the Element is : " + reader.ReadString());
                                break;
                            case "Location":
                                Console.WriteLine("Your Location is : " + reader.ReadString());
                                break;
                        }
                    }
                    Console.WriteLine("");
                }
            }
            return listOfEmails;
        }

        private static string AddHTMLSpaces(int count)
        {
            string fullString = string.Empty;
            for (int i = 0; i < count; i++)
            {
                fullString += "&nbsp;";
            }
            return fullString;
        }
        private static string AddHTMLBreakes(int count)
        {
            string fullString = string.Empty;
            for (int i = 0; i < count; i++)
            {
                fullString += "<br>";
            }
            return fullString;
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
            string appPath = CommonFolderPath();

            string fileName = DateTime.Now.ToString("yyyyMMdd");

            string timeStamp = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
            File.AppendAllText(appPath + fileName + ".txt", timeStamp + ": " + content + Environment.NewLine);
        }

        private static string CommonFolderPath()
        {
            string commonApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData, Environment.SpecialFolderOption.None);

            string appPath = Path.Combine(commonApplicationData, "VisualSVNHook\\");

            if (!Directory.Exists(appPath))
            {
                Directory.CreateDirectory(appPath);
            }

            return appPath;
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
        //this function Convert to Encord your Password 
        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        } //this function Convert to Decord your Password
        public static string DecodeFrom64(string encodedData)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }
    }
}
