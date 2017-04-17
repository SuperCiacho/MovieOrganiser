// File created by Bartosz Nowak on 20/07/2014 14:49

using System;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using MovieOrganiser.Model;
using MovieOrganiser.Utils;

namespace MovieOrganiser.ViewModel
{
    public class MovieDetailViewModel : MovieViewModel, IDisposable
    {
        private ICommand catalogFileCommand;
        private ICommand clearViewSource;
        public event EventHandler ClearView;

        public MovieDetailViewModel(MovieViewModel viewModelMovie, MovieInfo movieInfo)
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

        public MovieDetailViewModel(MovieViewModel viewModelMovie) : this(viewModelMovie, new MovieInfo())
        {
        }

        public MovieInfo MovieInfo { get; }

        public ICommand ClearViewSource
        {
            get
            {
                return clearViewSource ??
                       (clearViewSource = new RelayCommand<MouseButtonEventArgs>(OnClearViewSource));
            }
        }

        private void OnClearViewSource(MouseButtonEventArgs o)
        {
            if (((FrameworkElement) o.Source).Name == "Overlay")
            {
                this.MovieInfo.TranslationTechinque = null;
                this.ClearView?.Invoke(this, EventArgs.Empty);
            }
        }

        public ICommand CatalogFileCommand => this.catalogFileCommand ?? (this.catalogFileCommand = new RelayCommand(this.CatalogFile, this.CanCatalogFile));

        private bool CanCatalogFile()
        {
            return MovieInfo?.TranslationTechinque != null;
        }

        private void CatalogFile()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            if (CatalogTool.Instance.CatalogMovie(this.Movie, this.MovieInfo))
            {
                this.ClearView?.Invoke(this, EventArgs.Empty);
            }

            Mouse.OverrideCursor = null;
        }

        public void Dispose()
        {
        }
    }
}