using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Animation;

namespace DatEx.Skelya.GUI.CustomCtrls.Controls
{
    public class ValConverter_String_AsInitials : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            String val = value as String;
            List<String> nameParts = val is null ? null : val.Split(' ').ToList();
            String resultString = $"{nameParts?.ElementAtOrDefault(0)?.ElementAtOrDefault(0)}{nameParts?.ElementAtOrDefault(1)?.ElementAtOrDefault(0)}";
            String res = String.IsNullOrEmpty(resultString) ? "?" : resultString;
            return res;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
