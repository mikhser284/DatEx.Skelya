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
using DatEx.Skelya.GUI.ViewModel;

namespace DatEx.Skelya.GUI.UserControls
{
    /// <summary>
    /// Interaction logic for EventCommentsCtrl.xaml
    /// </summary>
    public partial class EventCommentsCtrl : UserControl
    {
        public EventCommentsCtrl()
        {
            InitializeComponent();

            Part_CommentsList.ItemsSource = VM_Comment.GetTestCollection();
        }
    }
}
