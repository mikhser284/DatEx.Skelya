using DatEx.Skelya.GUI.CustomCtrls.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace DatEx.Skelya.GUI.CustomCtrls.Controls
{
    public enum ETimeIntervalUnit
    {
        [Description("мин.")]
        Minutes,
        
        [Description("ч.")]
        Hours,
        
        [Description("д.")]
        Days
    }

    public class ValConverter_ETimeIntervalUnits_String : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => EnumHelper.GetDescription((ETimeIntervalUnit)value);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => EnumHelper.GetValue<ETimeIntervalUnit>((String)value);
    }
}
