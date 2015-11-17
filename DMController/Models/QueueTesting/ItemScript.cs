using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMController.Models.ConfigurationInfo;

namespace DMController.Models.QueueTesting
{
    class ItemScript
    {
        public string ScriptName { get; set; }
        public int IDScript { get; set; }
        public int IDConfig { get; set; }
        public Configuration Configuration { get; set; }

        public ItemScript() { Configuration = new Configuration(); }

        public ItemScript(string iScriptName, int iIDScript, int iIDConfig)
        {
            ScriptName = iScriptName;
            IDScript = iIDScript;
            IDConfig = iIDConfig;
            Configuration = new Configuration();
        }
    }
}
