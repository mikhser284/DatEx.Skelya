using DatEx.Skelya.GUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace DatEx.Skelya.GUI.UserControls
{
    public class LevelConverter : DependencyObject, IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            if (values == null && values.Length < 2) return 100;

            var item = values[0] as VM_EventTreeItem;

            if (values[0].GetType() != typeof(Int32)) return 100;
            if (values[1].GetType() != typeof(Double)) return 100;
            //
            int level = (Int32)values[0];
            double indent = (double)values[1];
            return indent * level;
            //return level * 30;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
