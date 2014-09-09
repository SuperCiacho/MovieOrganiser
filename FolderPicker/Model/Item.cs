namespace YorgiControls.Model
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using GalaSoft.MvvmLight;

    public abstract class Item : ObservableObject
    {
        #region Fields

        private bool isSelected;
        private bool isExpanded;
        private string displayIcon;

        #endregion

        #region Properties

        public string Name { get; set; }

        public string Path { get; set; }

        public ObservableCollection<Item> Children { get; protected set; }

        public string DisplayIcon
        {
            get { return displayIcon; }
            set
            {
                displayIcon = value;
                RaisePropertyChanged("DisplayIcon");
            }
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (value == isSelected) return;
                isSelected = value;
                this.RaisePropertyChanged("IsSelected");
            }
        }

        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                if (value == isExpanded) return;
                isExpanded = value;
                GetDirectories(this);
                ChangeIcon(value);

                this.RaisePropertyChanged("IsExpanded");
            }
        }

        #endregion

        #region Methods

        private void GetDirectories(Item item)
        {
            if (item is Drive && !((Drive)item).IsReady) return;
            if (item.Children.Count == 0 || !string.IsNullOrEmpty(((Directory)item.Children[0]).Path)) return;
            item.Children.Clear();

            try
            {
                foreach (var dir in System.IO.Directory.EnumerateDirectories(item.Path).Where(dir => !new System.IO.DirectoryInfo(dir).Attributes.HasFlag(System.IO.FileAttributes.System | System.IO.FileAttributes.Hidden)))
                {
                    var directory = new Directory(dir);

                    try
                    {
                        if (System.IO.Directory.EnumerateDirectories(dir).Any())
                            directory.Children.Add(new Directory(string.Empty));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    item.Children.Add(directory);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        protected virtual void ChangeIcon(bool isItemExpanded) { }

        #endregion
    }
}