// File created by Bartosz Nowak on 05/07/2014 00:06

using System;
using System.Globalization;
using Yorgi.FilmWebApi;
using Yorgi.FilmWebApi.Models;

namespace MovieOrganiser.ViewModel
{
    public class MovieViewModel : BaseViewModel
    {
        private const string NoCover = "../Assets/noImg.jpg";

        public MovieViewModel(Movie movie)
        {
            this.Movie = movie;
            this.PolishTitle = movie.PolishTitle;
            this.OriginalTitle = movie.Title;
            this.Year = movie.Year.ToString(CultureInfo.InvariantCulture);
            this.Genre = movie.Genre;
            this.Img = movie.CoverUrl ?? new Uri(NoCover, UriKind.Relative);
            this.Type = movie is Series ? 'S' : 'F';
        }

        protected MovieViewModel() { }

        public string PolishTitle { get; set; }
        public string OriginalTitle { get; set; }
        public string Year { get; set; }
        public string Genre { get; set; }
        public Uri Img { get; set; }
        public char Type { get; set; }
        public Movie Movie { get; set; }
    }
}