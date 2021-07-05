using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Linq;
using System.Text;
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

            List<string> listOfSystemGroups = new List<string>();
            List<GroupStructure> listOfGroupUsers = new List<GroupStructure>();
            List<VisualSVNHookStructure> listOfHookStructure = new List<VisualSVNHookStructure>();

            string roamingFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.None);
            string appPath = Path.Combine(roamingFolderPath, "VisualSVNHook\\");

            GroupUser groupUser = new GroupUser();
            Hook hook = new Hook();
            CSVFileReader cSVFileReader = new CSVFileReader();
            CSVToJSON cSVToJSON = new CSVToJSON();
            JSONwriterClass jSONwriterClass = new JSONwriterClass();
            ReferenceTaswar referenceTaswar = new ReferenceTaswar();

            //listOfSystemGroups = groupUser.ExtractNameOfGroups();

            //listOfGroupUsers = groupUser.ExtractUsersOfGroups(listOfSystemGroups);

            XMLFile xMLFile = new XMLFile();

            xMLFile.WriteXMLFile(listOfGroupUsers, appPath + xMLFileName );

            List<CSVFileStructure> listToReturn = new List<CSVFileStructure>();

            //listToReturn = cSVFileReader.ReadCSVFile(appPath + csvFilename);
            //listToReturn = cSVFileReader.ReadCSVFileNew(appPath + csvFilename);
            //listToReturn = cSVFileReader.ReadCSVFileNew1(appPath + csvFilename);

            var structureResult =  referenceTaswar.Function(@"Q:\temp\AccessReport.csv");

            //hook.WriteHookFile(listToReturn, appPath + xMLFileName, appPath + postHookFilename);
            hook.WriteHookFile(structureResult, appPath + xMLFileName, appPath + postHookFilename);

            //cSVToJSON.CSVToJSONFunc(appPath + csvFilename);
            //jSONwriterClass.JSONWriteFunc(appPath + csvFilename);
        }
    }
}