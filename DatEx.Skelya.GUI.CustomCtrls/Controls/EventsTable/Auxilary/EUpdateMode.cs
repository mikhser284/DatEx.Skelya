using DatEx.Skelya.GUI.CustomCtrls.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace DatEx.Skelya.GUI.CustomCtrls.Controls
{
    public enum EUpdateMode
    {
        [Description("автоматически")]
        Automatically,

        [Description("ВРУЧНУЮ")]
        Manualy
    }

    public class ValConverter_EUpdateMode_String : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => EnumHelper.GetDescription((EUpdateMode)value);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => EnumHelper.GetValue<EUpdateMode>((String)value);
    }
}
