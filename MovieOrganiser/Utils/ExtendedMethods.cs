using System.Linq;
using MovieOrganiser.View;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;
using MovieOrganiser.ViewModel;

namespace MovieOrganiser.Utils
{
    public static class ExtendedMethods
    {
        public static List<ViewMovie> ToMovieListView(this IEnumerable<Model.Movie> list)
        {
            var listView = new List<ViewMovie>();

            var listViewModelMovie = list.Select(item => new ViewModel.ViewModelMovie(item));

            Application.Current.Dispatcher.Invoke(() =>
            {
                listView = listViewModelMovie.Select(item => new ViewMovie {DataContext = item}).ToList();
            }, DispatcherPriority.Background);

            return listView;
        }

        public static string GetValue(this Model.TranslationTechnique tt)
        {
            switch(tt)
            {
                case Model.TranslationTechnique.Subtitles: return "Napisy";
                case Model.TranslationTechnique.VoiceOver: return "Lektor";
                case Model.TranslationTechnique.Dubbing: return "Dubbing";
                case Model.TranslationTechnique.Polish: return "PL";
                case Model.TranslationTechnique.MissingSubtitles: return "Brakujące napisy";
                default: return string.Empty;
            }
        }
    }
}
