using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yorgi.FilmWebApi.Models;

namespace Yorgi.FilmWebApi
{
    public interface IFilmWebApi
    {
        /// <summary>
        /// Lista film�w (+ podstawowe informacje) o danym tytule i roku produkcji
        /// Pozycje posortowane wg trafno�ci (popularno��)
        /// </summary>
        /// <param name="title">Tytu� filmu</param>
        /// <param name="year">Rok produkcji</param>
        /// <returns>Lista film�w wraz z podstawowymi informacjami</returns>
        Task<List<Movie>> GetMovieList(string title, int? year);


        /// <summary>
        /// Lista ID film�w o danym tytule. Pozycje posortowane wg trafno�ci (popularno��)
        /// </summary>
        /// <param name="title">Tytu� filmu</param>
        /// <param name="year">Rok produkcji</param>
        /// <returns>Lista ID film�w</returns>
        Task<List<int>> GetMovieIdList(string title, int? year);

        /// <summary>
        /// Lista seriali (+ podstawowe informacje) o danym tytule i roku produkcji
        /// </summary>
        /// <param name="title">Tytu� serialu</param>
        /// <param name="year"> Rok produkcji</param>
        /// <returns>Lista seriali wraz z podstawowymi informacjami.</returns>
        Task<List<Series>> GetSeriesList(string title, int? year);

        /// <summary>
        /// Lista ID seriali o danym tytule.
        /// Pozycje posortowane wg trafno�ci (popularno��).
        /// </summary>
        /// <param name="title">Tytu� serialu</param>
        /// <param name="year"></param>
        /// <returns>Lista ID seriali</returns>
        Task<List<int>> GetSeriesIdList(string title, int? year);
    }
}