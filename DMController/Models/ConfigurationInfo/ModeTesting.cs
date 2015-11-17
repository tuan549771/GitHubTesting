using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMController.Models.ConfigurationInfo
{
    class ModeTesting
    {
        public string ModeValue { get; set; }
        public string ModeValueAttribute { get; set; }

        public ModeTesting(string iModeValue, string iModeValueAttribute)
        {
            ModeValue = iModeValue;
            ModeValueAttribute = iModeValueAttribute;
        }
    }
}
