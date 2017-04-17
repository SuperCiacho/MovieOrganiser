namespace YorgiControls.Model
{
    internal class Drive : Item
    {
        private const string DriveReadyIcon = "Images/drive_ready.png";
        private const string DriveUnReadyIcon = "Images/drive_unready.png";

        public Drive(string name, bool isReady, System.IO.DriveType driveType)
        {
            this.Name = this.Path = name;
            this.IsReady = isReady;
            this.DriveType = driveType;
            this.Children = new System.Collections.ObjectModel.ObservableCollection<Item>();
            this.DisplayIcon = isReady ? DriveReadyIcon : DriveUnReadyIcon;
        }

        public Drive(string name, bool isReady) : this(name, isReady, System.IO.DriveType.Unknown) { }

        public bool IsReady { get; set; }

        public System.IO.DriveType DriveType { get; set; }
    }
}