using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4CommandPrompt
{
    class Program
    {
        static void Main(string[] args)
        {
            using (PowerShell powerShell = PowerShell.Create())
            {
                // Source functions.
                powerShell.AddScript("Get-SvnAccessRule");
                //powerShell.AddScript("help");

                // invoke execution on the pipeline (collecting output)
                Collection<PSObject> PSOutput = powerShell.Invoke();

                string path = Environment.CurrentDirectory;
                File.AppendAllText(Environment.CurrentDirectory + "\\outPut.txt", DateTime.Now.ToString()+ ":  ===========================");
                // loop through each output object item
                foreach (PSObject outputItem in PSOutput)
                {
                    // if null object was dumped to the pipeline during the script then a null object may be present here
                    if (outputItem != null)
                    {
                        File.AppendAllText(Environment.CurrentDirectory + "\\outPut.txt", outputItem.BaseObject.ToString()+Environment.NewLine);
                        Console.WriteLine($"Output line: [{outputItem}]");
                    }
                }

                // check the other output streams (for example, the error stream)
                if (powerShell.Streams.Error.Count > 0)
                {
                    // error records were written to the error stream.
                    // Do something with the error
                }
            }
        }
    }
}