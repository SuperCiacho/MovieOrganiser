using MovieOrganiser.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MovieOrganiser.Utils
{
    public class FilmWebApi
    {
        #region Fields

        internal ApiHelper ApiHelper { get; private set; }

        #endregion

        #region Api Methods

        public List<Movie> GetMovieList(string title)
        {
            return (List<Movie>)this.ApiHelper.GetItemsList(title, "film", false);
        }

        /// <summary>
        /// Lista filmów (+ podstawowe informacje) o danym tytule i roku produkcji
        /// Pozycje posortowane wg trafności (popularność)
        /// </summary>
        /// <param name="title">Tytuł filmu</param>
        /// <param name="year">Rok produkcji</param>
        /// <returns>Lista filmów wraz z podstawowymi informacjami</returns>
        public List<Movie> GetMovieList(string title, int year)
        {
            return (List<Movie>)this.ApiHelper.GetItemsList(title, "film", false, year);
        }

        /// <summary>
        /// Lista ID filmów o danym tytule. Pozycje posortowane wg trafności (popularność)
        /// </summary>
        /// <param name="title">Tytuł filmu</param>
        /// <returns>Lista ID filmów</returns>
        public List<int> GetMovieIdList(String title)
        {
            return this.ApiHelper.GetItemsList(title, "film", true).Select(film => film.Id).ToList();
        }

        /// <summary>
        /// Lista ID filmów o danym tytule. Pozycje posortowane wg trafności (popularność)
        /// </summary>
        /// <param name="title">Tytuł filmu</param>
        /// <param name="year">Rok produkcji</param>
        /// <returns>Lista ID filmów</returns>
        public List<int> GetMovieIdList(string title, int year)
        {
            return this.ApiHelper.GetItemsList(title, "film", true, year).Select(film => film.Id).ToList();
        }

        /// <summary>
        /// Lista seriali (+ podstawowe informacje) o danym tytule. 
        /// Pozycje posortowane wg trafności (popularność).
        /// </summary>
        /// <param name="title">Tytuł serialu</param>
        /// <returns>Lista seriali wraz z podstawowymi informacjami</returns>
        public List<Series> GetSeriesList(string title)
        {
            return (List<Series>)this.ApiHelper.GetItemsList(title, "serial", false);
        }

        /// <summary>
        /// Lista seriali (+ podstawowe informacje) o danym tytule i roku produkcji
        /// </summary>
        /// <param name="title">Tytuł serialu</param>
        /// <param name="year"> Rok produkcji</param>
        /// <returns>Lista seriali wraz z podstawowymi informacjami.</returns>
        public List<Series> GetSeriesList(string title, int year)
        {
            return (List<Series>)this.ApiHelper.GetItemsList(title, "serial", false, year);
        }

        /// <summary>
        /// Lista ID seriali o danym tytule. Pozycje posortowane wg trafności (popularność)
        /// </summary>
        /// <param name="title">Tytuł serialu</param>
        /// <returns>Lista ID seriali</returns>
        public List<int> GetSeriesIdList(string title)
        {
            return (List<int>)this.ApiHelper.GetItemsList(title, "serial", true);
        }
        
        /// <summary>
        /// Lista ID seriali o danym tytule.
        /// Pozycje posortowane wg trafności (popularność).
        /// </summary>
        /// <param name="title">Tytuł serialu</param>
        /// <param name="year"></param>
        /// <returns>Lista ID seriali</returns>
        public List<int> GetSeriesIdList(string title, int year)
        {
            return (List<int>)this.ApiHelper.GetItemsList(title, "serial", true, year);
        }

        #endregion

        public FilmWebApi(ApiHelper apiHelper)
        {
            ApiHelper = apiHelper;
            apiHelper.FilmWebApi = this;
        }
    }
}
