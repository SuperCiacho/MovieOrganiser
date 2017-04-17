using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using GalaSoft.MvvmLight.Command;

namespace MovieOrganiser.Converters
{
    public class DropArgumentsConverter : IEventArgsConverter
    {
        public object Convert(object value, object parameter)
        {
            var args = value as DragEventArgs;
            var data = args?.Data;
            if (data?.GetDataPresent(DataFormats.FileDrop) ?? false)
            {
                return (data.GetData(DataFormats.FileDrop) as string[])?[0] ?? string.Empty;
            }

            return string.Empty;
        }
    }
}