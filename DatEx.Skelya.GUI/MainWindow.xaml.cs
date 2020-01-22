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
        public static SkelyaClient SkelyaClient = null;
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
            SkelyaClient = new SkelyaClient(AppConfig.HttpAddressOf.SkelyaServer);
            SetAppPartsBindings_EventTable(UiPart_EventsTable, UiPart_TimeRange, UiPart_EventsFilter);
            SetAppPartsBindings_ApplicationMenu(UiPart_AppMenu, UiPart_EventsTable);
            SetAppPartsBindings_EventDetails(UiPart_EventDetails, UiPart_EventsTable);
            SetAppPartsBindings_EventComments(UiPart_EventComments, UiPart_EventsTable);
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

        private void SetAppPartsBindings_EventTable(EventsTableCtrl eventsTable, TimeRangeCtrl timeRange, EventFilterCtrl eventsFilter)
        {
            //eventsFilter.AppliedFilterChanged += AppliedFilterChanged;
            ////
            ////
            ////
            //void AppliedFilterChanged(object sender, RoutedPropertyChangedEventArgs<VM_FilterInfo> e)
            //{
            //    VM_FilterInfo appliedFilter = e.NewValue;
            //    eventsTable.AppliedFilter = appliedFilter;
            //    eventsTable.DesiredTimeRangeStart = appliedFilter.TimeFrom;
            //    eventsTable.DesiredTimeRangeEnd = appliedFilter.TimeTill;
            //}

            //
            //
            Binding desiredTimeRangeBinding = new Binding
            {
                Source = timeRange,
                Path = new PropertyPath(nameof(timeRange.TimeRange)),
                Mode = BindingMode.OneWay
            };
            BindingOperations.SetBinding(eventsTable, EventsTableCtrl.DesiredTimeRangeProperty, desiredTimeRangeBinding);
            eventsTable.DesiredTimeRangeChanged += EventsTable_DesiredTimeRangeChanged;
        }

        private void SetAppPartsBindings_EventDetails(EventDetailsCtrl eventDetails, EventsTableCtrl eventsTable)
        {
            Binding selectedEventRecordBinding = new Binding
            {
                Source = eventsTable,
                Path = new PropertyPath(nameof(eventsTable.SelectedEvent)),
                Mode = BindingMode.OneWay
            };
            BindingOperations.SetBinding(eventDetails, EventDetailsCtrl.EventRecordProperty, selectedEventRecordBinding);
        }

        private void SetAppPartsBindings_EventComments(EventCommentsCtrl eventComments, EventsTableCtrl eventsTable)
        {
            Binding selectedEventRecordBinding = new Binding
            {
                Source = eventsTable,
                Path = new PropertyPath(nameof(eventsTable.SelectedEvent)),
                Mode = BindingMode.OneWay
            };
            BindingOperations.SetBinding(eventComments, EventCommentsCtrl.EventRecordProperty, selectedEventRecordBinding);
        }

        private void EventsTable_DesiredTimeRangeChanged(Object sender, RoutedPropertyChangedEventArgs<VM_TimeRange> e)
        {
            if(sender is EventsTableCtrl eventsTable) UpdateEventsTable(eventsTable);
        }

        private void UpdateEventsTable(EventsTableCtrl eventsTable)
        {
            
            if(eventsTable.DesiredTimeRange == null || eventsTable.DesiredTimeRange.Start == null || eventsTable.DesiredTimeRange.End == null) return;
            DateTime startTime = (DateTime)eventsTable.DesiredTimeRange.Start;
            DateTime endTime = (DateTime)eventsTable.DesiredTimeRange.End;
            //
            var queryRes = SkelyaClient.GetEventLogRecords(startTime, endTime);
            var events = queryRes.Items;
            eventsTable.LoadData(events);
        }

        private void SetAppPartsBindings_ApplicationMenu(AppMenuCtrl mnu, EventsTableCtrl eventsTable)
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
                Source = eventsTable,
                Path = new PropertyPath($"{nameof(eventsTable.DesiredTimeRange)}.{nameof(eventsTable.DesiredTimeRange.Start)}"),
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
                Source = eventsTable,
                Path = new PropertyPath($"{nameof(eventsTable.DesiredTimeRange)}.{nameof(eventsTable.DesiredTimeRange.End)}"),
                Converter = new ValConverter_DateTime_String(),
                ConverterParameter = "yyyy.MM.dd-ddd",
                ConverterCulture = new CultureInfo("ru-RU"),
                Mode = BindingMode.OneWay
            };
            BindingOperations.SetBinding(mnu, AppMenuCtrl.DesiredEndTimeProperty, desiredEndTimeBinding);
            ////
            ////
            Binding desiredTimeLengthBinding = new Binding
            {
                Source = eventsTable,
                Path = new PropertyPath($"{nameof(eventsTable.DesiredTimeRange)}.{nameof(eventsTable.DesiredTimeRange.Length)}"),
                Converter = new ValConverter_TimeSpan_String(),
                //ConverterParameter = "yyyy.MM.dd-ddd",
                ConverterCulture = new CultureInfo("ru-RU"),
                Mode = BindingMode.OneWay
            };
            BindingOperations.SetBinding(mnu, AppMenuCtrl.DesiredTimeSpanProperty, desiredTimeLengthBinding);
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
