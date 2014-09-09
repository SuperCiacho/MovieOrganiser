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
using YorgiControls.ViewModels;

namespace MovieOrganiser.ViewModel
{
    public class ViewModelMainWindow : ViewModelView
    {
        #region Fields

        private bool isDropZoneVisible;
        private string searchBoxText;
        private string yearTextInput;
        private MovieType selectedType;
        private IList<ViewMovie> movieListView;
        private ViewMovieDetail selectedViewMovie;

        private ICommand searchCommand;
        private ICommand movieSelectionCommand;
        private ICommand dragEnterCommand;
        private ICommand dragLeaveCommand;
        private ICommand dropCommand;
        private ICommand quitCommand;
        private ICommand openCommand;
        private ICommand setLibraryPathCommand;
        private ICommand setContextMenuCommand;
        private string currentFileName;

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
                if (Equals(this.movieListView, value)) return;
                this.movieListView = value;
                RaisePropertyChanged("MovieListView");
            }
        }
        public bool IsDropZoneVisible
        {
            get { return isDropZoneVisible; }
            set
            {
                if (Equals(isDropZoneVisible, value)) return;
                isDropZoneVisible = value;
                RaisePropertyChanged("IsDropZoneVisible");
            }
        }
        public MovieType SelectedType
        {
            get { return selectedType; }
            set
            {
                if (Equals(selectedType, value)) return;
                selectedType = value;
                RaisePropertyChanged("SelectedType");
            }
        }
        public string SearchBoxText
        {
            get { return searchBoxText; }
            set
            {
                if (Equals(searchBoxText, value)) return;
                searchBoxText = value;
                RaisePropertyChanged("SearchBoxText");
            }
        }
        public string YearTextInput
        {
            get { return yearTextInput; }
            set
            {
                if (Equals(yearTextInput, value)) return;
                yearTextInput = value;
                RaisePropertyChanged("YearTextInput");
            }
        }
        public ViewMovieDetail SelectedViewMovie
        {
            get { return selectedViewMovie; }
            private set
            {
                if (Equals(selectedViewMovie, value)) return;
                selectedViewMovie = value;
                RaisePropertyChanged("SelectedViewMovie");
            }
        }
        public MovieInfo CurrentMovieFile { get; private set; }
        public string CurrentFileName
        {
            get { return currentFileName; }
            set
            {
                if (value != currentFileName)
                {
                    currentFileName = "Aktualnie otworzony plik:\n" + value;
                    RaisePropertyChanged("CurrentFileName");
                }
            }
        }

        #endregion

        #region Commands

        public ICommand SearchCommand
        {
            get
            {
                return searchCommand ?? (searchCommand = new RelayCommand<TextBox>(arg => DoSearch(arg.Text, YearTextInput)));
            }
        }
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
                    var ofd = new OpenFileDialog()
                    {
                        Filter = "Pliki wideo|*.avi;*.mkv;*.rm;*.rmvb;*.mp4;*.mov;*.3gp;*.flv;*.mpeg;*.mpg|Wszystke pliki|*.*",
                        CheckFileExists = true
                    };

                    if (ofd.ShowDialog() == true)
                    {
                        ProcessFileName(ofd.FileName);
                    }
                }));
            }
        }
        public ICommand SetLibraryPathCommand
        {
            get { return setLibraryPathCommand ?? (setLibraryPathCommand = new RelayCommand(() =>
            {
                var movieLibraryLocation = Properties.Settings.Default.Location;
                var path = new ViewModelFolderPicker(movieLibraryLocation).Show(new YorgiControls.Views.FolderPicker());
                if (string.IsNullOrEmpty(path) || path == movieLibraryLocation) return;
                Properties.Settings.Default.Location = path;
                Properties.Settings.Default.Save();
            }));
            }
        }
        public ICommand SetContextMenuCommand
        {
            get { return setContextMenuCommand ?? (setContextMenuCommand = new RelayCommand(Console.Beep)); }
        }
        public ICommand MovieSelectionCommand
        {
            get
            {
                return movieSelectionCommand ?? (movieSelectionCommand = new RelayCommand<int>(ShowMovieDetails));
            }
        }

        #endregion

        public ViewModelMainWindow()
        {
            MovieListView = new List<ViewMovie>();
            SelectedType = MovieType.Both;

            var args = Environment.GetCommandLineArgs();

            if (args.Length > 1) ProcessFileName(args[1]);
        }

        private void DoSearch(string title, string year = null)
        {
            IsBusy = true;

            Task.Factory.StartNew<IEnumerable<Movie>>(() =>
            {
                var value = string.IsNullOrEmpty(year);

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
            }).ContinueWith(task =>
                {
                    var list = task.Result.OrderBy(movie => movie.Title).ToMovieListView();
                    MovieListView = list;
                    IsBusy = false;
                });
        }

        private void DoDrop(DragEventArgs eventArg)
        {
            if (eventArg.Source.GetType() == typeof(DockPanel))
            {
                if (eventArg.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    ProcessFileName(((string[])eventArg.Data.GetData(DataFormats.FileDrop))[0]);
                }
            }
            IsDropZoneVisible = false;
        }

        private void ProcessFileName(string file)
        {
            this.CurrentMovieFile = CatalogTool.Instance.ParseFileName(file);
            this.SearchBoxText = CurrentMovieFile.Title;
            this.YearTextInput = CurrentMovieFile.Year.ToString();
            this.SelectedType = CurrentMovieFile.Type;
            this.CurrentFileName = System.IO.Path.GetFileName(CurrentMovieFile.FilePath);
        }

        private void ShowMovieDetails(int index)
        {
            var viewMovie = (UserControl)this.MovieListView[index];
            var movieDetails = new ViewMovieDetail();
            var viewModel = new ViewModelMovieDetail((ViewModelMovie)viewMovie.DataContext, CurrentMovieFile)
            {
                ClearView = () => SelectedViewMovie = null
            };
            movieDetails.DataContext = viewModel;
            SelectedViewMovie = movieDetails;
        }
    }
}
