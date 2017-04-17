// File created by Bartosz Nowak on 20/07/2014 16:33

using System.Windows;
using System.Windows.Controls;
using MovieOrganiser.ViewModel;

namespace MovieOrganiser.View
{
    /// <summary>
    /// Interaction logic for ViewMovieDetail.xaml
    /// </summary>
    public partial class MovieDetail : UserControl
    {
        public static readonly DependencyProperty SelectedMovieProperty = DependencyProperty.RegisterAttached(
            "SelectedMovie", typeof (MovieDetailViewModel), typeof (MovieDetail), new PropertyMetadata(default(MovieDetailViewModel)));

        public static void SetSelectedMovie(DependencyObject element, MovieDetailViewModel value)
        {
            element.SetValue(SelectedMovieProperty, value);
        }

        public static MovieDetailViewModel GetSelectedMovie(DependencyObject element)
        {
            return (MovieDetailViewModel) element.GetValue(SelectedMovieProperty);
        }

        public MovieDetail()
        {
            InitializeComponent();
        }
    }
}