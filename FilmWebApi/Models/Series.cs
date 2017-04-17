// File created by Bartosz Nowak on 04/10/2014 14:18

using Yorgi.FilmWebApi.Models;

namespace Yorgi.FilmWebApi.Models
{
    public class Series : Movie
    {
        public Series(IApiHelper apiHelper) : base(apiHelper) { }

        /// <summary>Ilość odcinków.</summary>
        public int EpisodesCount { get; set; }

        /// <summary>Ilość sezonów.</summary>
        public int SeasonsCount { get; set; }
    }
}