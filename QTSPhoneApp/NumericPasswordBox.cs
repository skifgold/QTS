using System.Text.RegularExpressions;
using Windows.UI.Xaml.Controls;

namespace QTSPhoneApp
{
    public class NumericPasswordBox : TextBox
    {
        public NumericPasswordBox()
        {
            KeyUp += (o, args) => { Text = Regex.Replace(Text, @".", "●"); };
        }
    }
}