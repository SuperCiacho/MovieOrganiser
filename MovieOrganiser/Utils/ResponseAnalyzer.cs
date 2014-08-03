using System;
using System.Collections.Generic;
using System.IO;
using MovieOrganiser.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace MovieOrganiser.Utils
{
    public class ResponseAnalyzer
    {
        private readonly object response;

        public ResponseAnalyzer(string response)
        {
            //informacja o czasie generowania + spacja
            var timestamp = response.Substring(response.LastIndexOf(" ", StringComparison.Ordinal));
            response = response.Replace(timestamp, string.Empty);

            this.response = JsonConvert.DeserializeObject<List<object>>(response);
        }

        public object Analyze(string contentType)
        {
            if (contentType == "Description") return ((List<object>)this.response)[0];
            return this.response; 
        }
    }
}
