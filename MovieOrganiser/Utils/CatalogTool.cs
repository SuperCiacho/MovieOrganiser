// File created by Bartosz Nowak on 16/07/2014 18:13

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using MovieOrganiser.Model;
using MovieOrganiser.Properties;
using Yorgi.FilmWebApi.Models;

namespace MovieOrganiser.Utils
{
    internal class CatalogTool
    {
        #region Fields

        #region Regular Expressions

        private static readonly Regex hdRegex = new Regex(@"\d{3,4}p{1}");
        private static readonly Regex yearRegex = new Regex(@"\d{4}");
        private static readonly Regex cdRegex = new Regex(@"cd\d{1}");
        private static readonly Regex seriesRegex = new Regex(@"([sS]\d{1,2}[eE]\d{2})|(\d{1,2}x\d{2})");

        #endregion

        private static readonly string[] subtitles = {".txt", ".sub", ".srt"};

        private static readonly List<string> keywords = new List<string>()
        {
            "xvid",
            "dvdrip",
            "x264",
            "bdrip",
            "brrip",
            "bluray",
            "dubbing",
            "dubb",
            "lektor",
            "hdtv",
            "pdtv",
            "ac3",
            "dts",
            "(",
            ")",
            "[",
            "]"
        };

        private static CatalogTool instance;

        private readonly List<char> invalidChars;

        #endregion

        private CatalogTool()
        {
            invalidChars = new List<char>();
            invalidChars.AddRange(Path.GetInvalidPathChars());
            invalidChars.AddRange(Path.GetInvalidFileNameChars());
        }

        public static CatalogTool Instance => instance ?? (instance = new CatalogTool());

