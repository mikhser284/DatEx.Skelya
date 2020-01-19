using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace DatEx.Skelya.GUI.CustomCtrls
{
    public class ValConverter_DateTime_String : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return " --- ";
            if (value.GetType() != typeof(DateTime)) throw new ArgumentException("Type of value must be DateTime");
            DateTime dateTime = (DateTime)value;
            String format = parameter != null ? "{0:" + parameter.ToString() + "}" :"{0:yyyy.MM.dd-ddd   HH:mm:ss.fff}";
            return String.Format(format, dateTime);
        }            

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
            
    }

    public class ValConverter_TimeSpan_String : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return " --- ";
            if (value.GetType() != typeof(TimeSpan)) throw new ArgumentException("Type of value must be TimeSpan");
            TimeSpan timeSpan = (TimeSpan)value;
            String format = parameter != null ? "0:"+ parameter.ToString() : "{0:ddd} д. {0:hh} ч. {0:mm} м.";
            return String.Format(format, timeSpan);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
