using DMController.Models;
using DMController.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
// using statements...
using MahApps.Metro.Controls;
namespace DMController.Views
{
    /// <summary>
    /// Interaction logic for ConfigurationWindow.xaml
    /// </summary>
    public partial class ConfigurationWindow : MetroWindow
    {
        ConfigurationViewModel _configurationVM;
        public ConfigurationWindow()
        {
            InitializeComponent();
        }

        public ConfigurationWindow(object obj)
        {
            InitializeComponent();
            lview.ItemsSource = (obj as ConfigurationViewModel).ListOptionTesting;
            _configurationVM = new ConfigurationViewModel()
            {
                ConfigurationName = (obj as ConfigurationViewModel).ConfigurationName,
                ListOptionTesting = lview.ItemsSource as ObservableCollection<OptionTestingViewModel>
            };
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        public object Answer
        {
            get { return _configurationVM; }
        }
    }
}
