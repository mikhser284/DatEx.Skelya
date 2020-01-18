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

namespace DatEx.Skelya.GUI.CustomCtrls.Controls
{
    public class ScalesCtrl : Control
    {
        static ScalesCtrl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScalesCtrl), new FrameworkPropertyMetadata(typeof(ScalesCtrl)));
        }
    }
}
