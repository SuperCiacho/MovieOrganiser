using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using YorgiControls.Model;
using YorgiControls.Utils;

namespace YorgiControls
{
    /// <summary>
    /// Interaction logic for YearCalendar.xaml
    /// </summary>
    public partial class YearCalendar : UserControl, INotifyPropertyChanged
    {
        #region Fields
        private int minYear = 1900;
        private int maxYear = 2085;
        private int page = 1;
        private int pageCount;
        private int elementsCount = 12;

        private IEnumerable<object> dateItems;
        private IList<Decade> DecadeItems { get; set; }
        private bool isYearSelectionMode;

        private ICommand leftArrowCommand;
        private ICommand rightArrowCommand;
        private ICommand itemSelectedCommand;
        private ICommand headerClickCommand;


        #endregion

        public static readonly RoutedEvent ItemSelectedEvent = EventManager.RegisterRoutedEvent("ItemSelected", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(YearCalendar));

        public event RoutedEventHandler ItemSelected
        {
            add { AddHandler(ItemSelectedEvent, value); }
            remove { RemoveHandler(ItemSelectedEvent, value); }
        }

        public YearCalendar()
        {
            this.InitializeComponent();

            this.ProduceDecadePeriods();
            this.DateItems = this.DecadeItems;
            this.DateListView.SetBinding(
                ListView.ItemsSourceProperty,
                new Binding("DateItems") { Source = this });

            this.PART_HeaderLeftButton.Command = this.LeftArrowCommand;
            this.PART_HeaderRightButton.Command = this.RightArrowCommand;
            this.PART_HeaderTitle.InputBindings.Add(
                new MouseBinding(this.HeaderClickCommand, new MouseGesture(MouseAction.LeftClick)));
            this.PART_HeaderTitle.Content = "Select decade...";
        }

        #region Properties

        #region Dependency Properties

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(YearCalendar),
            new FrameworkPropertyMetadata(string.Empty) { BindsTwoWayByDefault = true });
        public static readonly DependencyProperty SelectedYearProperty =
            DependencyProperty.Register(
            "SelectedYear", 
            typeof(int), 
            typeof(YearCalendar), 
            new FrameworkPropertyMetadata(default(int)) { BindsTwoWayByDefault = true });
        public static readonly DependencyProperty MaxYearProperty =
            DependencyProperty.Register(
            "MaxYear", 
            typeof(int), 
            typeof(YearCalendar), 
            new FrameworkPropertyMetadata(default(int)) { BindsTwoWayByDefault = true });
        public static readonly DependencyProperty MinYearProperty = 
            DependencyProperty.Register(
            "MinYear", 
            typeof(int), 
            typeof(YearCalendar), 
            new FrameworkPropertyMetadata(default(int)) { BindsTwoWayByDefault = true });

        public int MaxYear
        {
            get { return (int)GetValue(MaxYearProperty); }
            set { SetValue(MaxYearProperty, value); }
        }

        public int MinYear
        {
            get { return (int)GetValue(MinYearProperty); }
            set { SetValue(MinYearProperty, value); }
        }

        public int SelectedYear
        {
            get
            {
                return (int)GetValue(SelectedYearProperty);
            }
            set
            {
                SetValue(SelectedYearProperty, value);
            }
        }

        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        #endregion

        #region Commands
        public ICommand LeftArrowCommand
        {
            get
            {
                return leftArrowCommand ??
                       (this.leftArrowCommand = new Command(() => OnHeaderArrowClick(0),
                       () => isYearSelectionMode || page > 1));
            }
        }
        public ICommand RightArrowCommand
        {
            get
            {
                return rightArrowCommand ?? (this.rightArrowCommand = new Command(() => OnHeaderArrowClick(1),
                    () => isYearSelectionMode || this.page < this.pageCount));
            }
        }
        public ICommand ItemSelectedCommand
        {
            get
            {
                return itemSelectedCommand ?? (this.itemSelectedCommand = new Command<object>(OnDateItemSelection));
            }
        }

        public ICommand HeaderClickCommand
        {
            get
            {
                return headerClickCommand ?? (this.headerClickCommand = new Command(OnHeaderClick, () => this.isYearSelectionMode));
            }
        }

        #endregion

        public int ElementsCount
        {
            get { return elementsCount; }
            set { elementsCount = value; }
        }

        public int Page
        {
            get { return this.page; }
            set
            {
                if (this.page == value) return;
                this.page = value;

                this.OnPropertyChanged("DateItems");
            }
        }

        public IEnumerable<object> DateItems
        {
            get
            {
                if (this.isYearSelectionMode) return dateItems;
                return dateItems.Skip(elementsCount * (page - 1)).Take(elementsCount);
            }
            set
            {
                if (this.dateItems == value) return;
                this.dateItems = value;
                this.OnPropertyChanged("DateItems");
            }
        }

        #endregion

        #region Methods

        private void ProduceDecadePeriods()
        {
            var period = maxYear - minYear;
            var decades = (period + 10 - 1) / 10;
            this.pageCount = (decades + elementsCount - 1) / elementsCount;

            this.DecadeItems = new List<Decade>();

            for (var year = minYear; year < maxYear; year += 10)
            {
                var endYear = year + 9;
                this.DecadeItems.Add(new Decade()
                {
                    FromYear = year,
                    ToYear = endYear > maxYear ? maxYear : endYear
                });
            }
        }

        private void ProduceYearItems(Decade param)
        {
            this.isYearSelectionMode = true;
            var yearsItems = new List<object>();
            for (var year = param.FromYear - 1; year <= param.ToYear + 1; year++)
            {
                yearsItems.Add(year);
            }
            this.DateItems = yearsItems;
        }

        private void ModifyYearItems(bool backward)
        {
            var year = (int)(backward ? DateItems.First() : DateItems.Last());
            var yearsItems = new List<object>();
            var modValue = backward ? -10 : -1;
            for (var i = 0; i < 12; i++)
            {
                yearsItems.Add((year + modValue++));
            }
            DateItems = yearsItems;
        }

        private void OnHeaderClick()
        {
            this.isYearSelectionMode = false;
            this.PART_HeaderTitle.Content = "Select decade...";
            this.DateItems = this.DecadeItems;
        }

        /// <summary>
        /// On Header Arrow Click
        /// </summary>
        /// <param name="arrow">0 for left and 1 for right arrow</param>
        private void OnHeaderArrowClick(int arrow)
        {
            var val = arrow == 0;
            if (this.isYearSelectionMode) ModifyYearItems(val);

            if (val) this.Page--;
            else this.Page++;
        }

        private void OnDateItemSelection(object dateItem)
        {
            if (isYearSelectionMode)
            {
                this.SelectedYear = (int)dateItem;
                this.Text = string.Empty + dateItem; 
                isYearSelectionMode = false;
                this.PART_HeaderTitle.Content = "Select decade...";
                this.DateItems = this.DecadeItems;
                RaiseEvent(new RoutedEventArgs(ItemSelectedEvent));
            }
            else
            {
                this.PART_HeaderTitle.Content = "Select year...";
                ProduceYearItems((Decade)dateItem);
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
