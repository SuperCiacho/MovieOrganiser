using System.Runtime.Serialization;
using GalaSoft.MvvmLight.Command;
using MovieOrganiser.Model;
using MovieOrganiser.Utils;
using System;
using System.Windows.Input;

namespace MovieOrganiser.ViewModel
{
    public class ViewModelMovieDetail : ViewModelMovie
    {
        private ICommand clearViewSource;
        private ICommand catalogFileCommand;

        public MovieInfo MovieInfo { get; set; }
        
        public Action ClearView { private get; set; }
        public ICommand ClearViewSource
        {
            get
            {
                return clearViewSource ?? (clearViewSource = new RelayCommand<MouseButtonEventArgs>(o =>
                {
                    if (((System.Windows.FrameworkElement)o.Source).Name == "GrayZone") ClearView.Invoke();
                }));
            }
        }
        public ICommand CatalogFileCommand
        {
            get { return catalogFileCommand ?? (catalogFileCommand = new RelayCommand<object>(CatalogFile, o => MovieInfo != null && MovieInfo.TranslationTechinque.HasValue)); }
        }

        public ViewModelMovieDetail(ViewModelMovie viewModelMovie, MovieInfo movieInfo)
        {
            this.Genre = viewModelMovie.Genre;
            this.OriginalTitle = viewModelMovie.OriginalTitle;
            this.PolishTitle = viewModelMovie.PolishTitle;
            this.Img = viewModelMovie.Img;
            this.Year = viewModelMovie.Year;
            this.Type = viewModelMovie.Type;
            this.Movie = viewModelMovie.Movie;
            this.MovieInfo = movieInfo;
        }

        public ViewModelMovieDetail(ViewModelMovie viewModelMovie) : this(viewModelMovie, new MovieInfo()) { }

        private void CatalogFile(object arg)
        {
            CatalogTool.Instance.CatalogMovie(Movie, MovieInfo);
            this.ClearView.Invoke();
        }
    }
}
