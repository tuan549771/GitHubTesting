using DMController.Common;
using DMController.Models;
using DMController.Models.ConfigurationInfo;
using DMController.Models.QueueTesting;
using DMController.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DMController.ViewModels
{
    class QueueViewModel : ViewModelBase
    {
        #region private field

        private ScriptViewModel _script;
        private ObservableCollection<ScriptViewModel> _queuevm;
        private ICommand _AddCommand;
        private ICommand _UpCommand;
        private ICommand _DownCommand;
        private ICommand _DeleteCommand;
        private ICommand _ConfigurationCommand;
        private Int32 _SelectedIndex;
        private Queue _queue;
        private List<ItemScript> _scripts;
        private ItemScript _scriptM;

        #endregion

        #region Constructor

        public QueueViewModel()
        {
            _scripts = new List<ItemScript>();
        }

        #endregion

        #region Property

        public Int32 SelectedIndex
        {
            get { return _SelectedIndex; }
            set
            {
                _SelectedIndex = value;
                NotifyPropertyChanged("SelectedIndex");
            }
        }

        public ScriptViewModel ScriptTest
        {
            get
            {
                return _script;
            }
            set
            {
                _script = value;
                NotifyPropertyChanged("ScriptTest");
            }
        }

        public ObservableCollection<ScriptViewModel> QueueScripts
        {
            get
            {
                if (null == _queuevm)
                    _queuevm = new ObservableCollection<ScriptViewModel>();
                return _queuevm;
            }
            set
            {
                _queuevm = value;
                NotifyPropertyChanged("QueueScripts");
            }
        }

        public ICommand AddCommand
        {
            get
            {
                if (_AddCommand == null)
                {
                    _AddCommand = new RelayCommand<OrgElementViewModel>(
                        param => (param != null) && (param.Name.Contains(".srt")),
                        param => this.Add(param)
                        );
                }
                return _AddCommand;
            }
        }

        public ICommand UpCommand
        {
            get
            {
                if (_UpCommand == null)
                {
                    _UpCommand = new RelayCommand<Int32>(
                       param => param > 0,
                       param => this.Up(param)
                        );
                }
                return _UpCommand;
            }
        }

        public ICommand DownCommand
        {
            get
            {
                if (_DownCommand == null)
                {
                    _DownCommand = new RelayCommand<Int32>(
                       param => param >= 0 && param < QueueScripts.Count - 1,
                       param => this.Down(param)
                        );
                }
                return _DownCommand;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = new RelayCommand<ScriptViewModel>(
                        param => param != null,
                        param => this.Delete(param)
                        );
                }
                return _DeleteCommand;
            }
        }

        public ICommand ConfigurationCommand
        {
            get
            {
                if (_ConfigurationCommand == null)
                {
                    _ConfigurationCommand = new RelayCommand<ScriptViewModel>(
                        param => param != null,
                        param => this.Configuration(param)
                        );
                }
                return _ConfigurationCommand;
            }
        }

        #endregion

        private int IDConfiguration = 0;

        private void Add(OrgElementViewModel obj)
        {
            ++IDConfiguration;

            Configuration config = new Configuration();
            XMLParsers parser = new XMLParsers();

            config = parser.ParseConfiguration(obj.Name);
            if ((0 < config.ListOptionTesting.Count) && (config.ConfigurationName.Contains(".srt")))
                _scripts.Add(new ItemScript(obj.Name, obj.ID, IDConfiguration) { Configuration = config });
            else
                MessageBox.Show(string.Format("{0} will be support at next versions", obj.Name));

            Queue queue = new Queue(_scripts);
            UpdateQueue(queue);
            SelectedIndex = QueueScripts.Count - 1;

        }

        private void Up(Int32 iSelectedIndex)
        {
            _scriptM = new ItemScript();
            _scriptM = _scripts[iSelectedIndex - 1]; _scripts[iSelectedIndex - 1] = _scripts[iSelectedIndex]; _scripts[iSelectedIndex] = _scriptM;
            Queue queue = new Queue(_scripts);
            UpdateQueue(queue);
            SelectedIndex = iSelectedIndex - 1;
        }


        private void Down(Int32 iSelectedIndex)
        {
            _scriptM = new ItemScript();
            _scriptM = _scripts[iSelectedIndex + 1]; _scripts[iSelectedIndex + 1] = _scripts[iSelectedIndex]; _scripts[iSelectedIndex] = _scriptM;
            Queue queue = new Queue(_scripts);
            UpdateQueue(queue);
            SelectedIndex = iSelectedIndex + 1;
        }


        private void Delete(ScriptViewModel iScript)
        {
            int selectedIndex = SelectedIndex - 1;
            foreach (ItemScript script in _scripts)
            {
                if (script.IDScript == iScript.IDScript)
                {
                    _scripts.Remove(script);
                    break;
                }
            }
            Queue queue = new Queue(_scripts);
            UpdateQueue(queue);
            SelectedIndex = selectedIndex;
            if (SelectedIndex == -1 && QueueScripts.Count > 0)
                SelectedIndex = 0;
        }


        private void UpdateQueue(Queue iQueue)
        {
            _queue = iQueue;
            QueueScripts.Clear();
            int index = 1;
            foreach (ItemScript script in _queue.QueueScripts)
            {
                QueueScripts.Add(new ScriptViewModel(script.ScriptName, script.IDScript, index, IDConfiguration) { ConfigurationVM = UploadListOptionToViewModel(script.Configuration) });
                ++index;
            }
        }


        private void Configuration(ScriptViewModel iScript)
        {
            Configuration config = new Configuration();
            XMLParsers parser = new XMLParsers();
            config = UpdateBackConfiguration(iScript.ConfigurationVM);
            if ((0 < config.ListOptionTesting.Count) && (config.ConfigurationName.Contains(".srt")))
            {
                ConfigurationWindow window = new ConfigurationWindow(UploadListOptionToViewModel(config));
                if (true == window.ShowDialog())
                {
                    int index = -1;
                    foreach (ItemScript script in _scripts)
                    {
                        ++index;
                        if (script.IDScript == iScript.IDScript)
                        {
                            script.Configuration = UpdateBackConfiguration(window.Answer as ConfigurationViewModel);
                            Queue queue = new Queue(_scripts);
                            UpdateQueue(queue);
                            SelectedIndex = index;
                            break;
                        }
                    }
                }
            }
        }


        private ConfigurationViewModel UploadListOptionToViewModel(Configuration iConfig)
        {
            ConfigurationViewModel configurationVM = new ConfigurationViewModel();
            configurationVM.ConfigurationName = iConfig.ConfigurationName;
            foreach (OptionTesting option in iConfig.ListOptionTesting)
            {
                configurationVM.ListOptionTesting.Add(new OptionTestingViewModel()
                {
                    OptionTitle = option.OptionTitle,
                    OptionName = option.OptionName,
                    SelectedModeDefaultValue = option.SelectedModeDefaultValue,
                    ModeValues = (from mode in option.Modes
                                  where mode.ModeValue != null
                                  select new ModeTestingViewModel(mode.ModeValue, mode.ModeValueAttribute)).ToList<ModeTestingViewModel>(),
                }
                  );
            }
            return configurationVM;
        }

        private Configuration UpdateBackConfiguration(ConfigurationViewModel configurationVM)
        {
            Configuration temp = new Configuration();
            temp.ConfigurationName = configurationVM.ConfigurationName;
            foreach (OptionTestingViewModel option in configurationVM.ListOptionTesting)
            {
                temp.ListOptionTesting.Add(new OptionTesting()
                {
                    OptionTitle = option.OptionTitle,
                    OptionName = option.OptionName,
                    SelectedModeDefaultValue = option.SelectedModeDefaultValue,
                    Modes = (from mode in option.ModeValues
                             where mode.ModeValue != null
                             select new ModeTesting(mode.ModeValue, mode.ModeValueAttribute)).ToList<ModeTesting>(),
                }
                  );
            }
            return temp;
        }
    }
}
