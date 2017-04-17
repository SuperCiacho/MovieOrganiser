// File created by Bartosz Nowak on 19/05/2014 20:01

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using MovieOrganiser.Model;
using MovieOrganiser.Properties;
using MovieOrganiser.Utils;
using Yorgi.FilmWebApi;
using Yorgi.FilmWebApi.Models;
using YorgiControls;

namespace MovieOrganiser.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Consts
        private static readonly string[] VideoExtensions = { ".avi", ".mkv", ".rm", ".rmvb", ".mp4", ".mov", ".3gp", ".flv", ".mpeg", ".mpg" };

        #endregion

        #region Fields
        private readonly IFilmWebApi filmWebApi;

        private bool isDropZoneVisible;
        private string searchBoxText;
        private int? selectedYear;
        private MovieType selectedMovieType;
        private List<MovieViewModel> movieList;
        private MovieDetailViewModel selectedMovieDetails;

        private ICommand cancelSearchCommand;
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
        private bool isDropPossible;

        private readonly CancellationTokenSource cancellationTokenSource;

        #endregion

        public MainWindowViewModel(IFilmWebApi api)
        {
            this.filmWebApi = api;
            this.MovieList = new List<MovieViewModel>();
            this.SelectedMovieType = MovieType.Both;

            this.cancellationTokenSource = new CancellationTokenSource();

            var args = Environment.GetCommandLineArgs();

            if (args.Length > 1) this.ProcessFileName(args[1]);
        }

        #region Properties

        public List<MovieViewModel> MovieList
        {
            get => this.movieList;
            set => this.Set(ref this.movieList, value);
        }

        public bool IsDropZoneVisible
        {
            get => this.isDropZoneVisible;
            set => this.Set(ref this.isDropZoneVisible, value);
        }

        public MovieType SelectedMovieType
        {
            get => this.selectedMovieType;
            set => this.Set(ref this.selectedMovieType, value);
        }

        public string SearchBoxText
        {
            get => searchBoxText;
            set => this.Set(ref searchBoxText, value);
        }

        public int? SelectedYear
        {
            get => this.selectedYear;
            set => this.Set(ref this.selectedYear, value);
        }

        public MovieDetailViewModel SelectedMovieDetails
        {
            get => this.selectedMovieDetails;
            private set => this.Set(ref this.selectedMovieDetails, value);
        }

        public MovieInfo CurrentMovieFile { get; private set; }

        public string CurrentFileName
        {
            get => currentFileName;
            set => this.Set(ref this.currentFileName, "Aktualnie otworzony plik:\n" + value);
        }

        public bool IsDropPossible
        {
            get => this.isDropPossible;
            set => this.Set(ref this.isDropPossible, value);
        }

        #endregion

        #region Commands

        public ICommand SearchCommand
        {
            get { return searchCommand ?? (searchCommand = new RelayCommand<string>(text => this.DoSearch(text, this.selectedYear))); }
        }

        public ICommand CancelSearchCommand
        {
            get
            {
                return cancelSearchCommand ?? (cancelSearchCommand = new RelayCommand(() =>
                {
                    if (!this.cancellationTokenSource.IsCancellationRequested)
                    {
                        this.cancellationTokenSource.Cancel();
                    }
                }));
            }
        }

        public ICommand DragEnterCommand => this.dragEnterCommand ?? (this.dragEnterCommand = new RelayCommand<string>(this.OnDragEnter));

        public ICommand DragLeaveCommand
        {
            get { return dragLeaveCommand ?? (dragLeaveCommand = new RelayCommand(() => this.IsDropZoneVisible = false)); }
        }

        public ICommand DropCommand => this.dropCommand ?? (this.dropCommand = new RelayCommand<string>(this.DoDrop));

        public ICommand QuitCommand => this.quitCommand ?? (this.quitCommand = new RelayCommand(() => Application.Current.Shutdown()));

        public ICommand OpenCommand => openCommand ?? (openCommand = new RelayCommand(this.OnOpenCommand));

        public ICommand SetLibraryPathCommand => setLibraryPathCommand ?? (setLibraryPathCommand = new RelayCommand(this.OnSetLibraryPathCommand));

        public ICommand SetContextMenuCommand => this.setContextMenuCommand ?? (this.setContextMenuCommand = new RelayCommand(Console.Beep));

        public ICommand MovieSelectionCommand => this.movieSelectionCommand ?? (this.movieSelectionCommand = new RelayCommand<int>(this.ShowMovieDetails));

        #endregion

        private async void DoSearch(string title, int? year = null)
        {
            this.IsBusy = true;

            try
            {
                IEnumerable<Movie> downloadedData;
                switch (this.SelectedMovieType)
                {
                    case MovieType.Movie:
                        downloadedData = await this.filmWebApi.GetMovieList(title, year);
                        break;
                    case MovieType.Series:
                        downloadedData = await this.filmWebApi.GetSeriesList(title, year);
                        break;
                    case MovieType.Both:
                        var list = new List<Movie>();
                        list.AddRange(await this.filmWebApi.GetMovieList(title, year));
                        list.AddRange(await this.filmWebApi.GetSeriesList(title, year));

                        downloadedData = list;
                        break;
                    default:
                        downloadedData = Enumerable.Empty<Movie>();
                        break;
                }

                this.MovieList = downloadedData
                    .OrderBy(movie => movie.Title)
                    .Select(m => new MovieViewModel(m))
                    .ToList();
            }
            catch (AggregateException e)
            {
                throw e.Flatten();
            }
            finally
            {
                this.IsBusy = false;
            }

        }

        private void OnOpenCommand()
        {
            var ofd = new OpenFileDialog()
            {
                Filter = "Pliki wideo|*.avi;*.mkv;*.rm;*.rmvb;*.mp4;*.mov;*.3gp;*.flv;*.mpeg;*.mpg|Wszystke pliki|*.*",
                CheckFileExists = true
            };

            if (ofd.ShowDialog().GetValueOrDefault())
            {
                this.ProcessFileName(ofd.FileName);
            }
        }

        private void OnSetLibraryPathCommand()
        {
            var movieLibraryLocation = Settings.Default.Location;
            var fp = new FolderPicker(movieLibraryLocation);
            if (fp.ShowDialog().GetValueOrDefault())
            {
                var path = fp.SelectedPath;
                if (string.IsNullOrEmpty(path) || path == movieLibraryLocation) return;
                Settings.Default.Location = path;
                Settings.Default.Save();
            }
        }


        private void OnDragEnter(string filePath)
        {
            this.IsDropPossible = !string.IsNullOrWhiteSpace(filePath) && VideoExtensions.Any(filePath.EndsWith);
            this.IsDropZoneVisible = true;
        }

        private void DoDrop(string filePath)
        {
            if (this.isDropPossible)
            {
                this.SelectedMovieDetails = null;
                ProcessFileName(filePath);
            }

            IsDropZoneVisible = false;
        }

        private void ProcessFileName(string file)
        {
            this.CurrentMovieFile = CatalogTool.Instance.ParseFileName(file);
            this.SearchBoxText = CurrentMovieFile.Title;
            this.SelectedYear = CurrentMovieFile.Year;
            this.SelectedMovieType = CurrentMovieFile.Type;
            this.CurrentFileName = Path.GetFileName(CurrentMovieFile.FilePath);
        }

        private void ShowMovieDetails(int index)
        {
            var viewModel = new MovieDetailViewModel(this.MovieList[index], CurrentMovieFile);

            void OnClearView(object sender, EventArgs args)
            {
                this.SelectedMovieDetails = null;
                ((MovieDetailViewModel)sender).ClearView -= OnClearView;
            }

            viewModel.ClearView += OnClearView;
            this.SelectedMovieDetails = viewModel;
        }
    }
}