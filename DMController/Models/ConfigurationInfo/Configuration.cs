using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMController.Models.ConfigurationInfo
{
    class Configuration
    {
        private string _configurationName;
        public string ConfigurationName
        {
            get { return _configurationName; }
            set { _configurationName = value; }
        }
        public List<OptionTesting> ListOptionTesting { get; set; }

        public Configuration()
        {
            this.ConfigurationName = string.Empty;
            this.ListOptionTesting = new List<OptionTesting>();
        }
    }
}
