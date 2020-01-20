using DatEx.Skelya.GUI.CustomCtrls.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace DatEx.Skelya.GUI.CustomCtrls.Controls
{
    public enum ETimeIntervalMode
    {
        [Description("по")]
        FromTimeTillTime,
        
        [Description("+")]
        StartTimePlusTimeInterval,

        [Description("-")]
        EndTimeMinusTimeInterval
    }

    public class ValConverter_EDateIntervalMode_String : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => EnumHelper.GetDescription((ETimeIntervalMode)value);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => EnumHelper.GetValue<ETimeIntervalMode>((String)value);
    }
}
