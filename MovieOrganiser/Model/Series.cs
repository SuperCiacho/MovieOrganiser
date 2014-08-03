using MovieOrganiser.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieOrganiser.Model
{
    public class Series : Movie
    {
        /**
         * Ilość odcinków
         */
        public int EpisodesCount { get; set; }

        /**
         * Ilość sezonów
         */
        public int SeasonsCount { get; set; }

        public Series(FilmWebApi fah) : base(fah)
        { }
    }

}