        public MovieInfo ParseFileName(string file)
        {
            var movieInfo = new MovieInfo()
            {
                FilePath = file
            };

            var fileName = Path.GetFileNameWithoutExtension(file);

            if (string.IsNullOrEmpty(fileName)) return movieInfo;

            if (hdRegex.IsMatch(fileName))
                movieInfo.HD = hdRegex.Match(fileName).Value;

            if (yearRegex.IsMatch(fileName))
            {
                var match = yearRegex.Match(fileName);
                if (int.TryParse(match.Value, out var year) && year > 1900)
                {
                    fileName = fileName.Remove(match.Index);
                    movieInfo.Year = year;
                }
            }

            var sb = new StringBuilder(fileName);

            keywords.ForEach(x => sb.Replace(x, " "));
            sb.Replace('.', ' ').Replace('_', ' ');

            if (!string.IsNullOrEmpty(movieInfo.HD)) sb.Replace(movieInfo.HD, string.Empty);

            if (cdRegex.IsMatch(fileName))
            {
                sb.Replace(cdRegex.Match(fileName).Value, string.Empty);
            }
            fileName = sb.ToString();
            if (seriesRegex.IsMatch(fileName))
            {
                var m = seriesRegex.Match(fileName);
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

        public bool CatalogMovie(Movie movie, MovieInfo movieInfo)
        {
            var isSuccessful = true;

            var file = new FileInfo(movieInfo.FilePath);
            var isRoot = file.Directory != null && file.Directory.Parent == null;
            if (file.DirectoryName != null && !Regex.IsMatch(file.DirectoryName, @"^\d{2}.\d{1} - \w"))
            {
                var subsAvailable = false;

                var sb = new StringBuilder();
                sb.AppendFormat(
                    Constants.MovieNameFormat,
                    movie.Rate.ToString("##.#").Replace(',', '.'),
                    movie.PolishTitle,
                    movie.Title == null || movie.PolishTitle == movie.Title ? string.Empty : $"({movie.Title}) ",
                    movie.Genre,
                    movie.Year,
                    movieInfo.TranslationTechinque.GetValueOrDefault(TranslationTechnique.MissingSubtitles).GetDescription());

                if (movieInfo.HD != null) sb.Append($"[{movieInfo.HD}]");

                invalidChars.ForEach(c => sb.Replace(c.ToString(CultureInfo.InvariantCulture), string.Empty));

                var newDirName = sb.ToString();
                var newPath = Path.Combine(Settings.Default.Location, newDirName);
                var previousPath = file.FullName;
                var previousDir = file.Directory.FullName;

                if (!Directory.Exists(newPath))
                {
                    try
                    {
                        new DirectoryInfo(Settings.Default.Location).CreateSubdirectory(newDirName);
                        File.WriteAllText(Path.Combine(newPath, "Opis.txt"), movie.Description);
                        file.MoveTo(Path.Combine(newPath, file.Name));

                        #region Subtitles

                        if (movieInfo.TranslationTechinque == TranslationTechnique.Subtitles)
                        {
                            foreach (var subs in subtitles.Select(s => Path.ChangeExtension(previousPath, s)).Where(File.Exists))
                            {
                                File.Move(subs, Path.Combine(newPath, Path.GetFileName(subs)));
                                subsAvailable = true;
                            }
                        }

                        Helper.ShowMessageBox(string.Format("Film{1} został przeniesiony do poniżego folderu:\n{0}",
                            newDirName,
                            subsAvailable ? " wraz z napisami" : string.Empty),
                            "Powodzenie.",
                            MessageBoxButton.OK);

                        #endregion
                    }
                    catch (Exception ex)
                    {
                        Helper.ShowMessageBox(ex.Message + "\n\nPlik nie został przeniesiony.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Stop);
                        Directory.Delete(newPath, true);
                        isSuccessful = false;
                    }

                    if (!isRoot)
                    {
                        var result = Helper.ShowMessageBox(
                            "Czy chcesz usunąć poprzedni folder, w którym znajdował się film?\n" + previousDir,
                            "Usuwanie poprzedniego folderu",
                            MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            DeletingParentFolder(previousDir);
                        }
                    }
                }
            }
            else
                Helper.ShowMessageBox("Film już został skatalogowany", "Informacja", MessageBoxButton.OK, MessageBoxImage.Asterisk);

            return isSuccessful;
        }

        private static void DeletingParentFolder(string path)
        {
            try
            {
                var numberOfFiles = Directory.EnumerateFiles(path).Count();
                var numberOfDirs = Directory.EnumerateDirectories(path).Count();
                var isAnyFiles = numberOfFiles > 0;
                var isAnyDirs = numberOfDirs > 0;
                var isMultiple = (isAnyFiles && isAnyDirs) || numberOfDirs > 1 || numberOfFiles > 1;

                if (isAnyFiles || isAnyDirs)
                {
                    var info = $@"Folder zawiera {(isAnyFiles ? GetQuantityString(numberOfFiles, "file") : string.Empty)}
                            {(isAnyFiles && isAnyDirs ? " i " : string.Empty)}
                            {(isAnyDirs ? GetQuantityString(numberOfDirs, "dir") : string.Empty)}.\nPotwierdź {(isMultiple ? "ich" : "jego")} usunięcie.";

                    if (Helper.ShowMessageBox(info, "Informacja.", MessageBoxButton.OKCancel, MessageBoxImage.Asterisk) == MessageBoxResult.Cancel) return;
                }
                Directory.Delete(path, true);
            }
            catch (IOException e)
            {
                Console.Error.Write(e.Message);
                Helper.ShowMessageBox("Proces usuwania nie powiódł się.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private static string GetQuantityString(int quantity, string item)
        {
            switch (item)
            {
                case "dir":
                {
                    if (quantity == 0) item = "folderów";
                    else if (quantity == 1) item = "folder";
                    else if (quantity > 1 && quantity < 5) item = "foldery";
                    else if (quantity > 4) item = "folderów";
                    break;
                }
                case "file":
                {
                    if (quantity == 0) item = "plików";
                    else if (quantity == 1) item = "plik";
                    else if (quantity > 1 && quantity < 5) item = "pliki";
                    else if (quantity > 4) item = "plików";
                    break;
                }
            }

            return $"{quantity} {item}";
        }
    }
}