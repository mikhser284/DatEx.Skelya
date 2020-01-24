using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace DatEx.Skelya.GUI.CustomCtrls.Controls
{
    public class ValConverter_EventStatus_Brush : IValueConverter
    {
        private static Dictionary<String, String> Event_Brush = new Dictionary<string, string>
        {
            //{ "information", "headerImg_Criticality_Info" },
            { "warning", "headerImg_Criticality_Warning" },
            { "danger", "headerImg_Criticality_Danger" },
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() != typeof(String)) return null;
            String resourceName;
            if(Event_Brush.TryGetValue((String)value, out resourceName))
                return (DrawingBrush)Application.Current.TryFindResource(resourceName);
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

    }

    public class ValConverter_TriggersCountToBrush : IValueConverter
    {
        private static Dictionary<String, String> Event_Brush = new Dictionary<string, string>
        {
            //{ "information", "headerImg_Criticality_Info" },
            { "warning", "headerImg_Criticality_Warning" },
            { "danger", "headerImg_Criticality_Danger" },
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() != typeof(Int32)) return null;
            Int32 count = (Int32)value;
            if (count < 1) return null;
            return (DrawingBrush)Application.Current.TryFindResource("headerImg_Trigger_True");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

    }
}
