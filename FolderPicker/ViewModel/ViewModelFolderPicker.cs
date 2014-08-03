using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using FolderPicker.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace FolderPicker.ViewModel
{
    public class ViewModelFolderPicker : ViewModelBase
    {
        #region Fields

        #region Commands

        private ICommand cancelCommand;
        private ICommand okCommand;

        #endregion

        #endregion

        public ViewModelFolderPicker()
        {
            DriveList = System.IO.DriveInfo.GetDrives().Select(driveInfo =>
            {
                var drive = new Drive(driveInfo.Name, driveInfo.IsReady);
                if (driveInfo.IsReady) drive.Children.Add(new Directory(string.Empty, string.Empty));
                return drive;
            });
        }

        #region Properties

        public string Path { get; set; }

        public IEnumerable<Drive> DriveList { get; private set; }

        #region Commands

        public ICommand OkCommand
        {
            get
            {
                return okCommand ?? (okCommand = new RelayCommand<object[]>(items =>
                {
                    Path = items[0] as string;
                    (items[1] as Window).DialogResult = true;
                    (items[1] as Window).Close();
                }, items => items != null && items.All(x => x != null)));
            }
        }

        public ICommand CancelCommand
        {
            get { return cancelCommand ?? (cancelCommand = new RelayCommand<Window>(w => w.Close())); }
        }

        #endregion

        #endregion

        public string Show(Window window)
        {
            window.DataContext = this;
            return window.ShowDialog() == true ? Path : null;
        }
    }
}