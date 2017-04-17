// File created by Bartosz Nowak on 20/11/2014 21:33

using System.Windows;
using System.Windows.Input;
using MessageBox = Xceed.Wpf.Toolkit.MessageBox;

namespace MovieOrganiser.Utils
{
    public static class Helper
    {
        public static MessageBoxResult ShowMessageBox(string message, string title, MessageBoxButton button, MessageBoxImage image = MessageBoxImage.None)
        {
            Mouse.OverrideCursor = null;
            var retVal = MessageBox.Show(message, title, button, image);
            Mouse.OverrideCursor = Cursors.Wait;
            return retVal;
        }
    }
}