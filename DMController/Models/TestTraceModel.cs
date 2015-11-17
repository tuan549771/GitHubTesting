using DMController.Models.OrganizeNode;
using DMController.Models.QueueTesting;
using ErrorHandling;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;

namespace DMController.Models
{
    class TestTraceModel
    {
        public static string DRIVE_MASTER_EXE_PATH = string.Empty;
        public static string LOCATION_LOG_RESULT_PATH = string.Empty;
        private const string SCRIPT_NAME_GENERATE = "AutoMultilScript.srt";
        private const string STR_ALL_NETWORK_ARE_BUSY_TITLE = "Connection refused:";
        private const string STR_NETWORK_ARE_FAIL_TITLE = "Connection failed:";
        private const string STR_WRONG_HBA_CONFIGURATION_TITLE = "DriveMaster HBA Configuration";
        private const string STR_WRONG_PORT_TITLE = "Please check if the DEVICE on the SELECTED port works well!";
        private const string STR_DRIVE_MASTER_TITLE = "DriveMaster";
        private const string STR_DMTEST_PATH = @"C:\DMTest";
        private const string STR_DMTEST_SVN_PATH = @"C:\DMTestSVN";
        private const string STR_LOG_MESSAGE = "Moved file excels and logs";
        private const int WAIT_FOR_GET_LICENSE_IN_MILISECONDS = 300;//000; // wait for 300s = 5 mins
        private static int RESULT_INT = 0;

        public static void Start()
        {
            try
            {
                LogTextInformation.LogFileWrite(string.Format("[INFO] Begin Start Method"));
                GenerateScriptUlink.Generate();
                XMLParsers.GenerateConfiguaration(Queue.GetListItemScript());


                string pathToScriptLocation = OrganizeNodeSystem.GetFolderContainScripts();
                string currentDirectory = Directory.GetCurrentDirectory();

                string pathToScriptAuto = string.Format(@"{0}\{1}", currentDirectory, SCRIPT_NAME_GENERATE);

                RunDmMaster(DRIVE_MASTER_EXE_PATH, pathToScriptAuto);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                string logger = LogTextInformation.CreateErrorMessage(ex);
                LogTextInformation.LogFileWrite(string.Format("[DEBUG]: Exception TestTrace Model at Start function : {0} ", logger));
            }
        }


        public static void RunDmMaster(string iPathToDriveMasterExe, string iPathToScript)
        {
            LogTextInformation.LogFileWrite(string.Format("[INFO] Starting test with {0}", Path.GetFileName(iPathToScript)));
            // Create a ProcessStartInfo
            ProcessStartInfo startinfo = new ProcessStartInfo();
            startinfo.UseShellExecute = false;
            startinfo.CreateNoWindow = true;
            startinfo.WindowStyle = ProcessWindowStyle.Hidden;
            startinfo.FileName = iPathToDriveMasterExe;
            startinfo.RedirectStandardOutput = true;
            string arguments = string.Format(@"/s:{0}", iPathToScript);
            startinfo.Arguments = arguments;

            // Call command prompt to run a script of Drive Master       
            Process process = new Process();
            process.StartInfo = startinfo;
            process.Start();


            Thread.Sleep(WAIT_FOR_GET_LICENSE_IN_MILISECONDS);
            LogTextInformation.LogFileWrite(string.Format("[INFO] Waited For Get License In Miliseconds"));

            // Check Ulink 
            Process[] processlist = Process.GetProcesses();

            foreach (Process theprocess in processlist)
            {
                if (theprocess.MainWindowTitle == STR_ALL_NETWORK_ARE_BUSY_TITLE)
                {
                    theprocess.Kill();
                    RESULT_INT = 1;
                    LogTextInformation.LogFileWrite(string.Format("[INFO] Connection refused: All Network are busy"));
                    return;
                }
                if (theprocess.MainWindowTitle == STR_NETWORK_ARE_FAIL_TITLE)
                {
                    theprocess.Kill();
                    RESULT_INT = 2;
                    LogTextInformation.LogFileWrite(string.Format("[INFO] Connection failed: Network are busy"));
                    return;
                }
                if (theprocess.MainWindowTitle.Contains(STR_WRONG_HBA_CONFIGURATION_TITLE))
                {
                    theprocess.Kill();
                    RESULT_INT = 3;
                    LogTextInformation.LogFileWrite(string.Format("[INFO] DriveMaster HBA Configuration: Wrong HBA  Configuration"));
                    return;
                }
                if (theprocess.MainWindowTitle.Contains(STR_WRONG_PORT_TITLE))
                {
                    theprocess.Kill();
                    RESULT_INT = 4;
                    LogTextInformation.LogFileWrite(string.Format("[INFO] DriveMaster Port Configuration: Wrong Port"));
                    return;
                }
            }

            // Check if DM processing is done or yet
            bool isFinished = false;
            while (!isFinished)
            {
                Process[] processes = Process.GetProcesses();
                if (processes.Any(x => x.MainWindowTitle.Contains(STR_ALL_NETWORK_ARE_BUSY_TITLE)))
                {
                    processes.FirstOrDefault(x => x.MainWindowTitle.Contains(STR_ALL_NETWORK_ARE_BUSY_TITLE)).Kill();
                }
                if (!processes.Any(x => x.MainWindowTitle.Contains(STR_DRIVE_MASTER_TITLE)))
                {
                    isFinished = true;
                    RESULT_INT = 0;
                }
            }


            ///
            if (0 != RESULT_INT)
            {
                Thread.Sleep(600000);
                Start();
            }
            
        }


    }
}
