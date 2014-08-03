using System.Collections.ObjectModel;

namespace FolderPicker.Model
{
    public class Directory : Item
    {
        public Directory(string fullPath)
            : this(fullPath, System.IO.Path.GetFileName(fullPath))
        {
        }

        public Directory(string fullPath, string name)
        {
            Path = fullPath;
            Name = name;
            Children = new ObservableCollection<object>();
            DisplayIcon = "/FolderPicker;component/Pics/folder_close.png";
        }
    }
}