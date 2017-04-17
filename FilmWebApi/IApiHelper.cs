using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yorgi.FilmWebApi.Models;

namespace Yorgi.FilmWebApi
{
    public interface IApiHelper
    {
        /// <summary> Ustawienie informacji nt. filmu. </summary>
        /// <param name="movie">Referencja do filmu.</param>
        Task GetMovieInfo(Movie movie);

        /// <summary>
        /// Lista osób danej profesji z danego filmu
        /// </summary>
        /// <param name="filmId">ID filmu</param>
        /// <param name="profession">Nazwa profesji</param>
        /// <returns>Lista osób</returns>
        Task<List<Person>> GetFilmPersons(int filmId, Profession profession);

        /// <summary>
        /// Pobiera opis filmu.
        /// </summary>
        /// <param name="filmId">Identyfikator filmu.</param>
        /// <returns>Opis filmu.</returns>
        Task<string> GetFilmDescription(int filmId);
    }
}