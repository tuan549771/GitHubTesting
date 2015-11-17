using DMController.Models;
using DMController.ViewModels;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

namespace DMController.Views
{
    /// <summary>
    /// Interaction logic for TestTraceControl.xaml
    /// </summary>
    public partial class TestTraceControl : UserControl
    {
        public TestTraceControl()
        {
                InitializeComponent();
                tvMenuScripts.DataContext = OrgTreeViewModel.Instance();
        }

        private void Button_OpenExeFile(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            OpenFileDialog openFileDriveMasterExe = new OpenFileDialog();

            // Set filter for file extension and default file extension
            openFileDriveMasterExe.DefaultExt = ".exe";
            openFileDriveMasterExe.Filter = "Applications (*.exe)|*.exe";

            // Display OpenFileDialog by calling ShowDialog method
            if (true == openFileDriveMasterExe.ShowDialog())
            {
                txtPathExeDriveMaster.Content = openFileDriveMasterExe.FileName;
                TestTraceModel.DRIVE_MASTER_EXE_PATH = openFileDriveMasterExe.FileName;
            }

        }

        private void Button_ResultPath(object sender, RoutedEventArgs e)
        {
            using (System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog())
            {
                dlg.Description = "Select folder store log result";
                dlg.SelectedPath = "~/Desktop";
                dlg.ShowNewFolderButton = true;
                System.Windows.Forms.DialogResult result = dlg.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    txtPathResult.Content = dlg.SelectedPath;
                    TestTraceModel.LOCATION_LOG_RESULT_PATH = dlg.SelectedPath;
                }
            }
        }
    }
}
