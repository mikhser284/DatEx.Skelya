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
using System.Globalization;

namespace DatEx.Skelya.GUI
{
    public partial class MainWindow : Window
    {
        public static AppSettings AppConfig = null;
        public static SkeliaClient Client = null;
        //
        private static AppMenuCtrl UiPart_AppMenu = null;
        private static EventsTableCtrl UiPart_EventsTable = null;
        private static EventDetailsCtrl UiPart_EventDetails = null;
        private static EventCommentsCtrl UiPart_EventComments = null;
        private static EventsTreeCtrl UiPart_EventsTree = null;
        private static ScalesListCtrl UiPart_ScalesList = null;
        private static EventFilterCtrl UiPart_EventsFilter = null;
        private static TriggersCtrl UiPart_Triggers = null;
        private static TimeRangeCtrl UiPart_TimeRange = null;

        public MainWindow()
        {
            InitializeComponent();
            SetupApplicationComponents();

            CommandBindings.AddRange(new List<CommandBinding>
            {
                new CommandBinding(AppCommands.ShowEventSnapshotDialog, ShowEventSnapshotDialog_Executed, ShowEventSnapshotDialog_CanExecute),
                new CommandBinding(AppCommands.ShowAppSettingsDialog, ShowAppSettingsDialog_Executed, ShowAppSettingsDialog_CanExecute),
                new CommandBinding(AppCommands.ShowAppAboutDialog, ShowAppAboutDialog_Executed, ShowAppAboutDialog_CanExecute),
                new CommandBinding(EventRemarksCommands.AddRemark, AddRemark_Executed, AddRemark_CanExecute),
            });
        }

        private void SetupApplicationComponents()
        {
            AppConfig = AppSettings.Load();
            LoadApplicationParts();
            Client = new SkeliaClient(AppConfig.HttpAddressOf.SkelyaServer);
            SetAppPartsBindings_EventTable(UiPart_EventsFilter, UiPart_EventsTable);
            SetAppPartsBindings_ApplicationMenu(UiPart_AppMenu, UiPart_EventsTable, UiPart_TimeRange);
        }

        private void LoadApplicationParts()
        {
            UiPart_TimeRange = Part_TimeRange_tRange;
            UiPart_AppMenu = Part_AppMenu_appMnu;
            UiPart_EventsTable = Part_EventsTable;
            UiPart_EventsTable.AppliedFilterChanged += UiPart_EventsTable_FilterChanged;
            UiPart_EventDetails = Part_EventDetails;
            UiPart_EventComments = Part_EventComments;
            UiPart_EventsTree = Part_EventsTree;
            UiPart_ScalesList = Part_ScalesList;
            UiPart_EventsFilter = Part_EventsFilter;
            //UiPart_Triggers = Part_Triggers;
        }

        private void SetAppPartsBindings_EventTable(EventFilterCtrl eventsFilter, EventsTableCtrl eventsTable)
        {
            eventsFilter.AppliedFilterChanged += AppliedFilterChanged;
            //
            //
            //
            void AppliedFilterChanged(object sender, RoutedPropertyChangedEventArgs<VM_FilterInfo> e)
            {
                VM_FilterInfo appliedFilter = e.NewValue;
                eventsTable.AppliedFilter = appliedFilter;
                eventsTable.DesiredStartTime = appliedFilter.TimeFrom;
                eventsTable.DesiredEndTime = appliedFilter.TimeTill;
            }
        }

