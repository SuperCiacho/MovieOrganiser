using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace YorgiControls.Utils
{
    public static class TextBoxTools
    {
        public static bool GetNumericOnly(DependencyObject src)
        {
            return (bool)src.GetValue(NumericOnly);
        }

        public static void SetNumericOnly(DependencyObject src, bool value)
        {
            src.SetValue(NumericOnly, value);
        }

        public static DependencyProperty NumericOnly = DependencyProperty.RegisterAttached(
            "NumericOnly",
            typeof(bool), 
            typeof(TextBoxTools), 
            new PropertyMetadata(false, (src, args) =>
            {
                if (src is TextBox)
                {
                    var textBox = src as TextBox;

                    if ((bool)args.NewValue)
                    {
                        textBox.KeyDown += TextBoxOnKeyDown;
                    }
                }
            }));

        private static void TextBoxOnKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !IsDigit(e.Key);
        }

        private static bool IsDigit(Key key)
        {
            bool shiftKey = (Keyboard.Modifiers & ModifierKeys.Shift) != 0;
            bool retVal;
            if (key >= Key.D0 && key <= Key.D9 && !shiftKey)
            {
                retVal = true;
            }
            else
            {
                retVal = key >= Key.NumPad0 && key <= Key.NumPad9;
            }
            return retVal;
        }
    }
}
