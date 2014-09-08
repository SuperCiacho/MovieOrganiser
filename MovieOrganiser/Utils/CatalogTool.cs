using System.Globalization;
using System.Linq;
using System.Windows.Input;
using MovieOrganiser.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using Toolkit = Xceed.Wpf.Toolkit;
namespace MovieOrganiser.Utils
{
    internal class CatalogTool
    {
        #region Fields

        #region Regular Expressions
        // ReSharper disable once InconsistentNaming
        private static readonly Regex HDRegex = new Regex(@"\d{3,4}p{1}");
        private static readonly Regex YearRegex = new Regex(@"\d{4}");
        // ReSharper disable once InconsistentNaming
        private static readonly Regex CDRegex = new Regex(@"cd\d{1}");
        private static readonly Regex SeriesRegex = new Regex(@"([sS]\d{1,2}[eE]\d{2})|(\d{1,2}x\d{2})");
        #endregion

        private static readonly string[] Extensions = new string[] { "avi", "mp4", "mkv", "rm", "rmvb", "3gp", "flv", "mpeg", "mpg", "mov" };
        private static readonly string[] Subtitles = new string[] { ".txt", ".sub", ".srt" };
        private static readonly List<string> Keywords = new List<string>() { "xvid", "dvdrip", "x264", "bdrip", "brrip", "bluray", "dubbing", "dubb", "lektor", "hdtv", "pdtv", "ac3", "dts", "(", ")", "[", "]" };

        private static CatalogTool _instance;

        private readonly List<char> invalidChars;

        #endregion

        private CatalogTool()
        {
            invalidChars = new List<char>();
            invalidChars.AddRange(Path.GetInvalidPathChars());
            invalidChars.AddRange(Path.GetInvalidFileNameChars());
        }

        public static CatalogTool Instance
        {
            get
            {
                return _instance ?? (_instance = new CatalogTool());
            }
        }

        public MovieInfo ParseFileName(string file)
        {
            var movieInfo = new MovieInfo()
            {
                FilePath = file
            };

            var fileName = Path.GetFileNameWithoutExtension(file);

            if (string.IsNullOrEmpty(fileName)) return movieInfo;

            if (HDRegex.IsMatch(fileName))
                movieInfo.HD = HDRegex.Match(fileName).Value;

            if (YearRegex.IsMatch(fileName))
            {
                int year;
                var match = YearRegex.Match(fileName);
                if (int.TryParse(match.Value, out year) && year > 1900)
                {
                    fileName = fileName.Remove(match.Index);
                    movieInfo.Year = year;
                }
            }

            var sb = new StringBuilder(fileName);

            Keywords.ForEach(x => sb.Replace(x, " "));
            sb.Replace('.', ' ').Replace('_', ' ');

            if (!string.IsNullOrEmpty(movieInfo.HD)) sb.Replace(movieInfo.HD, string.Empty);

            if (CDRegex.IsMatch(fileName))
            {
                sb.Replace(CDRegex.Match(fileName).Value, string.Empty);
            }
            fileName = sb.ToString();
            if (SeriesRegex.IsMatch(fileName))
            {
                var m = SeriesRegex.Match(fileName);
                fileName = fileName.Remove(m.Index);
                movieInfo.Type = MovieType.Series;
            }
            else
            {
                movieInfo.Type = MovieType.Movie;
            }

            if (Regex.IsMatch(fileName, " pl$| en$"))
            {
                fileName = fileName.Remove(fileName.Length - 3, 3);
            }

            fileName = fileName.TrimStart(' ').TrimEnd(' ');
            movieInfo.Title = fileName;

            return movieInfo;
        }

        public void CatalogMovie(Movie movie, MovieInfo movieInfo)
        {
            Mouse.OverrideCursor = Cursors.Wait;

            var file = new FileInfo(movieInfo.FilePath);
            var isRoot = file.Directory != null && file.Directory.Parent == null;
            if (!Regex.IsMatch(file.DirectoryName, @"^\d{2}.\d{1} - \w"))
            {
                var subsAvailable = false;
                try
                {
                    var sb = new StringBuilder();
                    sb.AppendFormat("{0} - {1} ({2})[{3}][{4}][{5}]", 
                        movie.Rate.ToString("##.#").Replace(',', '.'),
                        movie.PolishTitle, 
                        movie.Title,
                        movie.Genre, 
                        movie.Year, 
                        movieInfo.TranslationTechinque.Value.GetValue());
                    if (movieInfo.HD != null) sb.Append(string.Format("[{0}]", movieInfo.HD));

                    invalidChars.ForEach(c => sb.Replace(c.ToString(CultureInfo.InvariantCulture), string.Empty));

                    var newDirName = sb.ToString();
                    var newPath = Path.Combine(Properties.Settings.Default.Location, newDirName);

                    if (!Directory.Exists(newPath))
                    {
                        try
                        {
                            new DirectoryInfo(Properties.Settings.Default.Location).CreateSubdirectory(newDirName);
                            file.MoveTo(Path.Combine(newPath, file.Name));
                            File.WriteAllText(Path.Combine(newPath, "Description.txt"), movie.Description);

                            #region Subtitles

                            if (movieInfo.TranslationTechinque == TranslationTechnique.Subtitles)
                                foreach (var subs in Subtitles.Select(s => Path.ChangeExtension(file.ToString(), s)).Where(File.Exists))
                                {
                                    File.Move(subs, Path.Combine(newPath, Path.GetFileName(subs)));
                                    subsAvailable = true;
                                    break;
                                }

                            Toolkit.MessageBox.Show(string.Format("Film{1} został przeniesiony do poniżego folderu:\n{0}", newDirName,
                                subsAvailable ? " wraz z napisami" : string.Empty), "Powodzenie.", MessageBoxButton.OK);

                            #endregion

                            if (!isRoot)
                            {
                                var result = Toolkit.MessageBox.Show("Czy chcesz usunąć poprzedni folder, w któtrym znajdował się film?", "Usuwanie poprzedniego folderu", MessageBoxButton.YesNo);
                                if (result == System.Windows.MessageBoxResult.Yes)
                                {
                                    DeletingParentFolder(file.DirectoryName);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Toolkit.MessageBox.Show(ex.Message + "\n\nPlik nie został przeniesiony.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Stop);
                            Directory.Delete(newPath);
                        }
                    }
                }
                catch (Exception exception)
                {
                    Toolkit.MessageBox.Show(exception.Message + "\n", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
                Toolkit.MessageBox.Show("Film już został skatalogowany", "Błąd", MessageBoxButton.OK, MessageBoxImage.Asterisk);

            Mouse.OverrideCursor = null;
        }

        private void DeletingParentFolder(string path)
        {
            var numberOfFiles = Directory.EnumerateFiles(path).Count();
            var numberOfDirs = Directory.EnumerateDirectories(path).Count();
            var isAnyFiles = numberOfFiles > 0;
            var isAnyDirs = numberOfFiles > 0;

            if (isAnyFiles || isAnyDirs)
            {
                var info = string.Format("Folder zawiera {0}{1}{2}. \nPotwierdź jego usunięcie.",
                    isAnyFiles ? numberOfFiles + " plików" : string.Empty,
                    isAnyFiles && isAnyDirs ? " i " : string.Empty,
                    isAnyDirs ? numberOfDirs + " folderów" : string.Empty
                    );
                var retValue = Toolkit.MessageBox.Show(info, "Informacja.", MessageBoxButton.OKCancel, MessageBoxImage.Asterisk);
                if (retValue == MessageBoxResult.Cancel) return;
            }
            Directory.Delete(path);
        }
    }
}
