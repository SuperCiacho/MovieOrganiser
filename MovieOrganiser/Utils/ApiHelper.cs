using MovieOrganiser.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MovieOrganiser.Utils
{
    public class ApiHelper
    {
        #region Fields

        public static string KEY = "qjcGhW2JnvGT9dfCt3uT_jozR3s";
        public static string WWW = "http://www.filmweb.pl";
        public static string API_SERVER = "https://ssl.filmweb.pl/api?";
        public static string URL_PERSON = "http://1.fwcdn.pl/p";
        public static string URL_POSTER = "http://1.fwcdn.pl/po";
        public static string VERSION = "1.0";
        public static string APPID = "android";

        private static readonly Regex RE_FILM_ID = new Regex("<span id=filmId class=hide>(\\d+)</span>");
        private static readonly Regex RE_FILM_DATA = new Regex("<a href=\"([^\"]*)\" class=\"hdr hdr-medium hitTitle\"");
        private static readonly Regex RE_SERIES_DATA = new Regex("<a href=\"([^\"]*)\" class=\"hdr hdr-medium hitTitle\">");

        private string _MethodName;
        public string _MethodSignature;

        #endregion

        #region Properties

        public string MethodName
        {
            get
            {
                return _MethodName;
            }
            internal set
            {
                try
                {
                    //zapis nazwy metody
                    string[] methodParts = value.Split(' ');
                    _MethodName = methodParts[0];
                    MethodSignature = value + "\\n";
                    Signature = CreateSignature(value);
                }
                catch (Exception e)
                {
                    _MethodName = null;
                }
            }
        }
        public string MethodSignature { get; private set; }

        /// <summary>
        /// Sygnatura zapytania
        /// </summary>
        public string Signature { get; private set; }

        /// <summary>
        /// Adres strony, której zawartość będzie analizowana
        /// </summary>
        public Uri Url { get; private set; }

        /// <summary>
        /// Czy z treści mają być pobierane tylko ID
        /// </summary>
        public bool OnlyId { get; private set; }

        /// <summary>
        /// Typ zapytania: film, serial
        /// </summary>
        public string QueryType { get; private set; }

        internal FilmWebApi FilmWebApi { get; set; }

        #endregion

        #region Connection Methods

        private string CreateSignature(string method)
        {
            string sig = string.Format("{0}\\n{1}{2}", method, ApiHelper.APPID, ApiHelper.KEY);

            var md5 = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(sig);
            bytes = md5.ComputeHash(bytes);
            var signatureBuilder = new StringBuilder();
            foreach (byte b in bytes) signatureBuilder.Append(b.ToString("x2").ToLower());

            return ApiHelper.VERSION + "," + signatureBuilder.ToString();
        }

        private Uri GetRequestParameters()
        {
            if (this.MethodName == null || this.Signature == null) return null;

            StringBuilder query = new StringBuilder(API_SERVER);
            try
            {
                //var encoder = System.Text.Encoding.UTF8;
                query.Append("methods=")
                .Append(this.MethodSignature)
                .Append("&signature=")
                .Append(this.Signature)
                .Append("&version=")
                .Append(VERSION)
                .Append("&appId=")
                .Append(APPID);
            }
            catch (Exception ex)
            {
                //LOGGER.error(ex.getMessage());
                return null;
            }
            return new Uri(query.ToString());
        }

        public object PrepareResponse()
        {
            String response = GetResponse();
            if (response == null) return null;

            var ra = new ResponseAnalyzer(response);
            object res = null;
            var list = new List<object>();

            switch (MethodName)
            {

                case "getFilmDescription":
                    res = ra.Analyze("Description");
                    break;

                case "getFilmPersons":
                    list = (List<object>)ra.Analyze(string.Empty);
                    List<Person> personList = new List<Person>();
                    foreach (IEnumerable<object> data in list)
                    {
                        var personData = data.ToList();
                        Person person = new Person();
                        try
                        {
                            person.Id = int.Parse(personData[0].ToString());
                        }
                        catch (NullReferenceException e)
                        {
                            //LOGGER.error("Brak ID osoby", e);
                        }
                        try
                        {
                            person.Role = personData[1].ToString();
                        }
                        catch (NullReferenceException e)
                        {
                            //LOGGER.debug("Brak roli", e);
                        }

                        try
                        {
                            person.Info = personData[2].ToString();
                        }
                        catch (NullReferenceException e)
                        {
                            //LOGGER.debug("Brak informacji dodatkowych", e);
                        }

                        person.Name = personData[3].ToString();
                        Uri photoUrl = null;
                        try
                        {
                            photoUrl = new Uri(URL_PERSON + personData[4].ToString());
                        }
                        catch (UriFormatException e)
                        {
                            //LOGGER.error("Niepoprawny adres zdjęcia osoby", e);
                        }
                        catch (NullReferenceException e)
                        {
                            //LOGGER.debug("Osoba nie posiada zdjęcia", e);
                        }
                        person.PhotoUrl = photoUrl;
                        personList.Add(person);
                    }

                    res = personList;
                    break;

                case "getFilmInfoFull":
                    res = (List<object>)ra.Analyze(string.Empty);
                    break;
            }
            return res;

        }

        private string GetResponse()
        {
            StringBuilder responseBuffer;

            if (Signature == null) return null;

            try
            {
                Uri url = GetRequestParameters();
                var request = WebRequest.Create(url);
                var response = request.GetResponse();

                Stream dataStream = response.GetResponseStream();

                using (TextReader connectionReader = new StreamReader(new BufferedStream(dataStream), Encoding.UTF8))
                {
                    /**
                     * Pierwsza linia jest komunikatem o stanie zapytania, możliwe opcje:
                     * "ok" - pozostałe linie są wynikiem
                     * "err" - pozostała linia zawiera opis błędu
                     */
                    string stateStr = connectionReader.ReadLine();
                    bool state = stateStr == "ok";

                    responseBuffer = new StringBuilder();

                    string line;
                    while ((line = connectionReader.ReadLine()) != null) responseBuffer.Append(line);

                    connectionReader.Close();

                    if (!state)
                    {
                        //LOGGER.error(resp);
                        return null;
                    }
                    else if (responseBuffer.ToString() == "exc NullPointerException")
                    {
                        //LOGGER.error("Brak danych");
                        return null;
                    }
                }
            }

            catch (UriFormatException e)
            {
                //LOGGER.error(e.getMessage(), e);
                return null;
            }
            catch (IOException e)
            {
                //LOGGER.error(e.getMessage(), e);
                return null;
            }
            //LOGGER.debug(resp);
            return responseBuffer.ToString();
        }

        #endregion

        #region ContentAnalyzer

        public bool SetUrlStr(Dictionary<string, object> parameters)
        {
            var urlStr = new StringBuilder(ApiHelper.WWW);
            urlStr.Append("/search/")
            .Append(QueryType)
            .Append("?q=");

            string title;
            try
            {
                title = System.Web.HttpUtility.UrlEncode(((String)parameters["title"]));
                urlStr.Append(title);
            }
            catch (Exception e)
            {
                //LOGGER.error("Błąd związany z kodowaniem URL", e);
                return false;
            }


            var year = parameters.FirstOrDefault(x => x.Key == "year").Value;
            if (year != null)
            {
                urlStr.Append("&startYear=");
                urlStr.Append(year);
                urlStr.Append("&endYear=");
                urlStr.Append(year);
            }
            try
            {
                this.Url = new Uri(urlStr.ToString());
                //LOGGER.debug(urlStr.toString());
            }
            catch (UriFormatException e)
            {
                //LOGGER.error("Problem z ustanowieniem adresu URL", e);
                return false;
            }
            return true;
        }

        public string GetHtmlCode(Uri url, String desc)
        {
            desc = desc != null ? desc + ": " : "";

            var html = new StringBuilder();

            try
            {
                var request = WebRequest.Create(url);
                var response = request.GetResponse();

                using (TextReader connectionReader = new StreamReader(new BufferedStream(response.GetResponseStream()), Encoding.UTF8))
                {
                    string str;
                    while ((str = connectionReader.ReadLine()) != null)
                    {
                        html.Append(str);
                    }
                    connectionReader.Close();
                }
            }
            catch (UriFormatException e)
            {
                desc += "Nieprawidłowy adres URL.";
                //LOGGER.error(desc, e);
            }
            catch (IOException e)
            {
                desc += "Błąd odczytu danych filmu.";
                //LOGGER.error(desc, e);
            }

            return html.ToString();
        }

        public IEnumerable<object> GetItemsList(params object[] parameters)
        {
            if (parameters.Length < 3 || parameters.Length > 4) return null;
            this.QueryType = (string)parameters[1];
            this.OnlyId = (bool)parameters[2];
            var urlParams = new Dictionary<string, object> {{"title", parameters[0]}};
            if (parameters.Length == 4) urlParams.Add("year", parameters[3]);
            return SetUrlStr(urlParams) ? this.GetMovieList() : new List<object>();
        }

        private IEnumerable<object> GetMovieList()
        {
            var html = GetHtmlCode(Url, "Lista filmów");
            var isMovieQuery = this.QueryType == "film";

            Regex exp = isMovieQuery ? RE_FILM_DATA : RE_SERIES_DATA;

            var matcher = exp.Matches(html);
            var allMatches = (from Match match in matcher select match.Groups[1].Value).ToList();

            if (allMatches.Count == 0)
            {
                string gettingObj = string.Format("Nie znaleziono pasujących do zapytania adresów URL {0}.\nZapytanie: {1}\n", this.QueryType == "film" ? "filmów" : "seriali", this.Url);
                //LOGGER.info(gettingObj);
            }

            IEnumerable<Movie> filmList;
            if(isMovieQuery)
                filmList = new List<Movie>();
            else
                filmList = new List<Series>();

            foreach (var i in allMatches)
            {
                Uri filmUrl = null;
                try
                {
                    filmUrl = new Uri(ApiHelper.WWW + i);
                }
                catch (UriFormatException e)
                {
                    //LOGGER.error("Błąd tworzenia URL", e);
                }

                Movie film = isMovieQuery ? new Movie(FilmWebApi) : new Series(FilmWebApi);
                film.FilmUrl = filmUrl;
                this.GetMovieId(ref film);
                if (isMovieQuery) (filmList as List<Movie>).Add(film);
                else (filmList as List<Series>).Add((Series)film);
            }

            if (this.OnlyId)
            {
                return filmList.Select(film => film.Id).Cast<object>().ToList();
            }

            return filmList;
        }

        /// <summary>
        /// Pobranie identyfkatora filmu podanego w parametrze.
        /// </summary>
        /// <param name="film">Film.</param>
        /// <returns>Film z uzupełnionym ID</returns>
        private void GetMovieId(ref Movie film)
        {
            var html = GetHtmlCode(film.FilmUrl, "Film");
            var matcher = RE_FILM_ID.Matches(html);
            foreach (Match m in matcher)
            {
                try
                {
                    film.Id = int.Parse(m.Groups[1].Value);
                }
                catch (FormatException e)
                {
                    //LOGGER.error("Nieprawidłowy format ID - nie zapisano", e);
                }
            }
            //else LOGGER.error("Nie znaleziono ID filmu.");
        }

        #endregion

        #region Non-Api Remote Methods

        /// <summary> Ustawienie informacji nt. filmu. </summary>
        /// <param name="movie">Referencja do filmu.</param>
        public void GetMovieInfo(Movie movie)
        {
            this.MethodName = string.Format("getFilmInfoFull [{0}]", movie.Id);
            if (this.MethodName != null)
            {
                var res = (List<object>)this.PrepareResponse();

                try
                {
                    movie.Title = res[(int)MovieInfoLevel.OriginalTitle].ToString();
                }
                catch (NullReferenceException e)
                {
                    //LOGGER.debug("Tytuł polski jest identyczny z oryginalnym. Przechowywany jako polski.");
                }

                try
                {
                    movie.PolishTitle = res[(int)MovieInfoLevel.PolishTitle].ToString();
                }
                catch (NullReferenceException e)
                {
                    //LOGGER.error("Brak polskiego tytułu", e);
                }

                try
                {
                    movie.Rate = float.Parse(res[(int)MovieInfoLevel.Rate].ToString());
                }
                catch (FormatException e)
                {
                    //LOGGER.error("Nieprawidłowy format oceny filmu.", e);
                }
                catch (NullReferenceException e)
                {
                    //LOGGER.debug("Brak oceny filmu.", e);
                }

                //liczba głosów
                try
                {
                    movie.Votes = Convert.ToInt32(res[(int)MovieInfoLevel.Votes]);
                }
                catch (FormatException e)
                {
                    //LOGGER.error("Nieprawidłowy format liczby głosów filmu.", e);
                }
                catch (NullReferenceException e)
                {
                    //LOGGER.debug("Brak głosów na film.", e);
                }

                //gatunek
                try
                {
                    movie.Genre = res[(int)MovieInfoLevel.Genre].ToString().Replace(",", ", "); ;
                }
                catch (NullReferenceException e)
                {
                    //LOGGER.debug("Brak informacji na temat gatunku.", e);
                }

                //rok produkcji
                try
                {
                    movie.Year = Convert.ToInt32(res[(int)MovieInfoLevel.Year]);
                }
                catch (FormatException e)
                {
                    //LOGGER.error("Nieprawidłowy format roku produkcji.", e);
                }
                catch (NullReferenceException e)
                {
                    //LOGGER.debug("Brak informacji o dacie produkcji.", e);
                }

                //czas trwania
                try
                {
                    movie.Duration = Convert.ToInt32(res[(int)MovieInfoLevel.Duration]);
                }
                catch (FormatException e)
                {
                    //LOGGER.error("Nieprawidłowy format czasu trwania filmu.", e);
                }
                catch (NullReferenceException e)
                {
                    //LOGGER.debug("Brak informacji o czasie trwania filmu.", e);
                }

                //okładka

                try
                {
                    var data = res[(int)MovieInfoLevel.CoverUrl];
                    if (data != null)
                    {
                        movie.CoverUrl = new Uri(ApiHelper.URL_POSTER + data);
                    }
                }
                catch (UriFormatException e)
                {
                    //LOGGER.error("Błąd URL dla okładki filmu.", e);
                }
                catch (NullReferenceException e)
                {
                    //LOGGER.debug("Brak URL okładki filmu.", e);
                }

                //kraj produkcji
                try
                {
                    movie.Countries = res[(int)MovieInfoLevel.Country].ToString(); ;
                }
                catch (NullReferenceException e)
                {
                    //LOGGER.debug("Brak informacji o kraju produkcji.", e);
                }

                //zarys fabuły
                try
                {
                    movie.Plot = (string)res[(int)MovieInfoLevel.Plot];
                }
                catch (NullReferenceException e)
                {
                    ////LOGGER.debug("Brak zarysu fabuły.", e);
                }

                //serial posiada kilka dodatkowych pól
                if (movie is Series)
                {
                    Series series = (Series)movie;

                    //liczba sezonów
                    try
                    {
                        series.SeasonsCount = Convert.ToInt32(res[(int)MovieInfoLevel.NumberOfSeasons]);
                    }
                    catch (FormatException e)
                    {
                        ////LOGGER.error("Nieprawidłowy format liczby sezonów.", e);
                    }
                    catch (NullReferenceException e)
                    {
                        ////LOGGER.debug("Brak informacji o liczbie sezonów.", e);
                    }

                    //liczba odcinków
                    try
                    {
                        series.EpisodesCount = Convert.ToInt32(res[(int)MovieInfoLevel.NumberOfEpisodes]);
                    }
                    catch (FormatException e)
                    {
                        ////LOGGER.error("Nieprawidłowy format liczby odcinków.", e);
                    }
                    catch (NullReferenceException e)
                    {
                        ////LOGGER.debug("Brak informacji o liczbie odcinków.", e);
                    }
                }
            }
        }

        /// <summary>
        /// Lista osób danej profesji z danego filmu
        /// </summary>
        /// <param name="filmId">ID filmu</param>
        /// <param name="profession">Nazwa profesji</param>
        /// <returns>Lista osób</returns>
        public List<Person> GetFilmPersons(int filmId, Profession profession)
        {
            if (filmId <= 0) return null;

            var pageNo = 0;
            var personList = new List<Person>();
            var partPersonList = new List<Person>();
            try
            {
                do
                {
                    this.MethodName = "getFilmPersons [" + filmId + "," + (int)profession + "," + 50 * pageNo + "," + 50 * (1 + pageNo) + "]";
                    if (this.MethodName != null)
                    {
                        partPersonList = (List<Person>)this.PrepareResponse();
                        if (partPersonList.Count > 0)
                        {
                            personList.AddRange(partPersonList);
                        }
                        pageNo++;
                    }
                }
                while (partPersonList.Count == 50); //TODO: Nie jestem pewny poprawności tego warunku
            }
            catch (NullReferenceException e)
            {
                string error = "Brak ludzi o profesji " + profession + " związanych z filmem.";
                //LOGGER.debug(error);
            }
            return personList;
        }

        /// <summary>
        /// Pobiera opis filmu.
        /// </summary>
        /// <param name="filmId">Identyfikator filmu.</param>
        /// <returns>Opis filmu.</returns>
        public String GetFilmDescription(int filmId)
        {
            if (filmId <= 0) return null;
            string desc = "";
            this.MethodName = "getFilmDescription [" + filmId + "]";
            if (this.MethodName != null)
            {
                try
                {
                    desc = PrepareResponse().ToString();
                }
                catch (NullReferenceException e)
                {
                    //LOGGER.error("Brak danych opisu filmu", e);
                }
            }
            return desc;
        }

        #endregion
    }
}
