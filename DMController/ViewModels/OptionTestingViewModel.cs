using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMController.ViewModels
{
    class OptionTestingViewModel
    {
        private string _optionTitle;
        private string _optionName;
        private List<ModeTestingViewModel> _modeValues;
        private int _selectedModeDefaultValue;

        public string OptionTitle
        {
            get { return _optionTitle; }
            set { _optionTitle = value; }
        }

        public string OptionName
        {
            get { return _optionName; }
            set { _optionName = value; }
        }

        public List<ModeTestingViewModel> ModeValues
        {
            get { return _modeValues; }
            set { _modeValues = value; }
        }

        public int SelectedModeDefaultValue
        {
            get { return _selectedModeDefaultValue; }
            set { _selectedModeDefaultValue = value; }
        }

        public OptionTestingViewModel()
        {
            OptionTitle = string.Empty;
            OptionName = string.Empty;
            ModeValues = new List<ModeTestingViewModel>();
            SelectedModeDefaultValue = 0;
        }
    }
}
