using DMController.Common;
using DMController.Models;
using DMController.Views;
using System.Collections.ObjectModel;
using System.Linq;

namespace DMController.ViewModels
{
    class ConfigurationViewModel : ViewModelBase
    {
        private string _configurationName;
        private ObservableCollection<OptionTestingViewModel> _listOptionTesting;

        #region Property

        public string ConfigurationName
        {
            get
            {
                if (null == _configurationName)
                    _configurationName = string.Empty;
                return _configurationName;
            }
            set
            {
                _configurationName = value;
            }
        }

        public ObservableCollection<OptionTestingViewModel> ListOptionTesting
        {
            get
            {
                if (null == _listOptionTesting)
                    _listOptionTesting = new ObservableCollection<OptionTestingViewModel>();
                return _listOptionTesting;
            }
            set
            {
                _listOptionTesting = value;
                NotifyPropertyChanged("ListOptionTesting");
            }
        }

        #endregion

        #region Constructor

        public ConfigurationViewModel()
        {
        }

        #endregion
    }
}
