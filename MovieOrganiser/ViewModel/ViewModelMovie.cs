using System.Globalization;
using MovieOrganiser.Model;
using System;

namespace MovieOrganiser.ViewModel
{
    public class ViewModelMovie : ViewModelView
    {
        public string PolishTitle { get ; set;}
        public string OriginalTitle { get; set; }
        public string Year { get; set; }
        public string Genre { get; set; }
        public Uri Img { get; set; }
        public char Type { get; set; }

        public Movie Movie { get; set; }

        public ViewModelMovie(Movie movie)
        {            
            this.Movie = movie;
            this.PolishTitle = movie.PolishTitle;
            this.OriginalTitle = movie.Title;
            this.Year = movie.Year.ToString(CultureInfo.InvariantCulture);
            this.Genre = movie.Genre;
            this.Img = movie.CoverUrl ?? new Uri("../Assets/noImg.jpg", UriKind.Relative);
            this.Type = movie is Series ? 'S' : 'F';
        }

        protected ViewModelMovie() { }
    }
}
