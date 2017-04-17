// File created by Bartosz Nowak on 20/04/2015 19:58

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Yorgi.FilmWebApi
{
    internal class ResponseAnalyzer
    {
        internal List<object> Response { get; }

        public ResponseAnalyzer(string response)
        {
            //informacja o czasie generowania + spacja
            var timestamp = response.Substring(response.LastIndexOf(" ", StringComparison.Ordinal));
            response = response.Replace(timestamp, string.Empty);

            this.Response = JsonConvert.DeserializeObject<List<object>>(response);
        }
    }
}