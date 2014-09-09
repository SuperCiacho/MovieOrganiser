namespace YorgiControls.Model
{
    public class Drive : Item
    {
        private const string DriveReadyIcon = "/YorgiControls;component/Pics/drive_ready.png";
        private const string DriveUnReadyIcon = "/YorgiControls;component/Pics/drive_unready.png";

        public Drive(string name, bool isReady)
        {
            Name = Path = name;
            IsReady = isReady;
            Children = new System.Collections.ObjectModel.ObservableCollection<Item>();
            DisplayIcon = isReady ? DriveReadyIcon : DriveUnReadyIcon;
        }

        public bool IsReady { get; set; }
    }
}