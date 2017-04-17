namespace YorgiControls
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Linq;
    using YorgiControls.Model;
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    /// <summary>
    ///     Interaction logic for FolderPicker.xaml
    /// </summary>
    public partial class FolderPicker
    {
        #region Fields

        private ICommand okButtonCommand;
        private ICommand cancelButtonCommand;

        #endregion

        #region Constructors

        public FolderPicker(string startLocation)
        {
            this.DriveList = new List<Drive>(System.IO.DriveInfo.GetDrives().Select(driveInfo =>
            {
                var drive = new Drive(driveInfo.Name, driveInfo.IsReady, driveInfo.DriveType);
                // If device is ready then fake directory is added to allow item expanding. 
                if (driveInfo.IsReady && IsDeviceTypeSupported(driveInfo.DriveType)) 
                    drive.Children.Add(new Directory(string.Empty, string.Empty));
                return drive;
            }));

            this.InitializeComponent();
            this.FolderTreeView.ItemsSource = this.DriveList;
            SelectCurrentPath(startLocation);

            this.OkButton.Command = this.OkButtonCommand;
            this.CancelButton.Command = this.CancelButtonCommand;
        }

        public FolderPicker() : this(null) { }

        #endregion

        #region Properties

        #region Commands

        public ICommand OkButtonCommand
        {
            get { return okButtonCommand ?? (this.okButtonCommand = new Utils.Command(OnOkButtonPressed, CanOkButtonBePressed)); }
        }

        public ICommand CancelButtonCommand
        {
            get
            {
                return this.cancelButtonCommand ?? (this.cancelButtonCommand = new Utils.Command(this.Close));
            }
        }

        #endregion

        public string SelectedPath { get; private set; }

        private List<Drive> DriveList { get; set; }

        #endregion

        #region Methods

        private void SelectCurrentPath(string startPath)
        {
            if (string.IsNullOrEmpty(startPath)) return;
            var sp = startPath.Split('\\');
            Item item = this.DriveList.First(x => x.Path.StartsWith(sp[0]));
            for (var i = 1; i < sp.Length; i++)
            {
                item.IsExpanded = true;
                var childItem = item.Children.FirstOrDefault(x => x.Name == sp[i]);
                if (childItem == null) break;
                item = childItem;
            }
            item.IsSelected = true;
        }

        private bool IsDeviceReady(string path)
        {
            try
            {
                if (string.IsNullOrEmpty(path)) throw new ArgumentException("Path argument is null or empty.");
                return this.DriveList.First(x => x.Name.Equals(System.IO.Path.GetPathRoot(path))).IsReady;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool IsDeviceTypeSupported(System.IO.DriveType driveType)
        {
            return driveType != System.IO.DriveType.CDRom;
        }

        private bool IsDeviceTypeSupported(string path)
        {
            var dt = this.DriveList.First(x => x.Name.Equals(System.IO.Path.GetPathRoot(path))).DriveType;
            return IsDeviceTypeSupported(dt);
        }

        private void OnOkButtonPressed()
        {
            SelectedPath = ((Item)this.FolderTreeView.SelectedItem).Path;
            this.DialogResult = true;
            this.Close();
        }

        private bool CanOkButtonBePressed()
        {
            var item = this.FolderTreeView.SelectedItem as Item;
            if (item != null)
            {
                return IsDeviceTypeSupported(item.Path)
                    && IsDeviceReady(item.Path)
                    && this.DriveList != null
                    && this.DriveList.All(x => x != null);                    
            }
            return false;
        }

        #endregion
    }
}