// File created by Bartosz Nowak on 20/04/2015 19:58

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Yorgi.FilmWebApi.Models;
using Yorgi.FilmWebApi.Utils;

namespace Yorgi.FilmWebApi
{
    internal class ContentAnalyzer : IContentAnalyzer
    {
        private readonly IApiHelper apiHelper;
        private const string FilmWebUrl = "http://www.filmweb.pl";
        private static readonly Regex FilmId = new Regex("thisFilmId=(\\d+)", RegexOptions.Compiled);
        private static readonly Regex RegexFilmData = new Regex("<a href=\"([^\"]*)\" class=\"hdr hdr-medium hitTitle\"", RegexOptions.Compiled);
        private static readonly Regex RegexSeriesData = new Regex("<a href=\"([^\"]*)\" class=\"hdr hdr-medium hitTitle\">", RegexOptions.Compiled);

        internal ContentAnalyzer(IApiHelper apiHelper)
        {
            this.apiHelper = apiHelper;
        }

        public Task<List<T>> GetItemsList<T>(SearchParameters parameters) where T : Movie
        {
            var url = this.PrepareRequestUrl(parameters);
            return url == null
                ? Task.FromResult(Enumerable.Empty<T>().ToList())
                : this.GetMovieList<T>(url, parameters.QueryType == QueryType.Film);
        }

        private Uri PrepareRequestUrl(SearchParameters parameters)
        {
            Uri uri = null;
            var urlBuilder = new StringBuilder(FilmWebUrl);
            urlBuilder.Append("/search/")
                .Append(parameters.QueryType.ToString().ToLower())
                .Append("?q=");

            try
            {
                var encodedTitle = HttpUtility.UrlEncode(parameters.Title);
                urlBuilder.Append(encodedTitle);
            }
            catch (Exception)
            {
                Logger.Error("B³¹d zwi¹zany z kodowaniem URL.");
                return null;
            }

            if (parameters.Year.HasValue)
            {
                urlBuilder.Append("&startYear=");
                urlBuilder.Append(parameters.Year);
                urlBuilder.Append("&endYear=");
                urlBuilder.Append(parameters.Year);
            }

            try
            {
                var url = urlBuilder.ToString();
                Logger.Debug(url);
                uri = new Uri(url);
            }
            catch (UriFormatException)
            {
                Logger.Error("Problem ze stworzeniem adresu URL");
            }

            return uri;
        }

        private async Task<string> GetHtmlCode(Uri url, string desc)
        {
            string html = null;

            try
            {

                // url = new Uri("http://www.filmweb.pl/film/Exodus%3A+Bogowie+i+kr%C3%B3lowie-2014-659455");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(url).ConfigureAwait(false);
                    var result = response.EnsureSuccessStatusCode();
                    if (result.IsSuccessStatusCode)
                    {
                        html = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                    }
                }
            }
            catch (HttpRequestException e)
            {
                Logger.Exception(e, desc);
            }

            return html;
        }

        private async Task<List<T>> GetMovieList<T>(Uri url, bool isMovieQuery) where  T: Movie
        {
            var html = await this.GetHtmlCode(url, "Lista filmów").ConfigureAwait(false);
            var regex = isMovieQuery ? RegexFilmData : RegexSeriesData;

            var matches = regex.Matches(html);

            if (matches.Count == 0)
            {
                Logger.Information($"Nie znaleziono pasuj¹cych do zapytania adresów URL {(isMovieQuery ? "filmów" : "seriali")}.\nZapytanie: {url}\n");
            }

            var filmList = new List<T>();

            foreach (Match match in matches)
            {
                Uri filmUrl = null;
                try
                {
                    filmUrl = new Uri(FilmWebUrl + match.Groups[1].Value);
                }
                catch (UriFormatException e)
                {
                    Logger.Exception(e, "B³¹d tworzenia URL.");
                }

                var film = Activator.CreateInstance(typeof(T), this.apiHelper) as T;
                film.FilmUrl = filmUrl;
                film.Id = await this.GetMovieId(filmUrl).ConfigureAwait(false);
                await this.apiHelper.GetMovieInfo(film).ConfigureAwait(false);
                filmList.Add(film);
            }

            return filmList;
        }

        /// <summary>
        /// Pobranie identyfkatora filmu podanego w parametrze.
        /// </summary>
        /// <param name="filmUrl">Url filmu.</param>
        /// <returns>Id filmu.</returns>
        private async Task<int> GetMovieId(Uri filmUrl)
        {
            var html = await this.GetHtmlCode(filmUrl, "Film").ConfigureAwait(false);
            var matcher = FilmId.Matches(html);

            if (matcher.Count == 0)
            {
                Logger.Error("Nie znaleziono ID filmu.");
                return 0;
            }

            foreach (Match m in matcher)
            {
                try
                {
                   return int.Parse(m.Groups[1].Value);
                }
                catch (FormatException)
                {
                    Logger.Error("Nieprawid³owy format ID - nie zapisano");
                }
            }

            return 0;
        }
    }
}