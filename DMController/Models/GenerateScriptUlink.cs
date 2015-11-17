using DMController.Models.OrganizeNode;
using DMController.Models.QueueTesting;
using ErrorHandling;
using System;
using System.IO;

namespace DMController.Models
{
    class GenerateScriptUlink
    {
        private const string SCRIPT_NAME_GENERATE = "AutoMultilScript.srt";
        private const string CALL_COMMAND_ULINK = "CALL";
        private const string RUN_APP_COMMAND_ULINK = "RUNAPP";
        private const string LOG_SAVE_COMMAND_ULINK = "LOG";
        private const string WINDOW_CONFIG_EXE = "WindowHandleConsole.exe";
        private const string WAIT_COMMAND_ULINK = "WAIT";
        private const int APP_TIMEOUT_ULINK = 10; // Wait for 10s
        private const string VALUE_RETURN_APP_ULINK = "v0";

        public static void Generate()
        {
            LogTextInformation.LogFileWrite(string.Format("[INFO] Begin Generate Method: Generate A Ulink Script"));
            string pathToScriptLocation = OrganizeNodeSystem.GetFolderContainScripts();
            string currentDirectory = Directory.GetCurrentDirectory();

            File.Delete(string.Format(@"{0}", SCRIPT_NAME_GENERATE));
            foreach (ItemScript itemScript in Queue.GetListItemScript())
            {
                // Absolute Path App
                string absolutePathToAppWinHandle = string.Format(@"{0}\{1}", currentDirectory, WINDOW_CONFIG_EXE);

                // Absolute Path Script
                string relativePathScript = OrganizeNodeSystem.GetRalativePath(itemScript.IDScript);
                string absolutePathScript = string.Format(@"{0}\{1}\{2}", currentDirectory, pathToScriptLocation, relativePathScript);
            
                DateTime timeNow = DateTime.Now;
                string timeFormatString = string.Format("{0:yyyy_MM_dd__}{1:hh}{2}{3:mm}{4}{5:ss}{6}{7:tt}", timeNow, timeNow, "h", timeNow, "m", timeNow, "s", timeNow);


                string pathToLocationSaveLog = TestTraceModel.LOCATION_LOG_RESULT_PATH;
                string pathLogFile = string.Format(@"{0}\{1}_{2}.log", pathToLocationSaveLog, itemScript.ScriptName.Replace(".srt", ""), timeFormatString);

                string idConfigScript = itemScript.IDConfig.ToString();

                string pathSaveScriptAuto = string.Format("{0}\\{1}", currentDirectory, SCRIPT_NAME_GENERATE);

                using (StreamWriter writer = new StreamWriter(pathSaveScriptAuto, true))
                {
                    // Write down run app Window detect with config AppName, CommandLine, TimeOut, Var Return of Ulink
                    writer.WriteLine(string.Format("{0} \"{1}\", \"{2}\", {3}, {4}", RUN_APP_COMMAND_ULINK, absolutePathToAppWinHandle, idConfigScript, APP_TIMEOUT_ULINK, VALUE_RETURN_APP_ULINK));

                    // Write down call script run with config Call "filename"
                    writer.WriteLine(string.Format("{0} \"{1}\"", CALL_COMMAND_ULINK, absolutePathScript));

                    // Write down save log with config Log/O  “filename”
                    writer.WriteLine(string.Format("{0} \"{1}\"", LOG_SAVE_COMMAND_ULINK, pathLogFile));
                }
            }
        }
    }
}
