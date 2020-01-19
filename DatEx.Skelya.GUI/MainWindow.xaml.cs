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
using System.Windows.Shapes;
using DatEx.Skelya.GUI.CustomCtrls;
using DatEx.Skelya.GUI.CustomCtrls.Commands;
using DatEx.Skelya.GUI.CustomCtrls.Controls;
using DatEx.Skelya.GUI.CustomCtrls.ViewModel;
using DatEx.Skelya.GUI.CustomCtrls.Dialogs;
using DatEx.Skelya.GUI.CustomCtrls.Windows;

namespace DatEx.Skelya.GUI
{
    public partial class MainWindow : Window
    {
        public static AppSettings AppConfig = null;
        public static SkeliaClient Client = null;
        //
        private static EventsTableCtrl UiPart_EventsTable = null;
        //private static EventDetailsCtrl UiPart_EventDetails = null;
        //private static EventCommentsCtrl UiPart_EventComments = null;
        //private static EventsTreeCtrl UiPart_EventsTree = null;
        //private static ScalesListCtrl UiPart_ScalesList = null;
        private static EventFilterCtrl UiPart_EventsFilter = null;
        //private static TriggersCtrl UiPart_Triggers = null;

        public MainWindow()
        {
            InitializeComponent();
            //
            AppConfig = AppSettings.Load();
            Client = new SkeliaClient(AppConfig.HttpAddressOf.SkelyaServer);
            //
            UiPart_EventsTable = Part_EventsTable;
            UiPart_EventsTable.FilterChanged += UiPart_EventsTable_FilterChanged;
            //UiPart_EventDetails = Part_EventDetails;
            //UiPart_EventComments = Part_EventComments;
            //UiPart_EventsTree = Part_EventsTree;
            //UiPart_ScalesList = Part_ScalesList;
            UiPart_EventsFilter = Part_EventsFilter;
            UiPart_EventsFilter.AppliedFilterChanged += UiPart_EventsFilter_AppliedFilterChanged;

            //UiPart_Triggers = Part_Triggers;
            
            CommandBindings.AddRange(new List<CommandBinding>
            {
                new CommandBinding(AppCommands.ShowEventSnapshotDialog, ShowEventSnapshotDialog_Executed, ShowEventSnapshotDialog_CanExecute),
                new CommandBinding(AppCommands.ShowAppSettingsDialog, ShowAppSettingsDialog_Executed, ShowAppSettingsDialog_CanExecute),
                new CommandBinding(AppCommands.ShowAppAboutDialog, ShowAppAboutDialog_Executed, ShowAppAboutDialog_CanExecute),
                //
                new CommandBinding(EventFilterCommands.ApplyFilter, ApplyFilter_Executed, ApplyFilter_CanExecute),
                //
                new CommandBinding(EventRemarksCommands.AddRemark, AddRemark_Executed, AddRemark_CanExecute),
            });
        }

        private void UiPart_EventsFilter_AppliedFilterChanged(object sender, RoutedPropertyChangedEventArgs<VM_FilterInfo> e)
        {
            UiPart_EventsTable.Filter = e.NewValue;
        }

        private void UiPart_EventsTable_FilterChanged(Object sender, RoutedPropertyChangedEventArgs<VM_FilterInfo> e)
        {
            //MessageBox.Show("Filter changed");
        }

        private void ShowEventSnapshotDialog_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ShowEventSnapshotDialog_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CurrentSnapshotWnd snapshotWnd = new CurrentSnapshotWnd();
            snapshotWnd.Show();
        }

        private void ShowAppSettingsDialog_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ShowAppSettingsDialog_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SettingsDlg settingsDlg = new SettingsDlg();
            if (settingsDlg.ShowDialog() != true) return;
        }

        private void ShowAppAboutDialog_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ShowAppAboutDialog_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AboutDlg aboutDlg = new AboutDlg();
            if (aboutDlg.ShowDialog() != true) return;
        }

        private void ApplyFilter_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ApplyFilter_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Not implemented");
        }

        private void AddRemark_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void AddRemark_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AddRemarkDlg remarkDlg = new AddRemarkDlg();
            remarkDlg.Owner = this;
            remarkDlg.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            if (remarkDlg.ShowDialog() != true) return;
        }
    }
}
