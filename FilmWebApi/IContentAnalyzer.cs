using System.Collections.Generic;
using System.Threading.Tasks;
using Yorgi.FilmWebApi.Models;

namespace Yorgi.FilmWebApi
{
    internal interface IContentAnalyzer
    {
        Task<List<T>> GetItemsList<T>(SearchParameters parameters) where T : Movie;
    }
}