using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DatEx.Skelya.GUI.CustomCtrls
{
    public class EventFilterCtrl : Control
    {
        static EventFilterCtrl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EventFilterCtrl), new FrameworkPropertyMetadata(typeof(EventFilterCtrl)));
        }
    }
}