        private void SetAppPartsBindings_ApplicationMenu(AppMenuCtrl mnu, EventsTableCtrl eventsTable, TimeRangeCtrl timeRange)
        {
            Binding updateModeBinding = new Binding
            {
                Source = eventsTable,
                Path = new PropertyPath(nameof(eventsTable.UpdateMode)),
                Converter = new ValConverter_EUpdateMode_String(),
                Mode = BindingMode.OneWay
            };
            BindingOperations.SetBinding(mnu, AppMenuCtrl.UpdateModeProperty, updateModeBinding);
            //
            //
            Binding updateIntervalInMinBinding = new Binding
            {
                Source = eventsTable,
                Path = new PropertyPath(nameof(eventsTable.UpdateIntervalInMin)),
                Mode = BindingMode.OneWay
            };
            BindingOperations.SetBinding(mnu, AppMenuCtrl.UpdateIntervalInMinProperty, updateIntervalInMinBinding);
            //
            //            
            Binding eventsDisplayedBinding = new Binding
            {
                Source = eventsTable,
                Path = new PropertyPath($"{nameof(eventsTable.EventsDisplayed)}.{nameof(eventsTable.EventsDisplayed.Count)}"),
                Mode = BindingMode.OneWay
            };
            BindingOperations.SetBinding(mnu, AppMenuCtrl.EventsDisplayedProperty, eventsDisplayedBinding);
            //
            //
            Binding eventsLoadedBinding = new Binding
            {
                Source = eventsTable,
                Path = new PropertyPath($"{nameof(eventsTable.EventsLoaded)}.{nameof(eventsTable.EventsLoaded.Count)}"),
                Mode = BindingMode.OneWay
            };
            BindingOperations.SetBinding(mnu, AppMenuCtrl.EventsLoadedProperty, eventsLoadedBinding);
            //
            //
            Binding timeOfFirstEventBinding = new Binding
            {
                Source = eventsTable,
                Path = new PropertyPath(nameof(eventsTable.TimeOfFirstEvent)),
                Converter = new ValConverter_DateTime_String(),
                ConverterParameter = "yyyy.MM.dd-ddd   HH:mm",                
                ConverterCulture = new CultureInfo("ru-RU"),
                Mode = BindingMode.OneWay
            };
            BindingOperations.SetBinding(mnu, AppMenuCtrl.TimeOfFirstEventProperty, timeOfFirstEventBinding);
            //
            //
            Binding timeOfLastEventBinding = new Binding
            {
                Source = eventsTable,
                Path = new PropertyPath(nameof(eventsTable.TimeOfLastEvent)),
                Converter = new ValConverter_DateTime_String(),
                ConverterParameter = "yyyy.MM.dd-ddd   HH:mm",
                ConverterCulture = new CultureInfo("ru-RU"),
                Mode = BindingMode.OneWay
            };
            BindingOperations.SetBinding(mnu, AppMenuCtrl.TimeOfLastEventProperty, timeOfLastEventBinding);
            //
            //
            Binding desiredStartTimeBinding = new Binding
            {
                Source = timeRange,
                Path = new PropertyPath(nameof(timeRange.TimeRangeStart)),
                Converter = new ValConverter_DateTime_String(),
                ConverterParameter = "yyyy.MM.dd-ddd",
                ConverterCulture = new CultureInfo("ru-RU"),
                Mode = BindingMode.OneWay
            };
            BindingOperations.SetBinding(mnu, AppMenuCtrl.DesiredStartTimeProperty, desiredStartTimeBinding);
            ////
            ////
            Binding desiredEndTimeBinding = new Binding
            {
                Source = timeRange,
                Path = new PropertyPath(nameof(timeRange.TimeRangeEnd)),
                Converter = new ValConverter_DateTime_String(),
                ConverterParameter = "yyyy.MM.dd-ddd",
                ConverterCulture = new CultureInfo("ru-RU"),
                Mode = BindingMode.OneWay
            };
            BindingOperations.SetBinding(mnu, AppMenuCtrl.DesiredEndTimeProperty, desiredEndTimeBinding);
            ////
            ////
            Binding desiredTimeSpanBinding = new Binding
            {
                Source = timeRange,
                Path = new PropertyPath(nameof(timeRange.TimeRange)),
                Converter = new ValConverter_TimeSpan_String(),
                //ConverterParameter = "yyyy.MM.dd-ddd",
                ConverterCulture = new CultureInfo("ru-RU"),
                Mode = BindingMode.OneWay
            };
            BindingOperations.SetBinding(mnu, AppMenuCtrl.DesiredTimeSpanProperty, desiredTimeSpanBinding);
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
