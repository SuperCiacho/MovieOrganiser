using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace YorgiControls
{
    /// <summary>
    /// Interaction logic for YearPicker.xaml
    /// </summary>
    public partial class YearPicker : UserControl
    {
        public YearPicker()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs args)
        {
            var w = Window.GetWindow(this);

            if (null != w)
            {
                // "bump" the offset to cause the popup to reposition itself
                //   on its own
                w.LocationChanged += OnPopupMove;
                // Also handle the window being resized (so the popup's position stays
                //  relative to its target element if the target element moves upon 
                //  window resize)
                w.SizeChanged += OnPopupMove;
            }
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

        public static readonly DependencyProperty SelectedYearProperty =
            DependencyProperty.Register("SelectedYear", typeof(int), typeof(YearPicker), new FrameworkPropertyMetadata(-1)
            {
                BindsTwoWayByDefault = true,
            });

        public static readonly DependencyProperty TextProperty = 
            DependencyProperty.Register("Text", typeof(string), typeof(YearPicker), new FrameworkPropertyMetadata(default(string)) 
            {
                BindsTwoWayByDefault = true 
            });

        private void ItemSelectedChanged(object sender, RoutedEventArgs e)
        {
            this.CalendarPopup.IsOpen = false;
        }

        private void OnPopupMove(object sender, EventArgs e)
        {
            var offset = this.CalendarPopup.HorizontalOffset;
            this.CalendarPopup.HorizontalOffset = offset + 1;
            this.CalendarPopup.HorizontalOffset = offset;
        }
    }
}
