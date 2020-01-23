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
using System.Linq;
using DatEx.Skelya.DataModel;

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
            var queryResTrigerful = SkelyaClient.GetEventLogRecords(startTime, endTime, triggerful: true);
            var triggeredEvents = queryResTrigerful.Items;
            var queryRes = SkelyaClient.GetEventLogRecords(startTime, endTime);
            var notTriggeredEvents = queryRes.Items;
            var res = triggeredEvents.UnionBy(notTriggeredEvents, x => x.Id).ToList();

            eventsTable.LoadData(notTriggeredEvents);
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
    }


    public static class Ext_Linq
    {
        /// <summary> Получить элементы множества A которые не входят в множество B </summary>
        /// <typeparam name="TA"> Тип данных множества A </typeparam>
        /// <typeparam name="TB"> Тип данных множества B </typeparam>
        /// <param name="setA"> Множество A </param>
        /// <param name="setB"> Множество B </param>
        /// <param name="comparer"> Функция сравнения елементов </param>
        public static IEnumerable<TA> Except<TA, TB>(this IEnumerable<TA> setA, IEnumerable<TB> setB, Func<TA, TB, bool> comparer)
        {
            return setA.Where(x => setB.Count(y => comparer(x, y)) == 0);
        }

        /// <summary> Получить элементы множества A которые входят в множество B </summary>
        /// <typeparam name="TA"> Тип данных множества A </typeparam>
        /// <typeparam name="TB"> Тип данных множества B </typeparam>
        /// <param name="setA"> Множество A </param>
        /// <param name="setB"> Множество B </param>
        /// <param name="comparer"> Функция сравнения елементов </param>
        public static IEnumerable<TA> Intersect<TA, TB>(this IEnumerable<TA> setA, IEnumerable<TB> setB, Func<TA, TB, bool> comparer)
        {
            return setA.Where(x => setB.Count(y => comparer(x, y)) == 1);
        }

        /// <summary> Получить объединение двух множеств по указанному свойству </summary>
        /// <param name="setA"> Множество A </param>
        /// <param name="setB"> Множество B </param>
        /// <param name="keySelector"> Свойство по которому будет вестись выборка </param>
        /// <returns></returns>
        public static IEnumerable<TSource> UnionBy<TSource, TKey>(this IEnumerable<TSource> setA, IEnumerable<TSource> setB, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey> ();
            foreach(TSource element in setA)
                if(seenKeys.Add(keySelector(element)))
                    yield return element;
            foreach(TSource element in setB)
                if(seenKeys.Add(keySelector(element)))
                    yield return element;
        }

        /// <summary> Получить элементы множества у которых значение указанного свойства уникально </summary>
        /// <param name="source"> Исходное множество </param>
        /// <param name="keySelector"> Свойство по которому будет вестись выборка </param>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey> ();
            foreach(TSource element in source)
                if(element != null && seenKeys.Add(keySelector(element)))
                    yield return element;
        }

        public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
        {
            return source.MinBy(selector, null);
        }

        public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey> comparer)
        {
            if(source == null)
                throw new ArgumentNullException("source");
            if(selector == null)
                throw new ArgumentNullException("selector");
            comparer = comparer ?? Comparer<TKey>.Default;
            using(var sourceIterator = source.GetEnumerator())
            {
                if(!sourceIterator.MoveNext())
                {
                    throw new InvalidOperationException("Sequence contains no elements");
                }
                var min = sourceIterator.Current;
                var minKey = selector (min);
                while(sourceIterator.MoveNext())
                {
                    var candidate = sourceIterator.Current;
                    var candidateProjected = selector (candidate);
                    if(comparer.Compare(candidateProjected, minKey) < 0)
                    {
                        min = candidate;
                        minKey = candidateProjected;
                    }
                }
                return min;
            }
        }

        public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
        {
            return source.MaxBy(selector, null);
        }

        public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey> comparer)
        {
            if(source == null)
                throw new ArgumentNullException("source");
            if(selector == null)
                throw new ArgumentNullException("selector");
            comparer = comparer ?? Comparer<TKey>.Default;
            using(var sourceIterator = source.GetEnumerator())
            {
                if(!sourceIterator.MoveNext())
                {
                    throw new InvalidOperationException("Sequence contains no elements");
                }
                var max = sourceIterator.Current;
                var maxKey = selector (max);
                while(sourceIterator.MoveNext())
                {
                    var candidate = sourceIterator.Current;
                    var candidateProjected = selector (candidate);
                    if(comparer.Compare(candidateProjected, maxKey) > 0)
                    {
                        max = candidate;
                        maxKey = candidateProjected;
                    }
                }
                return max;
            }
        }
    }
}
