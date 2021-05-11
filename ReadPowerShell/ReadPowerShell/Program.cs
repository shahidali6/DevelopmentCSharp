using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp4CommandPrompt
{
    class Program
    {
        static void Main(string[] args)
        {
            using (PowerShell powerShell = PowerShell.Create())
            {
                powerShell.Commands.Clear();
                //string commandndnd = "Get-Process";
                // Source functions.
                //powerShell.AddScript(EnumUnderScoreToHyphen(PowerShellCommands.Add_SvnAccessRule));
                //powerShell.AddScript("Get-Command -Module VisualSVN");
                powerShell.AddScript(@"C:\Program Files\VisualSVN Server\ShortcutStartup.ps1");
                var resutl = powerShell.Invoke();

                var commandReturn = EnumUnderScoreToHyphen(PowerShellCommands.Get_SvnAccessRule);
                powerShell.AddScript(commandReturn);
                //werShell.AddCommand(EnumUnderScoreToHyphen(PowerShellCommands.Get_SvnAccessRule));

                // invoke execution on the pipeline (collecting output)
                Collection<PSObject> PSOutput = powerShell.Invoke();

                if (PSOutput.Count == 0)
                {
                    Console.WriteLine("No Output Returned");
                    commandReturn = EnumUnderScoreToHyphen(PowerShellCommands.Get_Process);
                    powerShell.AddScript(commandReturn);
                    PSOutput = powerShell.Invoke();
                }
                string pathAndFile = Environment.CurrentDirectory + "\\outPut.txt";
                File.Delete(pathAndFile);
                File.AppendAllText(pathAndFile, DateTime.Now.ToString()+ ":  ===========================");
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

        private static string EnumUnderScoreToHyphen(PowerShellCommands command)
        {
            return command.ToString().Replace('_', '-');
        }

        enum PowerShellCommands
        {
            //VisualSVN Commands
            Upgrade_SvnRepository,
            Verify_SvnRepository,
            Add_SvnAccessRule,
            Add_SvnRepositoryHook,
            Backup_SvnRepository,
            Convert_SvnRepository,
            Disable_SvnJob,
            Enable_SvnJob,
            Get_SvnAccessRule,
            Get_SvnJob,
            Get_SvnRepository,
            Get_SvnRepositoryHook,
            Get_SvnRepositoryItem,
            Get_SvnRepositoryReplication,
            Get_SvnServerConfiguration,
            Import_SvnRepository,
            Measure_SvnRepository,
            New_SvnReplicationAuthentication,
            New_SvnRepository,
            New_SvnRepositoryItem,
            Remove_SvnAccessRule,
            Remove_SvnRepository,
            Remove_SvnRepositoryHook,
            Remove_SvnRepositoryItem,
            Rename_SvnRepository,
            Restore_SvnRepository,
            Resume_SvnRepository,
            Select_SvnAccessRule,
            Set_SvnAccessRule,
            Set_SvnRepository,
            Set_SvnRepositoryHook,
            Set_SvnRepositoryReplication,
            Set_SvnServerConfiguration,
            Start_SvnJob,
            Stop_SvnJob,
            Suspend_SvnRepository,
            Switch_SvnRepository,
            Sync_SvnRepository,
            Test_SvnRepository,
            Update_SvnRepository,

            //General Command
            Get_Process,
        }
    }
}