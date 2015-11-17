using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMController.Models.ConfigurationInfo
{
    class OptionTesting
    {
        public string OptionTitle { get; set; }
        public string OptionName { get; set; }
        public List<ModeTesting> Modes { get; set; }
        public int SelectedModeDefaultValue { get; set; }

        public OptionTesting()
        {
            OptionTitle = string.Empty;
            OptionName = string.Empty;
            Modes = new List<ModeTesting>();
            SelectedModeDefaultValue = 0;
        }
    }
}
