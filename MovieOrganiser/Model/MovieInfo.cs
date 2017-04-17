// File created by Bartosz Nowak on 16/07/2014 20:55

using GalaSoft.MvvmLight;
using Yorgi.FilmWebApi.Models;

namespace MovieOrganiser.Model
{
    public class MovieInfo : ObservableObject
    {
        public string FilePath { get; set; }
        public string Title { get; set; }
        public int? Year { get; set; }
        public MovieType Type { get; set; }
        public string HD { get; set; }
        public TranslationTechnique? TranslationTechinque { get; set; }
    }
}