using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yorgi.FilmWebApi.Models;

namespace Yorgi.FilmWebApi
{
    public interface IFilmWebApi
    {
        /// <summary>
        /// Lista filmów (+ podstawowe informacje) o danym tytule i roku produkcji
        /// Pozycje posortowane wg trafnoœci (popularnoœæ)
        /// </summary>
        /// <param name="title">Tytu³ filmu</param>
        /// <param name="year">Rok produkcji</param>
        /// <returns>Lista filmów wraz z podstawowymi informacjami</returns>
        Task<List<Movie>> GetMovieList(string title, int? year);


        /// <summary>
        /// Lista ID filmów o danym tytule. Pozycje posortowane wg trafnoœci (popularnoœæ)
        /// </summary>
        /// <param name="title">Tytu³ filmu</param>
        /// <param name="year">Rok produkcji</param>
        /// <returns>Lista ID filmów</returns>
        Task<List<int>> GetMovieIdList(string title, int? year);

        /// <summary>
        /// Lista seriali (+ podstawowe informacje) o danym tytule i roku produkcji
        /// </summary>
        /// <param name="title">Tytu³ serialu</param>
        /// <param name="year"> Rok produkcji</param>
        /// <returns>Lista seriali wraz z podstawowymi informacjami.</returns>
        Task<List<Series>> GetSeriesList(string title, int? year);

        /// <summary>
        /// Lista ID seriali o danym tytule.
        /// Pozycje posortowane wg trafnoœci (popularnoœæ).
        /// </summary>
        /// <param name="title">Tytu³ serialu</param>
        /// <param name="year"></param>
        /// <returns>Lista ID seriali</returns>
        Task<List<int>> GetSeriesIdList(string title, int? year);
    }
}