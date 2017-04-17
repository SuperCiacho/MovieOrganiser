// File created by Bartosz Nowak on 04/10/2014 14:17

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Yorgi.FilmWebApi.Models;
using Yorgi.FilmWebApi.Utils;

namespace Yorgi.FilmWebApi
{
    internal class ApiHelper : IApiHelper
    {
        #region Fields

        private const string UrlPoster = "http://1.fwcdn.pl/po";

        #endregion

        #region Properties

        [Inject]
        private Connection Connection { get; set; }

        #endregion

        #region Non-Api Remote Methods

        /// <summary> Ustawienie informacji nt. filmu. </summary>
        /// <param name="movie">Referencja do filmu.</param>
        public async Task GetMovieInfo(Movie movie)
        {
            this.Connection.MethodName = $"getFilmInfoFull [{movie.Id}]";
            if (this.Connection.MethodName == null) return;

            var responseTaskResult = await this.Connection.PrepareResponse().ConfigureAwait(false);
            var response = (List<object>) responseTaskResult;

            try
            {
                movie.Title = (string) response[(int) MovieInfoLevel.OriginalTitle];
            }
            catch (NullReferenceException)
            {
                Logger.Debug("Tytuł polski jest identyczny z oryginalnym. Przechowywany jako polski.");
            }

            try
            {
                movie.PolishTitle = (string) response[(int) MovieInfoLevel.PolishTitle];
            }
            catch (NullReferenceException)
            {
                Logger.Error("Brak polskiego tytułu.");
            }

            try
            {
                movie.Rate = Convert.ToDouble(response[(int) MovieInfoLevel.Rate]);
            }
            catch (FormatException)
            {
                Logger.Error("Nieprawidłowy format oceny filmu.");
            }
            catch (NullReferenceException)
            {
                Logger.Debug("Brak oceny filmu.");
            }

            //liczba głosów
            try
            {
                movie.Votes = Convert.ToInt64(response[(int)MovieInfoLevel.Votes]);
            }
            catch (FormatException)
            {
                Logger.Error("Nieprawidłowy format liczby głosów filmu.");
            }
            catch (NullReferenceException)
            {
                Logger.Debug("Brak głosów na film.");
            }

            //gatunek
            try
            {
                movie.Genre = ((string) response[(int) MovieInfoLevel.Genre]).Replace(",", ", ");
            }
            catch (NullReferenceException)
            {
                Logger.Debug("Brak informacji na temat gatunku.");
            }

            //rok produkcji
            try
            {
                movie.Year = Convert.ToInt32(response[(int) MovieInfoLevel.Year]);
            }
            catch (FormatException)
            {
                Logger.Error("Nieprawidłowy format roku produkcji.");
            }
            catch (NullReferenceException)
            {
                Logger.Debug("Brak informacji o dacie produkcji.");
            }

            //czas trwania
            try
            {
                movie.Duration = Convert.ToInt32(response[(int) MovieInfoLevel.Duration]);
            }
            catch (FormatException)
            {
                Logger.Error("Nieprawidłowy format czasu trwania filmu.");
            }
            catch (NullReferenceException)
            {
                Logger.Debug("Brak informacji o czasie trwania filmu.");
            }

            //okładka

            try
            {
                var data = response[(int) MovieInfoLevel.CoverUrl];
                if (data != null)
                {
                    movie.CoverUrl = new Uri(UrlPoster + data);
                }
            }
            catch (UriFormatException)
            {
                Logger.Error("Błąd URL dla okładki filmu.");
            }
            catch (NullReferenceException)
            {
                Logger.Debug("Brak URL okładki filmu.");
            }

            //kraj produkcji
            try
            {
                movie.Countries = (string) response[(int) MovieInfoLevel.Country];
            }
            catch (NullReferenceException)
            {
                Logger.Debug("Brak informacji o kraju produkcji.");
            }

            //zarys fabuły
            try
            {
                movie.Plot = (string) response[(int) MovieInfoLevel.Plot];
            }
            catch (NullReferenceException)
            {
                Logger.Debug("Brak zarysu fabuły.");
            }

            //serial posiada kilka dodatkowych pól
            if (movie is Series)
            {
                var series = (Series) movie;

                //liczba sezonów
                try
                {
                    series.SeasonsCount = Convert.ToInt32(response[(int) MovieInfoLevel.NumberOfSeasons]);
                }
                catch (FormatException)
                {
                    Logger.Error("Nieprawidłowy format liczby sezonów.");
                }
                catch (NullReferenceException)
                {
                    Logger.Debug("Brak informacji o liczbie sezonów.");
                }

                //liczba odcinków
                try
                {
                    series.EpisodesCount = Convert.ToInt32(response[(int) MovieInfoLevel.NumberOfEpisodes]);
                }
                catch (FormatException)
                {
                    Logger.Error("Nieprawidłowy format liczby odcinków.");
                }
                catch (NullReferenceException)
                {
                    Logger.Debug("Brak informacji o liczbie odcinków.");
                }
            }
        }

        /// <summary>
        /// Lista osób danej profesji z danego filmu
        /// </summary>
        /// <param name="filmId">ID filmu</param>
        /// <param name="profession">Nazwa profesji</param>
        /// <returns>Lista osób</returns>
        public async Task<List<Person>> GetFilmPersons(int filmId, Profession profession)
        {
            if (filmId <= 0) return null;

            var pageNo = 0;
            var personList = new List<Person>();
            var partPersonList = new List<Person>();
            try
            {
                do
                {
                    this.Connection.MethodName = "getFilmPersons [" + filmId + "," + (int) profession + "," + 50*pageNo + "," + 50*(1 + pageNo) + "]";
                    if (this.Connection.MethodName == null) continue;

                    partPersonList = (List<Person>) await this.Connection.PrepareResponse().ConfigureAwait(false);
                    if (partPersonList.Count > 0) personList.AddRange(partPersonList);
                    pageNo++;
                } while (partPersonList.Count == 50); //TODO: Nie jestem pewny poprawności tego warunku
            }
            catch (NullReferenceException)
            {
                Logger.Debug("Brak ludzi o profesji " + profession + " związanych z filmem.");
            }
            return personList;
        }

        /// <summary>
        /// Pobiera opis filmu.
        /// </summary>
        /// <param name="filmId">Identyfikator filmu.</param>
        /// <returns>Opis filmu.</returns>
        public async Task<string> GetFilmDescription(int filmId)
        {
            if (filmId <= 0) return null;
            var desc = new StringBuilder();
            this.Connection.MethodName = "getFilmDescription [" + filmId + "]";
            if (this.Connection.MethodName != null)
            {
                try
                {
                    var response = await this.Connection.PrepareResponse().ConfigureAwait(false);
                    desc.Append(response.ToString());
                }
                catch (NullReferenceException)
                {
                    Logger.Error("Brak danych opisu filmu.");
                }
            }
            return desc.ToString();
        }

        #endregion
    }
}