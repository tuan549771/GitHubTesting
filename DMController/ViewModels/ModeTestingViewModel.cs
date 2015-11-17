using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMController.ViewModels
{
    class ModeTestingViewModel
    {
        private string _modeValue;
        public string ModeValue
        {
            get { return _modeValue; }
            set
            {
                _modeValue = value;
            }
        }

        private string _modeValueAttribute;
        public string ModeValueAttribute
        {
            get { return _modeValueAttribute; }
            set
            {
                _modeValueAttribute = value;
            }
        }

        public ModeTestingViewModel(string iModeValue, string iModeValueAttribute)
        {
            ModeValue = iModeValue;
            ModeValueAttribute = iModeValueAttribute;
        }
    }
}
