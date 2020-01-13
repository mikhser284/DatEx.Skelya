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
using DatEx.Skelya.GUI.Commands;

namespace DatEx.Skelya.GUI.UserControls
{
    public partial class AppMenuCtrl : UserControl
    {
        public AppMenuCtrl()
        {
            InitializeComponent();

            //CommandBindings.AddRange(new List<CommandBinding>
            //{
            //    new CommandBinding(AppCommands.ShowAppSettingsDialog, ShowAppSettingsDialog_Executed, ShowAppSettingsDialog_CanExecute),
            //    new CommandBinding(AppCommands.ShowAppAboutDialog, ShowAppAboutDialog_Executed, ShowAppAboutDialog_CanExecute)
            //});
        }

        //private void ShowAppSettingsDialog_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        //{
        //    e.CanExecute = false;
        //}

        //private void ShowAppSettingsDialog_Executed(object sender, ExecutedRoutedEventArgs e)
        //{
        //    MessageBox.Show("Not implemented");
        //}

        //private void ShowAppAboutDialog_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        //{
        //    e.CanExecute = false;
        //}

        //private void ShowAppAboutDialog_Executed(object sender, ExecutedRoutedEventArgs e)
        //{
        //    MessageBox.Show("Not implemented");
        //}
    }
}
