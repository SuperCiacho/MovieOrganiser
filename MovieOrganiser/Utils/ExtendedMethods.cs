using MovieOrganiser.View;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;

namespace MovieOrganiser.Utils
{
    public static class ExtendedMethods
    {
        public static List<ViewMovie> ToMovieListView(this IEnumerable<Model.Movie> list)
        {
            var listView = new List<ViewMovie>();

            foreach (var item in list)
            {
                var viewModel = new ViewModel.ViewModelMovie(item);

                ViewMovie view = null;
                Application.Current.Dispatcher.Invoke(() => view = new ViewMovie { DataContext = viewModel }, DispatcherPriority.Render);
                listView.Add(view);
            }

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
