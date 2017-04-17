// File created by Bartosz Nowak on 20/04/2015 19:58

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Yorgi.FilmWebApi.Models;
using Yorgi.FilmWebApi.Utils;

namespace Yorgi.FilmWebApi
{
    internal class Connection
    {
        #region Fields

        private string methodName;

        private const string ApiServer = "https://ssl.filmweb.pl/api?";
        private const string Version = "1.0";
        private const string AppId = "android";
        private const string ApiKey = "qjcGhW2JnvGT9dfCt3uT_jozR3s";

        private const string UrlPerson = "http://1.fwcdn.pl/p";

        #endregion

        private static string CreateSignature(string method)
        {
            var sig = $"{method}\\n{AppId}{ApiKey}";

            var md5 = new MD5CryptoServiceProvider();
            var bytes = Encoding.UTF8.GetBytes(sig);
            bytes = md5.ComputeHash(bytes);
            var signatureBuilder = new StringBuilder($"{Version},");
            foreach (var b in bytes) signatureBuilder.Append(b.ToString("x2").ToLower());

            return signatureBuilder.ToString();
        }

        public async Task<object> PrepareResponse()
        {
            var response = await this.GetResponse().ConfigureAwait(false);
            if (response == null || response == "null") return Task.FromResult((object)null);

            var ra = new ResponseAnalyzer(response);
            object processedResponse = null;

            switch (this.MethodName)
            {
                case "getFilmDescription":
                    processedResponse = ra.Response[0];
                    break;

                case "getFilmPersons":
                    var personList = new List<Person>();
                    var personData = ra.Response;
                    var person = new Person();

                    try
                    {
                        person.Id = int.Parse(personData[0].ToString());
                    }
                    catch (NullReferenceException)
                    {
                        Logger.Error("Brak ID osoby.");
                    }

                    try
                    {
                        person.Role = personData[1].ToString();
                    }
                    catch (NullReferenceException)
                    {
                        Logger.Debug("Brak roli");
                    }

                    try
                    {
                        person.Info = personData[2].ToString();
                    }
                    catch (NullReferenceException)
                    {
                        Logger.Debug("Brak informacji dodatkowych");
                    }

                    person.Name = personData[3].ToString();
                    Uri photoUrl = null;
                    try
                    {
                        photoUrl = new Uri(UrlPerson + personData[4]);
                    }
                    catch (UriFormatException)
                    {
                        Logger.Error("Niepoprawny adres zdjêcia osoby.");
                    }
                    catch (NullReferenceException)
                    {
                        Logger.Debug("Osoba nie posiada zdjêcia.");
                    }

                    person.PhotoUrl = photoUrl;
                    personList.Add(person);


                    processedResponse = personList;
                    break;

                case "getFilmInfoFull":
                    processedResponse = ra.Response;
                    break;
            }

            return processedResponse;
        }

        private async Task<string> GetResponse()
        {
            string output = null;

            if (this.Signature == null) return null;

            try
            {
                var url = this.GetRequestParameters();

                using (var httpClient = new HttpClient())
                {
                    var dataStream = await httpClient.GetAsync(url).ConfigureAwait(false);
                    if (dataStream == null) return null;
                    using (var stream = await dataStream.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    using (TextReader connectionReader = new StreamReader(stream, Encoding.UTF8))
                    {
                        /**
                     * Pierwsza linia jest komunikatem o stanie zapytania, mo¿liwe opcje:
                     * "ok" - pozosta³e linie s¹ wynikiem
                     * "err" - pozosta³a linia zawiera opis b³êdu
                     */
                        var stateStr = connectionReader.ReadLine();
                        var state = stateStr == "ok";

                        var responseBuffer = new StringBuilder();

                        string line;
                        while ((line = connectionReader.ReadLine()) != null) responseBuffer.Append(line);

                        output = responseBuffer.ToString();

                        if (!state)
                        {
                            Logger.Error(output);
                            return null;
                        }

                        if (output == "exc NullPointerException")
                        {
                            Logger.Error("Brak danych");
                            return null;
                        }
                    }
                }
            }

            catch (UriFormatException e)
            {
                Logger.Exception(e);
            }
            catch (IOException e)
            {
                Logger.Exception(e);
            }

            Logger.Debug(output);
            return output;
        }

        private Uri GetRequestParameters()
        {
            if (this.MethodName == null || this.Signature == null) return null;

            var query = new StringBuilder(ApiServer);
            try
            {
                //var encoder = System.Text.Encoding.UTF8;
                query.Append("methods=")
                    .Append(this.MethodSignature)
                    .Append("&signature=")
                    .Append(this.Signature)
                    .Append("&version=").Append(Version)
                    .Append("&appId=").Append(AppId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return null;
            }
            return new Uri(query.ToString());
        }


        #region Properties

        /// <summary>
        /// Sygnatura zapytania
        /// </summary>
        private string Signature { get; set; }

        private string MethodSignature { get; set; }

        public string MethodName
        {
            get { return this.methodName; }
            set
            {
                try
                {
                    //zapis nazwy metody
                    string[] methodParts = value.Split(' ');
                    this.methodName = methodParts[0];
                    this.MethodSignature = value + "\\n";
                    this.Signature = CreateSignature(value);
                }
                catch (Exception)
                {
                    this.methodName = null;
                }
            }
        }

        #endregion
    }
}