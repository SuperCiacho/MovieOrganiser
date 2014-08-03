using System.Collections.ObjectModel;

namespace FolderPicker.Model
{
    public class Drive : Item
    {
        public Drive(string name, bool isReady)
        {
            Name = Path = name;
            IsReady = isReady;
            Children = new ObservableCollection<object>();
            DisplayIcon = "/FolderPicker;component/Pics/drive.png";
        }

        public bool IsReady { get; set; }
    }
}