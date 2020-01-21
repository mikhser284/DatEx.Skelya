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
            TimeIntervalProperty = RegisterProperty<Int32>(nameof(TimeInterval), 1, OnDependencyPropChanged_TimeInterval);
            TimeIntervalChangedEvent = RegisterEvent<Int32>(nameof(TimeIntervalChanged));
            //
            TimeIntervalUnitProperty = RegisterProperty<ETimeIntervalUnit>(nameof(TimeIntervalUnit), ETimeIntervalUnit.Days, OnDependencyPropChanged_TimeIntervalUnit);
            TimeIntervalUnitChangedEvent = RegisterEvent<ETimeIntervalUnit>(nameof(TimeIntervalUnitChanged));
            //
            TimeRangeIntervalProperty = RegisterPropertyWithoutCallback<TimeSpan>(nameof(TimeRangeInterval), TimeSpan.FromDays(1));
            //
            TimeRangeStartProperty = RegisterPropertyWithoutCallback<DateTime?>(nameof(TimeRangeStart), default(DateTime?));
            //
            TimeRangeEndProperty = RegisterPropertyWithoutCallback<DateTime?>(nameof(TimeRangeEnd), default(DateTime?));
            //
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
                Path = new PropertyPath(nameof(TimeInterval)),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            BindingOperations.SetBinding(Part_TimeSpan_tBox, TextBox.TextProperty, timeSpanBinding);
            //
            //
            Part_TimeSpanUnit_cBox.ItemsSource = EnumHelper.GetAllValues<ETimeIntervalUnit>();
            Binding timeSpanUnitBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath(nameof(TimeIntervalUnit)),
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
        public static DependencyProperty TimeIntervalProperty;

        public Int32 TimeInterval
        {
            get => (Int32)GetValue(TimeIntervalProperty);
            set => SetValue(TimeIntervalProperty, value);
        }

        private static void OnDependencyPropChanged_TimeInterval(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TimeRangeCtrl ctrl = sender as TimeRangeCtrl;
            if(ctrl == null) return;
            Int32 oldValue = (Int32)e.OldValue;
            Int32 newValue = (Int32)e.NewValue;
            RoutedPropertyChangedEventArgs<Int32> args = new RoutedPropertyChangedEventArgs<Int32>(oldValue, newValue);
            args.RoutedEvent = TimeRangeCtrl.TimeIntervalChangedEvent;
            ctrl.UpdateTimeRange();
            ctrl.RaiseEvent(args);
        }

        public static readonly RoutedEvent TimeIntervalChangedEvent;

        public event RoutedPropertyChangedEventHandler<Int32> TimeIntervalChanged
        {
            add => AddHandler(TimeIntervalChangedEvent, value);
            remove => RemoveHandler(TimeIntervalChangedEvent, value);
        }
        #endregion ————— TimeSpan

        #region ————— TimeSpanUnit ————————————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty TimeIntervalUnitProperty;

        public ETimeIntervalUnit TimeIntervalUnit
        {
            get => (ETimeIntervalUnit)GetValue(TimeIntervalUnitProperty);
            set => SetValue(TimeIntervalUnitProperty, value);
        }

        private static void OnDependencyPropChanged_TimeIntervalUnit(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TimeRangeCtrl ctrl = sender as TimeRangeCtrl;
            if(ctrl == null) return;
            ETimeIntervalUnit oldValue = (ETimeIntervalUnit)e.OldValue;
            ETimeIntervalUnit newValue = (ETimeIntervalUnit)e.NewValue;
            RoutedPropertyChangedEventArgs<ETimeIntervalUnit> args = new RoutedPropertyChangedEventArgs<ETimeIntervalUnit>(oldValue, newValue);
            args.RoutedEvent = TimeRangeCtrl.TimeIntervalUnitChangedEvent;
            ctrl.UpdateTimeRange();
            ctrl.RaiseEvent(args);
        }

        public static readonly RoutedEvent TimeIntervalUnitChangedEvent;

        public event RoutedPropertyChangedEventHandler<ETimeIntervalUnit> TimeIntervalUnitChanged
        {
            add => AddHandler(TimeIntervalUnitChangedEvent, value);
            remove => RemoveHandler(TimeIntervalUnitChangedEvent, value);
        }
        #endregion ————— TimeSpanUnit

        #region ————— TimeRangeStart ——————————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty TimeRangeStartProperty;

        public DateTime? TimeRangeStart
        {
            get => (DateTime?)GetValue(TimeRangeStartProperty);
            private set => SetValue(TimeRangeStartProperty, value);
        }
        #endregion ————— TimeRangeStart

        #region ————— TimeRangeEnd ————————————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty TimeRangeEndProperty;

        public DateTime? TimeRangeEnd
        {
            get => (DateTime?)GetValue(TimeRangeEndProperty);
            private set => SetValue(TimeRangeEndProperty, value);
        }
        #endregion ————— TimeRangeEnd

        #region ————— TimeRange ———————————————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty TimeRangeIntervalProperty;

        public TimeSpan TimeRangeInterval
        {
            get => (TimeSpan)GetValue(TimeRangeIntervalProperty);
            private set => SetValue(TimeRangeIntervalProperty, value);
        }
        #endregion ————— TimeRange

        #region ————— TimeRangeIsFixed ————————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty TimeRangeIsFixedProperty;

        public Boolean TimeRangeIsFixed
        {
            get => (Boolean)GetValue(TimeRangeIsFixedProperty);
            private set => SetValue(TimeRangeIsFixedProperty, value);
        }
        #endregion ————— TimeRangeIsFixed
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
                        TimeRangeInterval = (TimeRangeStart - TimeRangeEnd) ?? new TimeSpan();
                        TimeRangeIsFixed = Time != null && TimeEnd != null;
                        break;
                    }
                case ETimeIntervalMode.TimeMinusTimeInterval:
                    {
                        TimeRangeEnd = Time ?? DateTime.Now;
                        TimeRangeInterval = GetTimeSpan();
                        TimeRangeStart = TimeRangeEnd - TimeRangeInterval;
                        TimeRangeIsFixed = Time != null && TimeInterval > 0;
                        break;
                    }
                case ETimeIntervalMode.TimePlusTimeInterval:
                    {
                        TimeRangeStart = Time;
                        TimeRangeInterval = GetTimeSpan();
                        TimeRangeEnd = TimeRangeStart + TimeRangeInterval;
                        TimeRangeIsFixed = Time != null && TimeInterval > 0;
                        break;
                    }
                default: throw new InvalidOperationException($"Unexpected value of enum {typeof(ETimeIntervalMode).Name}");
            }

            
        }

        private TimeSpan GetTimeSpan()
        {
            ETimeIntervalUnit unit = TimeIntervalUnit;
            Int32 value = TimeInterval;            
            switch(unit)
            {
                case ETimeIntervalUnit.Minutes:
                    return TimeSpan.FromMinutes((Double)value);
                case ETimeIntervalUnit.Hours:
                    return TimeSpan.FromHours((Double)value);
                case ETimeIntervalUnit.Days:
                    return TimeSpan.FromDays((Double)value);
                default: throw new InvalidOperationException($"Unexpected value of enum {typeof(ETimeIntervalUnit).Name}");
            }
        }
    }
    #endregion ■■■■■ Other
}
