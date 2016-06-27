using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace QTSPhoneApp
{
    public class DateConverter : IValueConverter

    {

        public object Convert(object value, Type targetType, object parameter, string language)

        {

            if (value == null) return value;

            var theCurrentDate = value.ToString();
            var values = theCurrentDate.Split('/');
            var month = values[0];
              var year = values[2];
            var retvalue = $"{month}/{year}";
            return retvalue;

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)

        {
            throw new NotImplementedException();
        }

    }
}
