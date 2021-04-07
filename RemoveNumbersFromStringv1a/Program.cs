using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp4RemoveNumbersFromString
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileNamePath = "..\\..\\StringToConverted.txt";
            string fileNamePathNew = "..\\..\\StringToConvertedNew.txt";

            var tempp = Path.GetInvalidFileNameChars();

            List<string> listOfFileNames = new List<string>();
            List<string> listOfFilterFileNames = new List<string>();
            List<string> listOfFilterFileNamesWithSpaces = new List<string>();
            var pathnale = Directory.GetCurrentDirectory();
            var result = File.ReadAllLines(fileNamePath).ToList();

            listOfFileNames = result;
            foreach (var item in listOfFileNames)
            {
                listOfFilterFileNames.Add(RemoveDigits(item));
            }

            foreach (var item in listOfFilterFileNames)
            {
                listOfFilterFileNamesWithSpaces.Add(item);
                listOfFilterFileNamesWithSpaces.Add(String.Empty);
                listOfFilterFileNamesWithSpaces.Add(String.Empty);
            }
            using (StreamWriter sw = new StreamWriter(fileNamePathNew, false, Encoding.UTF8))
            {
                foreach (var item in listOfFilterFileNames)
                {
                    sw.WriteLine(item);
                    sw.WriteLine(String.Empty);
                    sw.WriteLine(String.Empty);
                }
            }
        }
        public static string RemoveDigits(string key)
        {
            return Regex.Replace(key, @"\d", "");
        }
    }
}
