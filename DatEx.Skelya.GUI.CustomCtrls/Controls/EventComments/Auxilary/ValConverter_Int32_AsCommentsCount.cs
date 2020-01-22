using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using DatEx.Skelya.GUI.CustomCtrls.ViewModel;

namespace DatEx.Skelya.GUI.CustomCtrls.Controls
{
    public class ValConverter_Int32_AsCommentsCount : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Int32 count = (Int32)value;
            //if(value.GetType() != typeof(Int32)) throw new ArgumentException("Type of value must be Int32");            
            return count == 0 ? "отсутствуют" : $"({count} шт.)";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
