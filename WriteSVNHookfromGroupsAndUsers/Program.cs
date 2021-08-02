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

namespace ConsoleApp4ReadGroups
{
    class Program
    {

        static void Main(string[] args)
        {
            string xMLFileName = "Settings.xml";
            string postHookFilename = "post-commit.cmd";
            string csvFilename = "AccessReport.csv";

            string roamingFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.None);
            string appPath = Path.Combine(roamingFolderPath, "VisualSVNHook\\");

            Hook hook = new Hook();
            ReferenceTaswar referenceTaswar = new ReferenceTaswar();

            var structureResult =  referenceTaswar.Function(@"F:\AccessReport.csv");

            bool status = hook.WriteHookFile(structureResult, appPath + xMLFileName, appPath + postHookFilename);

            Console.WriteLine("Completed Hook Generation!");
            Thread.Sleep(1000);
        }
    }
}