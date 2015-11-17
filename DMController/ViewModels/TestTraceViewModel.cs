using DMController.Common;
using DMController.Models;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DMController.ViewModels
{
    class TestTraceViewModel : ViewModelBase
    {
        private ICommand _RunCommand;
        //private ICommand _StopAllCommand;
        private bool _IsEnableRun = true;

        //Thread thread = new Thread(TestTraceModel.Start);

        public bool IsEnableRun
        {
            get
            {
                return _IsEnableRun;
            }
            set
            {
                _IsEnableRun = value;
                NotifyPropertyChanged("IsEnableRun");
            }
        }

        public ICommand RunCommand
        {
            get
            {
                if (_RunCommand == null)
                {
                    _RunCommand = new RelayCommand<Int32>(
                        param => (param > 0),
                        param => this.CallTestTrace(param)
                        );
                }
                return _RunCommand;
            }
        }

        //public ICommand StopAllCommand
        //{
        //    get
        //    {
        //        if (_StopAllCommand == null)
        //        {
        //            _StopAllCommand = new RelayCommand<bool>(
        //                param => (param = false),
        //                param => this.StopAllTestTrace()
        //                );
        //        }
        //        return _StopAllCommand;
        //    }
        //}

        private void CallTestTrace(object obj)
        {
            // Check have pathToDmExe and pathToLocationSaveLog
            //if ((false == TestTraceModel.DRIVE_MASTER_EXE_PATH.Contains(".exe")) || (TestTraceModel.LOCATION_LOG_RESULT_PATH == string.Empty))
            //{
            //    MessageBox.Show("Please Choice DriveMaster.exe or Location Path Save Log", "Info", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
            //    return;
            //}
            //thread.Start();
            TestTraceModel.Start();
            IsEnableRun = false;
        }

        //private void StopAllTestTrace()
        //{
        //    thread.Abort();
        //}
    }
}
