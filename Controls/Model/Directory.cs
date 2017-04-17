namespace YorgiControls.Model
{
    internal class Directory : Item
    {
        private const string CloseFolderIcon = "Images/folder_close.png";
        private const string OpenFolderIcon = "Images/folder_open.png";

        public Directory(string fullPath) : this(fullPath, System.IO.Path.GetFileName(fullPath)) { }

        public Directory(string fullPath, string name)
        {
            this.Path = fullPath;
            this.Name = name;
            this.Children = new System.Collections.ObjectModel.ObservableCollection<Item>();
            this.DisplayIcon = CloseFolderIcon;
        }

        protected override void ChangeIcon(bool isItemExpanded)
        {
            if( Children.Count <= 0) return;
            DisplayIcon = isItemExpanded ? OpenFolderIcon: CloseFolderIcon;
        }
    }
}