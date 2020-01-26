using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DatEx.Skelya.DataModel;
using DatEx.Skelya.GUI.CustomCtrls.Commands;
using DatEx.Skelya.GUI.CustomCtrls.ViewModel;

namespace DatEx.Skelya.GUI.CustomCtrls.Controls
{
    #region ■■■■■ Base ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    [TemplatePart(Name = nameof(Part_DataTable_dGrid), Type = typeof(DataTable))]
    public partial class EventsTableCtrl : Control
    {
        public EventsTableCtrl()
        {

        }

        static EventsTableCtrl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EventsTableCtrl), new FrameworkPropertyMetadata(typeof(EventsTableCtrl)));
            
            #region ————— Local methods ———————————————————————————————————————————————————————————————————————————————            
            static DependencyProperty RegisterProperty<T>(String propName, T defaultValue, Action<DependencyObject, DependencyPropertyChangedEventArgs> propChangedCallback)
                => DependencyProperty.Register(propName, typeof(T), typeof(EventsTableCtrl), new FrameworkPropertyMetadata(defaultValue, new PropertyChangedCallback(propChangedCallback)));

            static RoutedEvent RegisterEvent<T>(String handlerName)
                => EventManager.RegisterRoutedEvent(handlerName, RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<T>), typeof(EventsTableCtrl));
            #endregion ————— Local methods

            #region ————— Dependency property & routed events registration ————————————————————————————————————————————
            //
            UpdateModeProperty = RegisterProperty<EUpdateMode>(nameof(UpdateMode), EUpdateMode.Automatically, OnDependencyPropChanged_UpdateMode);
            UpdateModeChangedEvent = RegisterEvent<EUpdateMode>(nameof(UpdateModeChanged));
            //
            UpdateIntervalInMinProperty = RegisterProperty<Int32>(nameof(UpdateIntervalInMin), 1, OnDependencyPropChanged_UpdateIntervalInMin);
            UpdateIntervalInMinChangedEvent = RegisterEvent<Int32>(nameof(UpdateIntervalInMinChanged));
            //
            LastUpdateTimeProperty = RegisterProperty<DateTime?>(nameof(LastUpdateTime), default(DateTime?), OnDependencyPropChanged_LastUpdateTime);
            LastUpdateTimeChangedEvent = RegisterEvent<DateTime?>(nameof(LastUpdateTimeChanged));
            //
            NextUpdateTimeProperty = RegisterProperty<DateTime?>(nameof(NextUpdateTime), default(DateTime?), OnDependencyPropChanged_NextUpdateTime);
            NextUpdateTimeChangedEvent = RegisterEvent<DateTime?>(nameof(NextUpdateTimeChanged));
            //
            TimeOfFirstEventProperty = RegisterProperty<DateTime?>(nameof(TimeOfFirstEvent), default(DateTime?), OnDependencyPropChanged_TimeOfFirstEvent);
            TimeOfFirstEventChangedEvent = RegisterEvent<DateTime?>(nameof(TimeOfFirstEventChanged));
            //
            TimeOfLastEventProperty = RegisterProperty<DateTime?>(nameof(TimeOfLastEvent), default(DateTime?), OnDependencyPropChanged_TimeOfLastEvent);
            TimeOfLastEventChangedEvent = RegisterEvent<DateTime?>(nameof(TimeOfLastEventChanged));
            //
            EventsTimeSpanProperty = RegisterProperty<TimeSpan?>(nameof(EventsTimeSpan), default(TimeSpan?), OnDependencyPropChanged_EventsTimeSpan);
            EventsTimeSpanChangedEvent = RegisterEvent<TimeSpan?>(nameof(EventsTimeSpanChanged));
            //
            AppliedFilterProperty = RegisterProperty<VM_FilterInfo>(nameof(AppliedFilter), default(VM_FilterInfo), OnDependencyPropChanged_Filter);
            AppliedFilterChangedEvent = RegisterEvent<VM_FilterInfo>(nameof(AppliedFilterChanged));
            //
            EventsLoadedProperty = RegisterProperty<ObservableCollection<VM_EventLogRecord>>(nameof(EventsLoaded), new ObservableCollection<VM_EventLogRecord>(), OnDependencyPropChanged_EventsLoaded);
            EventsLoadedChangedEvent = RegisterEvent<ObservableCollection<VM_EventLogRecord>>(nameof(EventsLoadedChanged));
            //
            EventsDisplayedProperty = RegisterProperty<ObservableCollection<VM_EventLogRecord>>(nameof(EventsDisplayed), new ObservableCollection<VM_EventLogRecord>(), OnDependencyPropChanged_EventsDisplayed);
            EventsDisplayedChangedEvent = RegisterEvent<ObservableCollection<VM_EventLogRecord>>(nameof(EventsDisplayedChanged));
            //
            SelectedEventProperty = RegisterProperty<VM_EventLogRecord>(nameof(SelectedEvent), default(VM_EventLogRecord), OnDependencyPropChanged_SelectedEvent);
            SelectedEventChangedEvent = RegisterEvent<VM_EventLogRecord>(nameof(SelectedEventChanged));
            //
            DesiredTimeRangeProperty = RegisterProperty<VM_TimeRange>(nameof(DesiredTimeRange), new VM_TimeRange(), OnDependencyPropChanged_DesiredTimeRange);
            DesiredTimeRangeChangedEvent = RegisterEvent<VM_TimeRange>(nameof(DesiredTimeRangeChanged));
            //
            #endregion ————— Dependency property & routed events registration
        }
    }
    #endregion ■■■■■ Base



    #region ■■■■■ ControlParts ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class EventsTableCtrl
    {
        private DataGrid Part_DataTable_dGrid;

        private T FindTemplatePart<T>(String templatePartName) where T : DependencyObject
            => (GetTemplateChild(templatePartName) as T) ?? throw new NullReferenceException(templatePartName);

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            //
            Part_DataTable_dGrid = FindTemplatePart<DataGrid>(nameof(Part_DataTable_dGrid));
            //
            SetUpTemplateParts();
        }

        private void SetUpTemplateParts()
        {
            Binding displayedItemsBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath(nameof(EventsDisplayed)),
                Mode = BindingMode.OneWay
            };
            BindingOperations.SetBinding(Part_DataTable_dGrid, DataGrid.ItemsSourceProperty, displayedItemsBinding);

            Binding selectedItemBinding = new Binding
            {
                Source = Part_DataTable_dGrid,
                Path = new PropertyPath(nameof(Part_DataTable_dGrid.SelectedItem)),
                Mode = BindingMode.OneWay
            };
            BindingOperations.SetBinding(this, EventsTableCtrl.SelectedEventProperty, selectedItemBinding);

            //Grouping by device
            //ICollectionView devices = CollectionViewSource.GetDefaultView(Part_DataTable_dGrid.ItemsSource);
            //if (devices != null && devices.CanGroup == true)
            //{
            //    devices.GroupDescriptions.Clear();
            //    devices.GroupDescriptions.Add(new PropertyGroupDescription("DeviceName"));
            //}
        }
    }
    #endregion ■■■■■ ControlParts



    #region ■■■■■ Properties & Events ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class EventsTableCtrl
    {
        #region ————— UpdateMode ——————————————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty UpdateModeProperty;

        public EUpdateMode UpdateMode
        {
            get => (EUpdateMode)GetValue(UpdateModeProperty);
            set => SetValue(UpdateModeProperty, value);
        }

        private static void OnDependencyPropChanged_UpdateMode(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            EventsTableCtrl ctrl = sender as EventsTableCtrl;
            if (ctrl == null) return;
            EUpdateMode oldValue = (EUpdateMode)e.OldValue;
            EUpdateMode newValue = (EUpdateMode)e.NewValue;
            RoutedPropertyChangedEventArgs<EUpdateMode> args = new RoutedPropertyChangedEventArgs<EUpdateMode>(oldValue, newValue);
            args.RoutedEvent = EventsTableCtrl.UpdateModeChangedEvent;
            ctrl.RaiseEvent(args);
        }

        public static readonly RoutedEvent UpdateModeChangedEvent;

        public event RoutedPropertyChangedEventHandler<EUpdateMode> UpdateModeChanged
        {
            add => AddHandler(UpdateModeChangedEvent, value);
            remove => RemoveHandler(UpdateModeChangedEvent, value);
        }
        #endregion ————— UpdateMode

        #region ————— UpdateIntervalInMin —————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty UpdateIntervalInMinProperty;

        public Int32 UpdateIntervalInMin
        {
            get => (Int32)GetValue(UpdateIntervalInMinProperty);
            set => SetValue(UpdateIntervalInMinProperty, value);
        }

        private static void OnDependencyPropChanged_UpdateIntervalInMin(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            EventsTableCtrl ctrl = sender as EventsTableCtrl;
            if (ctrl == null) return;
            Int32 oldValue = (Int32)e.OldValue;
            Int32 newValue = (Int32)e.NewValue;
            RoutedPropertyChangedEventArgs<Int32> args = new RoutedPropertyChangedEventArgs<Int32>(oldValue, newValue);
            args.RoutedEvent = EventsTableCtrl.UpdateIntervalInMinChangedEvent;
            ctrl.RaiseEvent(args);
        }

        public static readonly RoutedEvent UpdateIntervalInMinChangedEvent;

        public event RoutedPropertyChangedEventHandler<Int32> UpdateIntervalInMinChanged
        {
            add => AddHandler(UpdateIntervalInMinChangedEvent, value);
            remove => RemoveHandler(UpdateIntervalInMinChangedEvent, value);
        }
        #endregion ————— UpdateIntervalInMin

        #region ————— LastUpdateTime ——————————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty LastUpdateTimeProperty;

        public DateTime? LastUpdateTime
        {
            get => (DateTime?)GetValue(LastUpdateTimeProperty);
            set => SetValue(LastUpdateTimeProperty, value);
        }

        private static void OnDependencyPropChanged_LastUpdateTime(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            EventsTableCtrl ctrl = sender as EventsTableCtrl;
            if (ctrl == null) return;
            DateTime? oldValue = (DateTime?)e.OldValue;
            DateTime? newValue = (DateTime?)e.NewValue;
            RoutedPropertyChangedEventArgs<DateTime?> args = new RoutedPropertyChangedEventArgs<DateTime?>(oldValue, newValue);
            args.RoutedEvent = EventsTableCtrl.LastUpdateTimeChangedEvent;
            ctrl.RaiseEvent(args);
        }

        public static readonly RoutedEvent LastUpdateTimeChangedEvent;

        public event RoutedPropertyChangedEventHandler<DateTime?> LastUpdateTimeChanged
        {
            add => AddHandler(LastUpdateTimeChangedEvent, value);
            remove => RemoveHandler(LastUpdateTimeChangedEvent, value);
        }
        #endregion ————— LastUpdateTime

        #region ————— NextUpdateTime ——————————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty NextUpdateTimeProperty;

        public DateTime? NextUpdateTime
        {
            get => (DateTime?)GetValue(NextUpdateTimeProperty);
            set => SetValue(NextUpdateTimeProperty, value);
        }

        private static void OnDependencyPropChanged_NextUpdateTime(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            EventsTableCtrl ctrl = sender as EventsTableCtrl;
            if (ctrl == null) return;
            DateTime? oldValue = (DateTime?)e.OldValue;
            DateTime? newValue = (DateTime?)e.NewValue;
            RoutedPropertyChangedEventArgs<DateTime?> args = new RoutedPropertyChangedEventArgs<DateTime?>(oldValue, newValue);
            args.RoutedEvent = EventsTableCtrl.NextUpdateTimeChangedEvent;
            ctrl.RaiseEvent(args);
        }

        public static readonly RoutedEvent NextUpdateTimeChangedEvent;

        public event RoutedPropertyChangedEventHandler<DateTime?> NextUpdateTimeChanged
        {
            add => AddHandler(NextUpdateTimeChangedEvent, value);
            remove => RemoveHandler(NextUpdateTimeChangedEvent, value);
        }
        #endregion ————— NextUpdateTime

        #region ————— TimeOfFirstEvent ————————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty TimeOfFirstEventProperty;

        public DateTime? TimeOfFirstEvent
        {
            get => (DateTime?)GetValue(TimeOfFirstEventProperty);
            set => SetValue(TimeOfFirstEventProperty, value);
        }

        private static void OnDependencyPropChanged_TimeOfFirstEvent(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            EventsTableCtrl ctrl = sender as EventsTableCtrl;
            if (ctrl == null) return;
            DateTime? oldValue = (DateTime?)e.OldValue;
            DateTime? newValue = (DateTime?)e.NewValue;
            RoutedPropertyChangedEventArgs<DateTime?> args = new RoutedPropertyChangedEventArgs<DateTime?>(oldValue, newValue);
            args.RoutedEvent = EventsTableCtrl.TimeOfFirstEventChangedEvent;
            ctrl.RaiseEvent(args);
        }

        public static readonly RoutedEvent TimeOfFirstEventChangedEvent;

        public event RoutedPropertyChangedEventHandler<DateTime?> TimeOfFirstEventChanged
        {
            add => AddHandler(TimeOfFirstEventChangedEvent, value);
            remove => RemoveHandler(TimeOfFirstEventChangedEvent, value);
        }
        #endregion ————— TimeOfFirstEvent

        #region ————— TimeOfLastEvent —————————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty TimeOfLastEventProperty;

        public DateTime? TimeOfLastEvent
        {
            get => (DateTime?)GetValue(TimeOfLastEventProperty);
            set => SetValue(TimeOfLastEventProperty, value);
        }

        private static void OnDependencyPropChanged_TimeOfLastEvent(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            EventsTableCtrl ctrl = sender as EventsTableCtrl;
            if (ctrl == null) return;
            DateTime? oldValue = (DateTime?)e.OldValue;
            DateTime? newValue = (DateTime?)e.NewValue;
            RoutedPropertyChangedEventArgs<DateTime?> args = new RoutedPropertyChangedEventArgs<DateTime?>(oldValue, newValue);
            args.RoutedEvent = EventsTableCtrl.TimeOfLastEventChangedEvent;
            ctrl.RaiseEvent(args);
        }

        public static readonly RoutedEvent TimeOfLastEventChangedEvent;

        public event RoutedPropertyChangedEventHandler<DateTime?> TimeOfLastEventChanged
        {
            add => AddHandler(TimeOfLastEventChangedEvent, value);
            remove => RemoveHandler(TimeOfLastEventChangedEvent, value);
        }
        #endregion ————— TimeOfLastEvent

        #region ————— EventsTimeSpan ——————————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty EventsTimeSpanProperty;

        public TimeSpan? EventsTimeSpan
        {
            get => (TimeSpan?)GetValue(EventsTimeSpanProperty);
            set => SetValue(EventsTimeSpanProperty, value);
        }

        private static void OnDependencyPropChanged_EventsTimeSpan(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            EventsTableCtrl ctrl = sender as EventsTableCtrl;
            if (ctrl == null) return;
            TimeSpan? oldValue = (TimeSpan?)e.OldValue;
            TimeSpan? newValue = (TimeSpan?)e.NewValue;
            RoutedPropertyChangedEventArgs<TimeSpan?> args = new RoutedPropertyChangedEventArgs<TimeSpan?>(oldValue, newValue);
            args.RoutedEvent = EventsTableCtrl.EventsTimeSpanChangedEvent;
            ctrl.RaiseEvent(args);
        }

        public static readonly RoutedEvent EventsTimeSpanChangedEvent;

        public event RoutedPropertyChangedEventHandler<TimeSpan?> EventsTimeSpanChanged
        {
            add => AddHandler(EventsTimeSpanChangedEvent, value);
            remove => RemoveHandler(EventsTimeSpanChangedEvent, value);
        }
        #endregion ————— EventsTimeSpan

        #region ————— AppliedFilter ———————————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty AppliedFilterProperty;

        public VM_FilterInfo AppliedFilter
        {
            get => (VM_FilterInfo)GetValue(AppliedFilterProperty);
            set => SetValue(AppliedFilterProperty, value);
        }

        private static void OnDependencyPropChanged_Filter(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            EventsTableCtrl ctrl = sender as EventsTableCtrl;
            if(ctrl == null) return;
            VM_FilterInfo oldValue = (VM_FilterInfo)e.OldValue;
            VM_FilterInfo newValue = (VM_FilterInfo)e.NewValue;
            RoutedPropertyChangedEventArgs<VM_FilterInfo> args = new RoutedPropertyChangedEventArgs<VM_FilterInfo>(oldValue, newValue);
            args.RoutedEvent = EventsTableCtrl.AppliedFilterChangedEvent;
            ctrl.RaiseEvent(args);
        }

        public static readonly RoutedEvent AppliedFilterChangedEvent;

        public event RoutedPropertyChangedEventHandler<VM_FilterInfo> AppliedFilterChanged
        {
            add => AddHandler(AppliedFilterChangedEvent, value);
            remove => RemoveHandler(AppliedFilterChangedEvent, value);
        }
        #endregion ————— AppliedFilter

        #region ————— EventsLoaded ————————————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty EventsLoadedProperty;

        public ObservableCollection<VM_EventLogRecord> EventsLoaded
        {
            get => (ObservableCollection<VM_EventLogRecord>)GetValue(EventsLoadedProperty);
            set => SetValue(EventsLoadedProperty, value);
        }

        private static void OnDependencyPropChanged_EventsLoaded(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            EventsTableCtrl ctrl = sender as EventsTableCtrl;
            if (ctrl == null) return;
            ObservableCollection<VM_EventLogRecord> oldValue = (ObservableCollection<VM_EventLogRecord>)e.OldValue;
            ObservableCollection<VM_EventLogRecord> newValue = (ObservableCollection<VM_EventLogRecord>)e.NewValue;
            RoutedPropertyChangedEventArgs<ObservableCollection<VM_EventLogRecord>> args = new RoutedPropertyChangedEventArgs<ObservableCollection<VM_EventLogRecord>>(oldValue, newValue);
            args.RoutedEvent = EventsTableCtrl.EventsLoadedChangedEvent;
            ctrl.RaiseEvent(args);
        }

        public static readonly RoutedEvent EventsLoadedChangedEvent;

        public event RoutedPropertyChangedEventHandler<ObservableCollection<VM_EventLogRecord>> EventsLoadedChanged
        {
            add => AddHandler(EventsLoadedChangedEvent, value);
            remove => RemoveHandler(EventsLoadedChangedEvent, value);
        }
        #endregion ————— EventsLoaded

        #region ————— EventsDisplayed —————————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty EventsDisplayedProperty;

        public ObservableCollection<VM_EventLogRecord> EventsDisplayed
        {
            get => (ObservableCollection<VM_EventLogRecord>)GetValue(EventsDisplayedProperty);
            set => SetValue(EventsDisplayedProperty, value);
        }

        private static void OnDependencyPropChanged_EventsDisplayed(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            EventsTableCtrl ctrl = sender as EventsTableCtrl;
            if (ctrl == null) return;
            ObservableCollection<VM_EventLogRecord> oldValue = (ObservableCollection<VM_EventLogRecord>)e.OldValue;
            ObservableCollection<VM_EventLogRecord> newValue = (ObservableCollection<VM_EventLogRecord>)e.NewValue;
            RoutedPropertyChangedEventArgs<ObservableCollection<VM_EventLogRecord>> args = new RoutedPropertyChangedEventArgs<ObservableCollection<VM_EventLogRecord>>(oldValue, newValue);
            args.RoutedEvent = EventsTableCtrl.EventsDisplayedChangedEvent;
            ctrl.RaiseEvent(args);
        }

        public static readonly RoutedEvent EventsDisplayedChangedEvent;

        public event RoutedPropertyChangedEventHandler<ObservableCollection<VM_EventLogRecord>> EventsDisplayedChanged
        {
            add => AddHandler(EventsDisplayedChangedEvent, value);
            remove => RemoveHandler(EventsDisplayedChangedEvent, value);
        }
        #endregion ————— EventsDisplayed

        #region ————— SelectedEvent ———————————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty SelectedEventProperty;

        public VM_EventLogRecord SelectedEvent
        {
            get => (VM_EventLogRecord)GetValue(SelectedEventProperty);
            set => SetValue(SelectedEventProperty, value);
        }

        private static void OnDependencyPropChanged_SelectedEvent(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            EventsTableCtrl ctrl = sender as EventsTableCtrl;
            if (ctrl == null) return;
            VM_EventLogRecord oldValue = (VM_EventLogRecord)e.OldValue;
            VM_EventLogRecord newValue = (VM_EventLogRecord)e.NewValue;
            RoutedPropertyChangedEventArgs<VM_EventLogRecord> args = new RoutedPropertyChangedEventArgs<VM_EventLogRecord>(oldValue, newValue);
            args.RoutedEvent = EventsTableCtrl.SelectedEventChangedEvent;
            ctrl.RaiseEvent(args);
        }

        public static readonly RoutedEvent SelectedEventChangedEvent;

        public event RoutedPropertyChangedEventHandler<ObservableCollection<VM_EventLogRecord>> SelectedEventChanged
        {
            add => AddHandler(SelectedEventChangedEvent, value);
            remove => RemoveHandler(SelectedEventChangedEvent, value);
        }
        #endregion ————— SelectedEvent

        #region ————— DesiredTimeRange ————————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty DesiredTimeRangeProperty;

        public VM_TimeRange DesiredTimeRange
        {
            get => (VM_TimeRange)GetValue(DesiredTimeRangeProperty);
            set => SetValue(DesiredTimeRangeProperty, value);
        }

        private static void OnDependencyPropChanged_DesiredTimeRange(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            EventsTableCtrl ctrl = sender as EventsTableCtrl;
            if(ctrl == null) return;
            VM_TimeRange oldValue = (VM_TimeRange)e.OldValue;
            VM_TimeRange newValue = (VM_TimeRange)e.NewValue;
            RoutedPropertyChangedEventArgs<VM_TimeRange> args = new RoutedPropertyChangedEventArgs<VM_TimeRange>(oldValue, newValue);
            args.RoutedEvent = EventsTableCtrl.DesiredTimeRangeChangedEvent;
            ctrl.RaiseEvent(args);
        }

        public static readonly RoutedEvent DesiredTimeRangeChangedEvent;

        public event RoutedPropertyChangedEventHandler<VM_TimeRange> DesiredTimeRangeChanged
        {
            add => AddHandler(DesiredTimeRangeChangedEvent, value);
            remove => RemoveHandler(DesiredTimeRangeChangedEvent, value);
        }
        #endregion ————— DesiredTimeRange
    }
    #endregion ■■■■■ Properties & Events



    #region ■■■■■ Class handlers ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class EventsTableCtrl
    {
        #region ————— ClassHandler ————————————————————————————————————————————————————————————————————————————————

        private static void OnTreeItemExpanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem treeViewItem = e.OriginalSource as TreeViewItem;
            if(treeViewItem == null) return;
            treeViewItem.ItemContainerGenerator.StatusChanged += ItemContainerGenerator_StatusChanged;

            void ItemContainerGenerator_StatusChanged(object sender, EventArgs e)
            {
                ItemContainerGenerator generator = sender as ItemContainerGenerator;
                if(generator.Status != GeneratorStatus.ContainersGenerated) return;
                Application.Current.Dispatcher.BeginInvoke(new Action(() => MessageBox.Show("Item expanded")));
                generator.StatusChanged -= ItemContainerGenerator_StatusChanged;
            }
        }

        #endregion ————— ClassHandler
    }
    #endregion ■■■■■ Class handlers



    #region ■■■■■ Commands ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class EventsTableCtrl
    {
        #region ————— CommandName —————————————————————————————————————————————————————————————————————————————————————

        private static void CommandName_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show($"Command {((RoutedUICommand)e.Command).Text} not implemented");
        }

        private static void CommandName_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
        }

        #endregion ————— CommandName
    }
    #endregion ■■■■■ Commands



    #region ■■■■■ Other ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class EventsTableCtrl
    {
        public void LoadData(IEnumerable<EventLogRecord> events)
        {
            ObservableCollection<VM_EventLogRecord> loadedEvents 
                = new ObservableCollection<VM_EventLogRecord>(events.OrderByDescending(x => x.DateTime).Select(x => new VM_EventLogRecord(x)));
            EventsLoaded.Clear();
            EventsDisplayed.Clear();

            foreach(var item in loadedEvents)
            {
                EventsLoaded.Add(item);
                EventsDisplayed.Add(item);
            }
        }
    }
    #endregion ■■■■■ Other
}