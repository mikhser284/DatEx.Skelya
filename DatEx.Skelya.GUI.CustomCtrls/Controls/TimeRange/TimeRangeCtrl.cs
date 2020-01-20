using DatEx.Skelya.GUI.CustomCtrls.Helpers;
using DatEx.Skelya.GUI.CustomCtrls.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DatEx.Skelya.GUI.CustomCtrls.Controls
{
    #region ■■■■■ Base ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    [TemplatePart(Name = nameof(Part_TimeStart_dPicker), Type = typeof(DatePicker))]
    [TemplatePart(Name = nameof(Part_TimeEnd_dPicker), Type = typeof(DatePicker))]
    [TemplatePart(Name = nameof(Part_TimeSelectionMode_cBox), Type = typeof(ComboBox))]
    [TemplatePart(Name = nameof(Part_TimeSpan_tBox), Type = typeof(TextBox))]
    [TemplatePart(Name = nameof(Part_TimeSpanUnit_cBox), Type = typeof(ComboBox))]
    public partial class TimeRangeCtrl : Control
    {
        public TimeRangeCtrl() { UpdateTimeRange(); }

        static TimeRangeCtrl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TimeRangeCtrl), new FrameworkPropertyMetadata(typeof(TimeRangeCtrl)));

            #region ————— Local methods ———————————————————————————————————————————————————————————————————————————————            
            static DependencyProperty RegisterProperty<T>(String propName, T defaultValue, Action<DependencyObject, DependencyPropertyChangedEventArgs> propChangedCallback)
                => DependencyProperty.Register(propName, typeof(T), typeof(TimeRangeCtrl), new FrameworkPropertyMetadata(defaultValue, new PropertyChangedCallback(propChangedCallback)));

            static DependencyProperty RegisterPropertyWithoutCallback<T>(String propName, T defaultValue)
                => DependencyProperty.Register(propName, typeof(T), typeof(TimeRangeCtrl), new FrameworkPropertyMetadata(defaultValue));

            static RoutedEvent RegisterEvent<T>(String handlerName)
                => EventManager.RegisterRoutedEvent(handlerName, RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<T>), typeof(TimeRangeCtrl));
            #endregion ————— Local methods

            #region ————— Dependency property & routed events registration ————————————————————————————————————————————
            //
            TimeProperty = RegisterProperty<DateTime?>(nameof(Time), default(DateTime?), OnDependencyPropChanged_Time);
            TimeChangedEvent = RegisterEvent<DateTime?>(nameof(TimeChanged));
            //
            TimeEndProperty = RegisterProperty<DateTime?>(nameof(TimeEnd), default(DateTime?), OnDependencyPropChanged_TimeEnd);
            TimeEndChangedEvent = RegisterEvent<DateTime?>(nameof(TimeEndChanged));
            //
            TimeSelectionModeProperty = RegisterProperty<ETimeIntervalMode>(nameof(TimeSelectionMode), ETimeIntervalMode.TimeMinusTimeInterval, OnDependencyPropChanged_TimeSelectionMode);
            TimeSelectionModeChangedEvent = RegisterEvent<ETimeIntervalMode>(nameof(TimeSelectionModeChanged));
            //
            TimePeriodProperty = RegisterProperty<Int32>(nameof(TimePeriod), 1, OnDependencyPropChanged_TimePeriod);
            TimePeriodChangedEvent = RegisterEvent<Int32>(nameof(TimePeriodChanged));
            //
            TimePeriodUnitProperty = RegisterProperty<ETimeIntervalUnits>(nameof(TimePeriodUnit), ETimeIntervalUnits.Days, OnDependencyPropChanged_TimePeriodUnit);
            TimePeriodUnitChangedEvent = RegisterEvent<ETimeIntervalUnits>(nameof(TimePeriodUnitChanged));
            //
            TimeRangeProperty = RegisterPropertyWithoutCallback<TimeSpan>(nameof(TimeRange), TimeSpan.FromDays(1));
            TimeRangeStartProperty = RegisterPropertyWithoutCallback<DateTime?>(nameof(TimeRangeStart), default(DateTime?));
            TimeRangeEndProperty = RegisterPropertyWithoutCallback<DateTime?>(nameof(TimeRangeEnd), default(DateTime?));
            TimeRangeIsFixedProperty = RegisterPropertyWithoutCallback<Boolean>(nameof(TimeRangeIsFixed), false);
            //
            #endregion ————— Dependency property & routed events registration
        }
    }
    #endregion ■■■■■ Base



    #region ■■■■■ ControlParts ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class TimeRangeCtrl
    {
        private DatePicker Part_TimeStart_dPicker;
        private DatePicker Part_TimeEnd_dPicker;
        private ComboBox Part_TimeSelectionMode_cBox;
        private TextBox Part_TimeSpan_tBox;
        private ComboBox Part_TimeSpanUnit_cBox;

        private T FindTemplatePart<T>(String templatePartName) where T : DependencyObject
            => (GetTemplateChild(templatePartName) as T) ?? throw new NullReferenceException(templatePartName);

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            //
            Part_TimeStart_dPicker = FindTemplatePart<DatePicker>(nameof(Part_TimeStart_dPicker));
            Part_TimeEnd_dPicker = FindTemplatePart<DatePicker>(nameof(Part_TimeEnd_dPicker));
            Part_TimeSelectionMode_cBox = FindTemplatePart<ComboBox>(nameof(Part_TimeSelectionMode_cBox));
            Part_TimeSpan_tBox = FindTemplatePart<TextBox>(nameof(Part_TimeSpan_tBox));
            Part_TimeSpanUnit_cBox = FindTemplatePart<ComboBox>(nameof(Part_TimeSpanUnit_cBox));
            //
            SetUpTemplateParts();
        }

        private void SetUpTemplateParts()
        {
            Binding timeStartBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath(nameof(Time)),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            BindingOperations.SetBinding(Part_TimeStart_dPicker, DatePicker.SelectedDateProperty, timeStartBinding);
            //
            //
            Binding timeEndBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath(nameof(TimeEnd)),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            BindingOperations.SetBinding(Part_TimeEnd_dPicker, DatePicker.SelectedDateProperty, timeEndBinding);
            //
            //
            Part_TimeSelectionMode_cBox.ItemsSource = EnumHelper.GetAllValues<ETimeIntervalMode>();
            Binding timeSelectionModeBinding = new Binding
            {
                 Source = this,
                 Path = new PropertyPath(nameof(TimeSelectionMode)),
                 Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            BindingOperations.SetBinding(Part_TimeSelectionMode_cBox, ComboBox.SelectedValueProperty, timeSelectionModeBinding);
            //
            //
            Binding timeSpanBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath(nameof(TimePeriod)),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            BindingOperations.SetBinding(Part_TimeSpan_tBox, TextBox.TextProperty, timeSpanBinding);
            //
            //
            Part_TimeSpanUnit_cBox.ItemsSource = EnumHelper.GetAllValues<ETimeIntervalUnits>();
            Binding timeSpanUnitBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath(nameof(TimePeriodUnit)),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            BindingOperations.SetBinding(Part_TimeSpanUnit_cBox, ComboBox.SelectedValueProperty, timeSpanUnitBinding);
            
        }
    }
    #endregion ■■■■■ ControlParts



    #region ■■■■■ Properties & Events ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class TimeRangeCtrl
    {
        #region ————— TimeStart ———————————————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty TimeProperty;

        public DateTime? Time
        {
            get => (DateTime?)GetValue(TimeProperty);
            set => SetValue(TimeProperty, value);
        }

        private static void OnDependencyPropChanged_Time(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TimeRangeCtrl ctrl = sender as TimeRangeCtrl;
            if(ctrl == null) return;
            DateTime? oldValue = (DateTime?)e.OldValue;
            DateTime? newValue = (DateTime?)e.NewValue;
            RoutedPropertyChangedEventArgs<DateTime?> args = new RoutedPropertyChangedEventArgs<DateTime?>(oldValue, newValue);
            args.RoutedEvent = TimeRangeCtrl.TimeChangedEvent;
            ctrl.UpdateTimeRange();
            ctrl.RaiseEvent(args);
        }

        public static readonly RoutedEvent TimeChangedEvent;

        public event RoutedPropertyChangedEventHandler<DateTime?> TimeChanged
        {
            add => AddHandler(TimeChangedEvent, value);
            remove => RemoveHandler(TimeChangedEvent, value);
        }
        #endregion ————— TimeStart

        #region ————— TimeEnd —————————————————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty TimeEndProperty;

        public DateTime? TimeEnd
        {
            get => (DateTime?)GetValue(TimeEndProperty);
            set => SetValue(TimeEndProperty, value);
        }

        private static void OnDependencyPropChanged_TimeEnd(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TimeRangeCtrl ctrl = sender as TimeRangeCtrl;
            if(ctrl == null) return;
            DateTime? oldValue = (DateTime?)e.OldValue;
            DateTime? newValue = (DateTime?)e.NewValue;
            RoutedPropertyChangedEventArgs<DateTime?> args = new RoutedPropertyChangedEventArgs<DateTime?>(oldValue, newValue);
            args.RoutedEvent = TimeRangeCtrl.TimeEndChangedEvent;
            ctrl.UpdateTimeRange();
            ctrl.RaiseEvent(args);
        }

        public static readonly RoutedEvent TimeEndChangedEvent;

        public event RoutedPropertyChangedEventHandler<DateTime?> TimeEndChanged
        {
            add => AddHandler(TimeEndChangedEvent, value);
            remove => RemoveHandler(TimeEndChangedEvent, value);
        }
        #endregion ————— TimeEnd

        #region ————— TimeSelectionMode ———————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty TimeSelectionModeProperty;

        public ETimeIntervalMode TimeSelectionMode
        {
            get => (ETimeIntervalMode)GetValue(TimeSelectionModeProperty);
            set => SetValue(TimeSelectionModeProperty, value);
        }

        private static void OnDependencyPropChanged_TimeSelectionMode(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TimeRangeCtrl ctrl = sender as TimeRangeCtrl;
            if(ctrl == null) return;
            ETimeIntervalMode oldValue = (ETimeIntervalMode)e.OldValue;
            ETimeIntervalMode newValue = (ETimeIntervalMode)e.NewValue;
            RoutedPropertyChangedEventArgs<ETimeIntervalMode> args = new RoutedPropertyChangedEventArgs<ETimeIntervalMode>(oldValue, newValue);
            args.RoutedEvent = TimeRangeCtrl.TimeSelectionModeChangedEvent;            
            ChangePartsVisibility(newValue, ctrl);
            ctrl.UpdateTimeRange();
            ctrl.RaiseEvent(args);

            void ChangePartsVisibility(ETimeIntervalMode mode, TimeRangeCtrl ctrl)
            {
                switch( mode)
                {
                    case ETimeIntervalMode.TimeMinusTimeInterval:
                        {
                            ctrl.Part_TimeEnd_dPicker.Visibility = Visibility.Collapsed;
                            ctrl.Part_TimeSpan_tBox.Visibility = Visibility.Visible;
                            ctrl.Part_TimeSpanUnit_cBox.Visibility = Visibility.Visible;
                            break;
                        }
                    case ETimeIntervalMode.FromTimeTillTime:
                        {
                            ctrl.Part_TimeEnd_dPicker.Visibility = Visibility.Visible;
                            ctrl.Part_TimeSpan_tBox.Visibility = Visibility.Collapsed;
                            ctrl.Part_TimeSpanUnit_cBox.Visibility = Visibility.Collapsed;
                            break;
                        }
                    case ETimeIntervalMode.TimePlusTimeInterval:
                        {
                            ctrl.Part_TimeEnd_dPicker.Visibility = Visibility.Collapsed;
                            ctrl.Part_TimeSpan_tBox.Visibility = Visibility.Visible;
                            ctrl.Part_TimeSpanUnit_cBox.Visibility = Visibility.Visible;
                            break;
                        }
                }
            }
        }

        public static readonly RoutedEvent TimeSelectionModeChangedEvent;

        public event RoutedPropertyChangedEventHandler<ETimeIntervalMode> TimeSelectionModeChanged
        {
            add => AddHandler(TimeSelectionModeChangedEvent, value);
            remove => RemoveHandler(TimeSelectionModeChangedEvent, value);
        }
        #endregion ————— TimeSelectionMode

        #region ————— TimeSpan ————————————————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty TimePeriodProperty;

        public Int32 TimePeriod
        {
            get => (Int32)GetValue(TimePeriodProperty);
            set => SetValue(TimePeriodProperty, value);
        }

        private static void OnDependencyPropChanged_TimePeriod(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TimeRangeCtrl ctrl = sender as TimeRangeCtrl;
            if(ctrl == null) return;
            Int32 oldValue = (Int32)e.OldValue;
            Int32 newValue = (Int32)e.NewValue;
            RoutedPropertyChangedEventArgs<Int32> args = new RoutedPropertyChangedEventArgs<Int32>(oldValue, newValue);
            args.RoutedEvent = TimeRangeCtrl.TimePeriodChangedEvent;
            ctrl.UpdateTimeRange();
            ctrl.RaiseEvent(args);
        }

        public static readonly RoutedEvent TimePeriodChangedEvent;

        public event RoutedPropertyChangedEventHandler<Int32> TimePeriodChanged
        {
            add => AddHandler(TimePeriodChangedEvent, value);
            remove => RemoveHandler(TimePeriodChangedEvent, value);
        }
        #endregion ————— TimeSpan

        #region ————— TimeSpanUnit ————————————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty TimePeriodUnitProperty;

        public ETimeIntervalUnits TimePeriodUnit
        {
            get => (ETimeIntervalUnits)GetValue(TimePeriodUnitProperty);
            set => SetValue(TimePeriodUnitProperty, value);
        }

        private static void OnDependencyPropChanged_TimePeriodUnit(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TimeRangeCtrl ctrl = sender as TimeRangeCtrl;
            if(ctrl == null) return;
            ETimeIntervalUnits oldValue = (ETimeIntervalUnits)e.OldValue;
            ETimeIntervalUnits newValue = (ETimeIntervalUnits)e.NewValue;
            RoutedPropertyChangedEventArgs<ETimeIntervalUnits> args = new RoutedPropertyChangedEventArgs<ETimeIntervalUnits>(oldValue, newValue);
            args.RoutedEvent = TimeRangeCtrl.TimePeriodUnitChangedEvent;
            ctrl.UpdateTimeRange();
            ctrl.RaiseEvent(args);
        }

        public static readonly RoutedEvent TimePeriodUnitChangedEvent;

        public event RoutedPropertyChangedEventHandler<ETimeIntervalUnits> TimePeriodUnitChanged
        {
            add => AddHandler(TimePeriodUnitChangedEvent, value);
            remove => RemoveHandler(TimePeriodUnitChangedEvent, value);
        }
        #endregion ————— TimeSpanUnit


        #region ————— TimeRange ———————————————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty TimeRangeStartProperty;

        public DateTime? TimeRangeStart
        {
            get => (DateTime?)GetValue(TimeRangeStartProperty);
            private set => SetValue(TimeRangeStartProperty, value);
        }


        public static DependencyProperty TimeRangeEndProperty;

        public DateTime? TimeRangeEnd
        {
            get => (DateTime?)GetValue(TimeRangeEndProperty);
            private set => SetValue(TimeRangeEndProperty, value);
        }

        public static DependencyProperty TimeRangeProperty;

        public TimeSpan TimeRange
        {
            get => (TimeSpan)GetValue(TimeRangeProperty);
            private set => SetValue(TimeRangeProperty, value);
        }

        public static DependencyProperty TimeRangeIsFixedProperty;

        public Boolean TimeRangeIsFixed
        {
            get => (Boolean)GetValue(TimeRangeIsFixedProperty);
            private set => SetValue(TimeRangeIsFixedProperty, value);
        }
        #endregion ————— TimeRange
    }
    #endregion ■■■■■ Properties & Events



    #region ■■■■■ Other ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class TimeRangeCtrl
    {
        public void UpdateTimeRange()
        {
            ETimeIntervalMode mode = TimeSelectionMode;
            switch (mode)
            {
                case ETimeIntervalMode.FromTimeTillTime:
                    {
                        TimeRangeStart = Time < TimeEnd ? Time : TimeEnd;
                        TimeRangeEnd = TimeEnd > Time ? TimeEnd : Time;
                        TimeRange = (TimeRangeStart - TimeRangeEnd) ?? new TimeSpan();
                        TimeRangeIsFixed = Time != null && TimeEnd != null;
                        break;
                    }
                case ETimeIntervalMode.TimeMinusTimeInterval:
                    {
                        TimeRangeEnd = Time ?? DateTime.Now;
                        TimeRange = GetTimeSpan();
                        TimeRangeStart = TimeRangeEnd - TimeRange;
                        TimeRangeIsFixed = Time != null && TimePeriod > 0;
                        break;
                    }
                case ETimeIntervalMode.TimePlusTimeInterval:
                    {
                        TimeRangeStart = Time;
                        TimeRange = GetTimeSpan();
                        TimeRangeEnd = TimeRangeStart + TimeRange;
                        TimeRangeIsFixed = Time != null && TimePeriod > 0;
                        break;
                    }
                default: throw new InvalidOperationException($"Unexpected value of enum {typeof(ETimeIntervalMode).Name}");
            }

            
        }

        private TimeSpan GetTimeSpan()
        {
            ETimeIntervalUnits unit = TimePeriodUnit;
            Int32 value = TimePeriod;            
            switch(unit)
            {
                case ETimeIntervalUnits.Minutes:
                    return TimeSpan.FromMinutes((Double)value);
                case ETimeIntervalUnits.Hours:
                    return TimeSpan.FromHours((Double)value);
                case ETimeIntervalUnits.Days:
                    return TimeSpan.FromDays((Double)value);
                default: throw new InvalidOperationException($"Unexpected value of enum {typeof(ETimeIntervalUnits).Name}");
            }
        }
    }
    #endregion ■■■■■ Other
}
