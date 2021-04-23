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
                //powerShell.AddScript("Import-Module AppVPkgConverter");
                //powerShell.AddScript("Get-Command -Module AppVPkgConverter");
                //powerShell.AddScript("ConvertFrom-AppvLegacyPackage -DestinationPath "C:\Temp" -SourcePath "C:\Temp2"");

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

            //using (Process process = new Process())
            //{
            //    process.StartInfo.FileName = "cmd.exe";
            //    process.StartInfo.Arguments = "dir";
            //    process.StartInfo.UseShellExecute = false;
            //    process.StartInfo.RedirectStandardOutput = true;
            //    process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            //    process.Start();

            //    // Synchronously read the standard output of the spawned process.
            //    StreamReader reader = process.StandardOutput;
            //    string output = reader.ReadToEnd();

            //    // Write the redirected output to this application's window.
            //    Console.WriteLine(output);

            //    File.WriteAllText(@"d:\testttt.txt", output);

            //    process.WaitForExit();
            //}

            //Console.WriteLine("\n\nPress any key to exit.");
            //Console.ReadLine();

            //// Start the child process.
            //Process p = new Process();
            //// Redirect the output stream of the child process.
            //p.StartInfo.UseShellExecute = false;
            //p.StartInfo.RedirectStandardOutput = true;
            //p.StartInfo.FileName = "cmd.exe";
            //p.Start();
            //// Do not wait for the child process to exit before
            //// reading to the end of its redirected stream.
            //// p.WaitForExit();
            //// Read the output stream first and then wait.
            //string output = p.StandardOutput.ReadToEnd();
            //p.WaitForExit();

            //string strCommand = "dir";

            ////Create process
            //System.Diagnostics.Process pProcess = new System.Diagnostics.Process();

            ////strCommand is path and file name of command to run
            //pProcess.StartInfo.FileName = strCommand;

            ////strCommandParameters are parameters to pass to program
            //pProcess.StartInfo.Arguments = strCommandParameters;

            //pProcess.StartInfo.UseShellExecute = false;

            ////Set output of program to be written to process output stream
            //pProcess.StartInfo.RedirectStandardOutput = true;

            ////Optional
            //pProcess.StartInfo.WorkingDirectory = strWorkingDirectory;

            ////Start the process
            //pProcess.Start();

            ////Get program output
            //string strOutput = pProcess.StandardOutput.ReadToEnd();

            ////Wait for process to finish
            //pProcess.WaitForExit();

            //System.Diagnostics.Process process = new System.Diagnostics.Process();
            //System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            //startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            //startInfo.FileName = "cmd.exe";
            //startInfo.Arguments = "/C copy /b Image1.jpg + Archive.rar Image2.jpg";
            //process.StartInfo = startInfo;
            //process.Start();
            //startInfo.Arguments = "dir";
        }
    }
}
