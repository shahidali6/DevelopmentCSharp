using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace SVNHookGenerator
{
    class Program
    {

        static void Main(string[] args)
        {
            string xMLFileName = "Settings.xml";
            string postHookFilename = "post-commit.cmd";
            string csvFilename = "AccessReport.csv";

            ApplicationSettings applicationSettings = new ApplicationSettings();
            applicationSettings.Settingsfile = xMLFileName; 
            applicationSettings.PostHookfileName = postHookFilename;            
            applicationSettings.AccessCSVFileName = csvFilename;
            applicationSettings.AplicationDirectory = Directory.GetCurrentDirectory();
            applicationSettings.RepositoriesPath = String.Empty;

            SVNHook hook = new SVNHook();
            ReferenceTaswar referenceTaswar = new ReferenceTaswar();

            var structureResult =  referenceTaswar.Function(Path.Combine(applicationSettings.AplicationDirectory, csvFilename));

            bool status = hook.WriteHookFile(structureResult, appPath + xMLFileName, appPath + postHookFilename);

            Console.WriteLine("Completed Hook Generation!");
            Thread.Sleep(1000);
        }
    }
}