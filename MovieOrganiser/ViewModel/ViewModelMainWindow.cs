using FolderPicker;
using FolderPicker.View;
using FolderPicker.ViewModel;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using MovieOrganiser.Model;
using MovieOrganiser.Utils;
using MovieOrganiser.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MovieOrganiser.ViewModel
{
    public class ViewModelMainWindow : ViewModelView
    {
        #region Fields

        private bool isDropZoneVisible;
        private string searchBoxText;
        private string yearTextInput;
        private IList<ViewMovie> movieListView;
        private ViewMovieDetail selectedViewMovie;
        private Task ongoingTask;

        private ICommand searchCommand;
        private ICommand movieSelectionCommand;
        private ICommand dragEnterCommand;
        private ICommand dragLeaveCommand;
        private ICommand dropCommand;
        private ICommand quitCommand;
        private ICommand openCommand;
        private ICommand setLibraryPathCommand;
        private ICommand setContextCommand;

        #endregion

        #region Properties

        public IList<ViewMovie> MovieListView
        {
            get
            {
                return this.movieListView;
            }
            set
            {
                if (this.movieListView == value) return;
                this.movieListView = value;
                RaisePropertyChanged("MovieListView");
            }
        }
        public bool IsDropZoneVisible
        {
            get { return isDropZoneVisible; }
            set
            {
                isDropZoneVisible = value;
                RaisePropertyChanged("IsDropZoneVisible");
            }
        }
        public MovieType SelectedType { get; set; }
        public string SearchBoxText
        {
            get { return searchBoxText; }
            set
            {
                searchBoxText = value;
                RaisePropertyChanged("SearchBoxText");
            }
        }
        public string YearTextInput
        {
            get { return yearTextInput; }
            set
            {
                yearTextInput = value;
                RaisePropertyChanged("YearTextInput");
            }
        }
        public ViewMovieDetail SelectedViewMovie
        {
            get { return selectedViewMovie; }
            private set
            {
                selectedViewMovie = value;
                RaisePropertyChanged("SelectedViewMovie");
            }
        }
        public MovieInfo LastDroppedMovieFile { get; private set; }

        #endregion

        #region Commands

        public ICommand SearchCommand
        {
            get
            {
                return searchCommand ?? (searchCommand = new RelayCommand<TextBox>(arg => DoSearch(arg.Text, YearTextInput)));
            }
        }

        public ICommand OnWindowLoaded { get; set; }

        public ICommand DragEnterCommand
        {
            get
            {
                return dragEnterCommand ?? (dragEnterCommand = new RelayCommand<DragEventArgs>(e =>
                    {
                        IsDropZoneVisible = true;
                        SelectedViewMovie = null;
                        e.Effects = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Link : DragDropEffects.None;
                    }));
            }
        }
        public ICommand DragLeaveCommand
        {
            get
            {
                return dragLeaveCommand ?? (dragLeaveCommand = new RelayCommand<object>(s => IsDropZoneVisible = false));
            }
        }
        public ICommand DropCommand
        {
            get
            {
                return dropCommand ?? (dropCommand = new RelayCommand<DragEventArgs>(DoDrop));
            }
        }
        public ICommand QuitCommand
        {
            get { return quitCommand ?? (quitCommand = new RelayCommand(() => Application.Current.Shutdown())); }
        }
        public ICommand OpenCommand
        {
            get
            {
                return openCommand ?? (openCommand = new RelayCommand(() =>
                {
                    var ofd = new OpenFileDialog();
                    ofd.ShowDialog();
                    // TODO ustawić filtry i dodać całą logikę.
                }));
            }
        }
        public ICommand SetLibraryPathCommand
        {
            get { return setLibraryPathCommand ?? (setLibraryPathCommand = new RelayCommand(() =>
            {
                var path = new ViewModelFolderPicker().Show(new ViewFolderPicker());
                if (string.IsNullOrEmpty(path) || path == Properties.Settings.Default.Location) return;
                Properties.Settings.Default.Location = path;
                Properties.Settings.Default.Save();
            }));
            }
        }
        public ICommand SetContextCommand
        {
            get { return setContextCommand ?? (setContextCommand = new RelayCommand(() => Application.Current.Shutdown())); }
        }
        public ICommand MovieSelectionCommand
        {
            get
            {
                return movieSelectionCommand ?? (movieSelectionCommand = new RelayCommand<MouseButtonEventArgs>(ShowMovieDetails));
            }
        }

        #endregion

        public ViewModelMainWindow()
        {
            MovieListView = new List<ViewMovie>();
            SelectedType = MovieType.Both;

            for (var i = 0; i < 5; i++)
            {
                var view = new ViewMovie()
                {
                    DataContext = new ViewModelMovie()
                    {
                        Type = i % 3 == 0 ? "S" : "M"
                    }
                };
                MovieListView.Add(view);
            }

            SearchBoxText = "Mr Nobody";
        }

        private void DoSearch(string title, string year = null)
        {
            IsBusy = true;
            ongoingTask = Task.Factory.StartNew<IEnumerable<Movie>>(() =>
            {
                bool value = year == null;

                switch (SelectedType.ToString())
                {
                    case "Movie": return value ? FilmWebApi.GetMovieList(title) : FilmWebApi.GetMovieList(title, Convert.ToInt32(year));
                    case "Series": return value ? FilmWebApi.GetSeriesList(title) : FilmWebApi.GetSeriesList(title, Convert.ToInt32(year));
                    case "Both":
                        var list = new List<Movie>();
                        if (value)
                        {
                            list.AddRange(FilmWebApi.GetMovieList(title));
                            list.AddRange(FilmWebApi.GetSeriesList(title));
                        }
                        else
                        {
                            list.AddRange(FilmWebApi.GetMovieList(title, Convert.ToInt32(year)));
                            list.AddRange(FilmWebApi.GetSeriesList(title, Convert.ToInt32(year)));
                        }
                        return list;
                    default: return new List<Movie>();
                }
            })
                .ContinueWith(task =>
                {
                    MovieListView = task.Result.OrderBy(movie => movie.Title).ToMovieListView();
                    IsBusy = false;
                });
        }

        private void DoDrop(DragEventArgs eventArg)
        {
            if (eventArg.Source.GetType() == typeof(DockPanel))
            {
                if (eventArg.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    var file = ((string[])eventArg.Data.GetData(DataFormats.FileDrop))[0];
                    LastDroppedMovieFile = CatalogTool.Instance.ParseFileName(file);
                    SearchBoxText = LastDroppedMovieFile.Title;
                    YearTextInput = LastDroppedMovieFile.Year.ToString();
                    SelectedType = LastDroppedMovieFile.Type;
                    RaisePropertyChanged();
                }
            }
            IsDropZoneVisible = false;
        }

        private void ShowMovieDetails(MouseButtonEventArgs eventArgs)
        {
            var listbox = eventArgs.Source as ListBox;
            var view = (UserControl)listbox.Items[listbox.SelectedIndex];
            var movieDetail = new ViewMovieDetail();
            var viewModel = new ViewModelMovieDetail((ViewModelMovie)view.DataContext, LastDroppedMovieFile)
            {
                ClearView = () => SelectedViewMovie = null,
            };
            movieDetail.DataContext = viewModel;
            SelectedViewMovie = movieDetail;
        }
    }
}
