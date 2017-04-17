// File created by Bartosz Nowak on 19/05/2014 19:38

using System.Windows;
using Yorgi.FilmWebApi.Utils;
using MovieOrganiser.Utils;

namespace MovieOrganiser
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Logger.Exception(e.Exception, "Nieobsłużony wyjątek.");
            Helper.ShowMessageBox(e.Exception.Message, "Wystąpił błąd", MessageBoxButton.OK, MessageBoxImage.Stop);
        }
    }
}