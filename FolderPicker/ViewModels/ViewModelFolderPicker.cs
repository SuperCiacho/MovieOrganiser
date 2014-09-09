using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using YorgiControls.Model;

namespace YorgiControls.ViewModels
{
    public class ViewModelFolderPicker : ViewModelBase
    {
        #region Fields

        #region Commands

        private ICommand cancelCommand;
        private ICommand okCommand;

        #endregion

        #endregion

        #region Constructors

        public ViewModelFolderPicker(string startLocation)
        {
            this.StartPath = startLocation;

            DriveList = new ObservableCollection<Drive>(System.IO.DriveInfo.GetDrives().Select(driveInfo =>
            {
                var drive = new Drive(driveInfo.Name, driveInfo.IsReady);
                if (driveInfo.IsReady) drive.Children.Add(new Directory(string.Empty, string.Empty));
                return drive;
            }));

            SelectCurrentPath();
        }

        public ViewModelFolderPicker() : this(null)
        {

        }

        #endregion

        #region Properties

        public string SelectedPath { get; private set; }

        public string StartPath { get; set; }

        public ObservableCollection<Drive> DriveList { get; private set; }

        #region Commands

        public ICommand OkCommand
        {
            get
            {
                return okCommand ?? (okCommand = new RelayCommand<object[]>(items =>
                {
                    SelectedPath = items[0] as string;
                    ((Window) items[1]).DialogResult = true;
                    ((Window) items[1]).Close();
                }, items => items != null && items.All(x => x != null) && IsDeviceReady(items[0] as string)));
            }
        }

        public ICommand CancelCommand
        {
            get { return cancelCommand ?? (cancelCommand = new RelayCommand<Window>(w => w.Close())); }
        }

        #endregion

        #endregion

        #region Methods

        public string Show(Window window)
        {
            if(window == null) throw new ArgumentNullException("window", "Window argument is null.");
            window.DataContext = this;
            return window.ShowDialog() == true ? SelectedPath : null;
        }

        private void SelectCurrentPath()
        {
            if (string.IsNullOrEmpty(StartPath)) return;
            var sp = StartPath.Split('\\');
            Item item = this.DriveList.First(x => x.Path.StartsWith(sp[0]));
            for (var i = 1; i < sp.Length; i++)
            {
                item.IsExpanded = true;
                item = item.Children.First(x => x.Name == sp[i]);
            }
            item.IsSelected = true;
        }

        private bool IsDeviceReady(string path)
        {
            if(string.IsNullOrEmpty(path)) throw new ArgumentException("Path argument is null or empty.");
            return DriveList.First(x => x.Name.Equals(path.Substring(0, 3))).IsReady;
        }

        #endregion
    }
}