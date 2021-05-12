using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp4RemoveNumbersFromString
{
    class Program
    {
        static void Main(string[] args)
        {
            //Variables
            string filePathPDFs = "SplittedPDFs";
            string fileNamePath = "StringToConverted.txt";
            string fileNamePathNew = "StringToConvertedNew.txt";
            string splitter = " - ";

            //Lists
            List<string> listOfPDFFilesOrignalName = new List<string>();
            List<string> listOfPDFfilesModifiedName = new List<string>();

            var executingPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            var sortedFiles = new DirectoryInfo(Path.Combine(executingPath, filePathPDFs)).GetFiles()
                                                  .OrderBy(f => f.LastWriteTime)
                                                  .ToList();

            string fileNameToRemove = sortedFiles[0].Name.ToString().Replace("1.pdf", string.Empty);          

            var readTextFile = File.ReadAllLines(Path.Combine(executingPath, fileNamePath));
            string requestedFileName = string.Empty;
            string requestedFileNameClient = string.Empty;
            string requestedFileNameNumber = string.Empty;
            string requestedFileNameDate = string.Empty;
            for (int i = 0; i < readTextFile.Length; i++)
            {
                int logic = (i % 3);
                switch (logic)
                {
                    case 0:
                        requestedFileNameClient = RemoveDigitsAndInavalid(readTextFile[i]);
                        break;
                    case 1:
                        requestedFileNameNumber = readTextFile[i];
                        break;
                    case 2:
                        var splitterDate = readTextFile[i].Split('/');
                        if (splitterDate.Length > 2)
                        {
                            splitterDate = splitterDate.Skip(1).ToArray();
                        }
                        requestedFileNameDate = string.Join(".", splitterDate);
                        break;
                    default:
                        break;
                }
                if ((i % 3) == 2)
                {
                    listOfPDFfilesModifiedName.Add(requestedFileNameDate + splitter + requestedFileNameNumber + splitter + requestedFileNameClient);
                }
            }

            File.WriteAllLines(fileNamePathNew, listOfPDFfilesModifiedName.ToArray(), Encoding.UTF8);

            string newDirectoryPath = Path.Combine(executingPath, "PDFFilesNewNames");
            if (!Directory.Exists(newDirectoryPath))
            {
                Directory.CreateDirectory(newDirectoryPath);
            }            

            for (int i = 0; i < sortedFiles.Count; i++)
            {
                string destinationFileName = listOfPDFfilesModifiedName[i]+".pdf";
                string checkFilePath = Path.Combine(newDirectoryPath, destinationFileName);

                if (File.Exists(checkFilePath))
                {
                    destinationFileName = "_"+listOfPDFfilesModifiedName[i] + ".pdf";
                }

                File.Copy(sortedFiles[i].FullName, Path.Combine(newDirectoryPath, destinationFileName.Trim()));
            }
        }
        public static string RemoveDigitsAndInavalid(string key)
        {
            var tempp = Path.GetInvalidFileNameChars().ToList();
            tempp.Add('|');
            key = Regex.Replace(key, @"\d", string.Empty);
            foreach (var item in tempp)
            {
                key = key.Replace(item.ToString(), string.Empty);
            }
            return key;
        }
    }
}