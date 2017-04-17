// File created by Bartosz Nowak on 04/10/2014 14:17

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yorgi.FilmWebApi.Models;
using Yorgi.FilmWebApi.Utils;

namespace Yorgi.FilmWebApi
{
    internal class FilmWebApi : IFilmWebApi
    {
        #region Fields

        private readonly IContentAnalyzer contentAnalyzer;

        #endregion

        #region Constructors 

        private FilmWebApi(IContentAnalyzer contentAnalyzer)
        {
            this.contentAnalyzer = contentAnalyzer;
        }

        public static FilmWebApi CreateInstance()
        {
            return NinjectHelper.Get<FilmWebApi>();
        }

        #endregion
        
        #region Api Methods

        /// <summary>
        /// Lista filmów (+ podstawowe informacje) o danym tytule i roku produkcji
        /// Pozycje posortowane wg trafności (popularność)
        /// </summary>
        /// <param name="title">Tytuł filmu</param>
        /// <param name="year">Rok produkcji</param>
        /// <returns>Lista filmów wraz z podstawowymi informacjami</returns>
        public Task<List<Movie>> GetMovieList(string title, int? year)
        {
            var searchParameters = new SearchParameters(title, QueryType.Film, false, year);
            return this.Get<Movie>(searchParameters);
        }

        /// <summary>
        /// Lista ID filmów o danym tytule. Pozycje posortowane wg trafności (popularność)
        /// </summary>
        /// <param name="title">Tytuł filmu</param>
        /// <param name="year">Rok produkcji</param>
        /// <returns>Lista ID filmów</returns>
        public Task<List<int>> GetMovieIdList(string title, int? year)
        {
            var searchParameters = new SearchParameters(title, QueryType.Film, true, year);
            return this.GetIds(searchParameters);
        }

        /// <summary>
        /// Lista seriali (+ podstawowe informacje) o danym tytule i roku produkcji
        /// </summary>
        /// <param name="title">Tytuł serialu</param>
        /// <param name="year"> Rok produkcji</param>
        /// <returns>Lista seriali wraz z podstawowymi informacjami.</returns>
        public Task<List<Series>> GetSeriesList(string title, int? year)
        {
            var searchParameters = new SearchParameters(title, QueryType.Serial, false, year);
            return this.Get<Series>(searchParameters);
        }

        /// <summary>
        /// Lista ID seriali o danym tytule.
        /// Pozycje posortowane wg trafności (popularność).
        /// </summary>
        /// <param name="title">Tytuł serialu</param>
        /// <param name="year"></param>
        /// <returns>Lista ID seriali</returns>
        public Task<List<int>> GetSeriesIdList(string title, int? year)
        {
            var searchParameters = new SearchParameters(title, QueryType.Serial, true, year);
            return this.GetIds(searchParameters);
        }

        private async Task<List<int>> GetIds(SearchParameters searchParameters)
        {
            return (await this.Get<Movie>(searchParameters).ConfigureAwait(false)).Select(film => film.Id).ToList();
        }

        private async Task<List<T>> Get<T>(SearchParameters searchParameters) where T : Movie
        {
            return await this.contentAnalyzer.GetItemsList<T>(searchParameters).ConfigureAwait(false);
        }

        #endregion
    }
}