using System;
using System.Collections.Generic;
using System.Globalization;
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
using DatEx.Skelya.GUI.CustomCtrls.Commands;
using DatEx.Skelya.GUI.CustomCtrls.Controls;
using DatEx.Skelya.GUI.CustomCtrls.Helpers;

namespace DatEx.Skelya.GUI.CustomCtrls.Controls
{
    #region ■■■■■ Base ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    [TemplatePart(Name = nameof(Part_FromTime_dPicker), Type = typeof(DatePicker))]
    [TemplatePart(Name = nameof(Part_Mode_cBox), Type = typeof(ComboBox))]
    [TemplatePart(Name = nameof(Part_TimeInterval_tBox), Type = typeof(TextBox))]
    [TemplatePart(Name = nameof(Part_TimeIntervalUnit_cBox), Type = typeof(ComboBox))]
    [TemplatePart(Name = nameof(Part_TillTime_dPicker), Type = typeof(DatePicker))]
    public partial class DateIntervalCtrl : Control
    {
        public DateIntervalCtrl()
        {
            //Mode = EDateIntervalMode.EndTimeMinusTimeInterval;
        }

        static DateIntervalCtrl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DateIntervalCtrl), new FrameworkPropertyMetadata(typeof(DateIntervalCtrl)));

            #region ————— Dependency property registration ————————————————————————————————————————————————————————————

            StartDateProperty = DependencyProperty.Register(nameof(StartDate), typeof(DateTime?), typeof(DateIntervalCtrl),
                new FrameworkPropertyMetadata(default(DateTime?), new PropertyChangedCallback(OnDependencyPropChanged_StartDate)));

            EndDateProperty = DependencyProperty.Register(nameof(EndDate), typeof(DateTime?), typeof(DateIntervalCtrl),
                new FrameworkPropertyMetadata(default(DateTime?), new PropertyChangedCallback(OnDependencyPropChanged_EndDate)));

            ModeProperty = DependencyProperty.Register(nameof(Mode), typeof(EDateIntervalMode), typeof(DateIntervalCtrl),
                new FrameworkPropertyMetadata(EDateIntervalMode.EndTimeMinusTimeInterval, new PropertyChangedCallback(OnDependencyPropChanged_Mode)));

            TimeIntervalProperty = DependencyProperty.Register(nameof(TimeInterval), typeof(Int32?), typeof(DateIntervalCtrl),
                new FrameworkPropertyMetadata(5, new PropertyChangedCallback(OnDependencyPropChanged_TimeInterval)));

            TimeIntervalUnitsProperty = DependencyProperty.Register(nameof(TimeIntervalUnits), typeof(ETimeIntervalUnits), typeof(DateIntervalCtrl),
                new FrameworkPropertyMetadata(ETimeIntervalUnits.Days, new PropertyChangedCallback(OnDependencyPropChanged_TimeIntervalUnits)));

            #endregion ————— Dependency property registration

            #region ————— Routed events registraiton ——————————————————————————————————————————————————————————————————

            StartDateChangedEvent = EventManager.RegisterRoutedEvent(nameof(StartDateChanged), RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<DateTime?>), typeof(DateIntervalCtrl));

            EndDateChangedEvent = EventManager.RegisterRoutedEvent(nameof(EndDateChanged), RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<DateTime?>), typeof(DateIntervalCtrl));

            ModeChangedEvent = EventManager.RegisterRoutedEvent(nameof(ModeChanged), RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<EDateIntervalMode>), typeof(DateIntervalCtrl));

            TimeIntervalChangedEvent = EventManager.RegisterRoutedEvent(nameof(TimeIntervalChanged), RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<Int32?>), typeof(DateIntervalCtrl));

            TimeIntervalUnitsChangedEvent = EventManager.RegisterRoutedEvent(nameof(TimeIntervalUnitsChanged), RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<ETimeIntervalUnits>), typeof(DateIntervalCtrl));

            #endregion ————— Routed events registraiton

            #region ————— Class handlers registraiton —————————————————————————————————————————————————————————————————

            //EventManager.RegisterClassHandler(typeof(DateIntervalCtrl), TreeViewItem.ExpandedEvent, new RoutedEventHandler(OnTreeItemExpanded));

            #endregion ————— Class handlers registraiton

            #region ————— Commands registration ———————————————————————————————————————————————————————————————————————

            BindCommand(CtrlTemplateCommands.CommandName, CommandName_Executed, CommandName_CanExecute);

            #endregion ————— Commands registration
        }

        private static void BindCommand(RoutedCommand command, ExecutedRoutedEventHandler executedHandler, CanExecuteRoutedEventHandler canExecuteHandler)
        {
            CommandManager.RegisterClassCommandBinding(typeof(DateIntervalCtrl), new CommandBinding(command, executedHandler, canExecuteHandler));
        }
    }
    #endregion ■■■■■ Base



    #region ■■■■■ ControlParts ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class DateIntervalCtrl
    {
        private DatePicker Part_FromTime_dPicker;
        private ComboBox Part_Mode_cBox;
        private TextBox Part_TimeInterval_tBox;
        private ComboBox Part_TimeIntervalUnit_cBox;
        private DatePicker Part_TillTime_dPicker;

        private T FindTemplatePart<T>(String templatePartName) where T : DependencyObject
            => (GetTemplateChild(templatePartName) as T) ?? throw new NullReferenceException(templatePartName);

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            //
            Part_FromTime_dPicker = FindTemplatePart<DatePicker>(nameof(Part_FromTime_dPicker));
            Part_Mode_cBox = FindTemplatePart<ComboBox>(nameof(Part_Mode_cBox));
            Part_TimeInterval_tBox = FindTemplatePart<TextBox>(nameof(Part_TimeInterval_tBox));
            Part_TimeIntervalUnit_cBox = FindTemplatePart<ComboBox>(nameof(Part_TimeIntervalUnit_cBox));
            Part_TillTime_dPicker = FindTemplatePart<DatePicker>(nameof(Part_TillTime_dPicker));
            //
            SetUpTemplateParts();
        }

        private void SetUpTemplateParts()
        {
            Binding startTimeBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath(nameof(StartDate)),
                Mode = BindingMode.TwoWay
            };
            BindingOperations.SetBinding(Part_FromTime_dPicker, DatePicker.SelectedDateProperty, startTimeBinding);
            //
            //
            Part_Mode_cBox.ItemsSource = EnumHelper.GetAllValues<EDateIntervalMode>();
            Binding modeBinding = new Binding
            {
                 Source = this,
                 Path = new PropertyPath(nameof(Mode)),
                 Mode = BindingMode.TwoWay
            };
            BindingOperations.SetBinding(Part_Mode_cBox, ComboBox.SelectedValueProperty, modeBinding);
            //
            //
            Binding timeIntervalBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath(nameof(TimeInterval)),
                Mode = BindingMode.TwoWay
            };
            BindingOperations.SetBinding(Part_TimeInterval_tBox, TextBox.TextProperty, timeIntervalBinding);
            //
            //
            Part_TimeIntervalUnit_cBox.ItemsSource = EnumHelper.GetAllValues<ETimeIntervalUnits>();
            Binding timeIntervalUnitBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath(nameof(TimeIntervalUnits)),
                Mode = BindingMode.TwoWay
            };
            BindingOperations.SetBinding(Part_TimeIntervalUnit_cBox, ComboBox.SelectedValueProperty, timeIntervalUnitBinding);
            //
            //
            Binding endTimeBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath(nameof(EndDate)),
                Mode = BindingMode.TwoWay
            };
            BindingOperations.SetBinding(Part_TillTime_dPicker, DatePicker.SelectedDateProperty, endTimeBinding);
        }

        private String GetToolTipText(RoutedUICommand command)
        {
            KeyGesture keyGesture = command.InputGestures[0] as KeyGesture;
            return keyGesture == null ? command.Text : $"{command.Text} [{keyGesture.GetDisplayStringForCulture(CultureInfo.CurrentCulture)}]";
        }
    }
    #endregion ■■■■■ ControlParts



    #region ■■■■■ Properties ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class DateIntervalCtrl
    {
        #region ————— StartDateProperty ———————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty StartDateProperty;

        public DateTime? StartDate
        {
            get => (DateTime?)GetValue(StartDateProperty);
            set => SetValue(StartDateProperty, value);
        }

        private static void OnDependencyPropChanged_StartDate(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            DateIntervalCtrl ctrl = sender as DateIntervalCtrl;
            if(ctrl == null) return;
            DateTime? oldValue = (DateTime?)e.OldValue;
            DateTime? newValue = (DateTime?)e.NewValue;
            RoutedPropertyChangedEventArgs<DateTime?> args = new RoutedPropertyChangedEventArgs<DateTime?>(oldValue, newValue);
            args.RoutedEvent = DateIntervalCtrl.StartDateChangedEvent;
            ctrl.RaiseEvent(args);
        }
        #endregion ————— StartDateProperty

        #region ————— EndDateProperty —————————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty EndDateProperty;

        public DateTime? EndDate
        {
            get => (DateTime?)GetValue(EndDateProperty);
            set => SetValue(EndDateProperty, value);
        }

        private static void OnDependencyPropChanged_EndDate(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            DateIntervalCtrl ctrl = sender as DateIntervalCtrl;
            if(ctrl == null) return;
            DateTime? oldValue = (DateTime?)e.OldValue;
            DateTime? newValue = (DateTime?)e.NewValue;
            RoutedPropertyChangedEventArgs<DateTime?> args = new RoutedPropertyChangedEventArgs<DateTime?>(oldValue, newValue);
            args.RoutedEvent = DateIntervalCtrl.EndDateChangedEvent;
            ctrl.RaiseEvent(args);
        }
        #endregion ————— EndDateProperty

        #region ————— ModeProperty ————————————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty ModeProperty;

        public EDateIntervalMode Mode
        {
            get => (EDateIntervalMode)GetValue(ModeProperty);
            set => SetValue(ModeProperty, value);
        }

        private static void OnDependencyPropChanged_Mode(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            DateIntervalCtrl ctrl = sender as DateIntervalCtrl;
            if(ctrl == null) return;
            EDateIntervalMode oldValue = (EDateIntervalMode)e.OldValue;
            EDateIntervalMode newValue = (EDateIntervalMode)e.NewValue;
            RoutedPropertyChangedEventArgs<EDateIntervalMode> args = new RoutedPropertyChangedEventArgs<EDateIntervalMode>(oldValue, newValue);
            args.RoutedEvent = DateIntervalCtrl.ModeChangedEvent;            
            ChangePartsVisibility(newValue, ctrl);
            ctrl.RaiseEvent(args);

            void ChangePartsVisibility(EDateIntervalMode mode, DateIntervalCtrl ctrl)
            {
                switch( mode)
                {
                    case EDateIntervalMode.EndTimeMinusTimeInterval:
                        {
                            ctrl.Part_TillTime_dPicker.Visibility = Visibility.Collapsed;
                            ctrl.Part_TimeInterval_tBox.Visibility = Visibility.Visible;
                            ctrl.Part_TimeIntervalUnit_cBox.Visibility = Visibility.Visible;
                            break;
                        }
                    case EDateIntervalMode.FromTimeTillTime:
                        {
                            ctrl.Part_TillTime_dPicker.Visibility = Visibility.Visible;
                            ctrl.Part_TimeInterval_tBox.Visibility = Visibility.Collapsed;
                            ctrl.Part_TimeIntervalUnit_cBox.Visibility = Visibility.Collapsed;
                            break;
                        }
                    case EDateIntervalMode.StartTimePlusTimeInterval:
                        {
                            ctrl.Part_TillTime_dPicker.Visibility = Visibility.Collapsed;
                            ctrl.Part_TimeInterval_tBox.Visibility = Visibility.Visible;
                            ctrl.Part_TimeIntervalUnit_cBox.Visibility = Visibility.Visible;
                            break;
                        }
                }
            }
        }
        #endregion ————— ModeProperty

        #region ————— TimeIntervalProperty ————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty TimeIntervalProperty;

        public Int32? TimeInterval
        {
            get => (Int32?)GetValue(TimeIntervalProperty);
            set => SetValue(TimeIntervalProperty, value);
        }

        private static void OnDependencyPropChanged_TimeInterval(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            DateIntervalCtrl ctrl = sender as DateIntervalCtrl;
            if(ctrl == null) return;
            Int32? oldValue = (Int32?)e.OldValue;
            Int32? newValue = (Int32?)e.NewValue;
            RoutedPropertyChangedEventArgs<Int32?> args = new RoutedPropertyChangedEventArgs<Int32?>(oldValue, newValue);
            args.RoutedEvent = DateIntervalCtrl.TimeIntervalChangedEvent;
            ctrl.RaiseEvent(args);
        }
        #endregion ————— TimeIntervalProperty

        #region ————— TimeIntervalUnitsProperty ———————————————————————————————————————————————————————————————————————
        public static DependencyProperty TimeIntervalUnitsProperty;

        public ETimeIntervalUnits TimeIntervalUnits
        {
            get => (ETimeIntervalUnits)GetValue(TimeIntervalUnitsProperty);
            set => SetValue(TimeIntervalUnitsProperty, value);
        }

        private static void OnDependencyPropChanged_TimeIntervalUnits(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            DateIntervalCtrl ctrl = sender as DateIntervalCtrl;
            if(ctrl == null) return;
            ETimeIntervalUnits oldValue = (ETimeIntervalUnits)e.OldValue;
            ETimeIntervalUnits newValue = (ETimeIntervalUnits)e.NewValue;
            RoutedPropertyChangedEventArgs<ETimeIntervalUnits> args = new RoutedPropertyChangedEventArgs<ETimeIntervalUnits>(oldValue, newValue);
            args.RoutedEvent = DateIntervalCtrl.TimeIntervalUnitsChangedEvent;
            ctrl.RaiseEvent(args);
        }
        #endregion ————— TimeIntervalUnitsProperty
    }
    #endregion ■■■■■ ControlParts



    #region ■■■■■ Events ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class DateIntervalCtrl
    {
        #region ————— StartDateChangedEvent ———————————————————————————————————————————————————————————————————————————

        public static readonly RoutedEvent StartDateChangedEvent;

        public event RoutedPropertyChangedEventHandler<DateTime?> StartDateChanged
        {
            add => AddHandler(StartDateChangedEvent, value);
            remove => RemoveHandler(StartDateChangedEvent, value);
        }

        #endregion ————— StartDateChangedEvent

        #region ————— EndDateChangedEvent —————————————————————————————————————————————————————————————————————————————

        public static readonly RoutedEvent EndDateChangedEvent;

        public event RoutedPropertyChangedEventHandler<DateTime?> EndDateChanged
        {
            add => AddHandler(EndDateChangedEvent, value);
            remove => RemoveHandler(EndDateChangedEvent, value);
        }

        #endregion ————— EndDateChangedEvent

        #region ————— ModeChangedEvent ————————————————————————————————————————————————————————————————————————————————

        public static readonly RoutedEvent ModeChangedEvent;

        public event RoutedPropertyChangedEventHandler<EDateIntervalMode> ModeChanged
        {
            add => AddHandler(ModeChangedEvent, value);
            remove => RemoveHandler(ModeChangedEvent, value);
        }

        #endregion ————— ModeChangedEvent

        #region ————— TimeIntervalChangedEvent ————————————————————————————————————————————————————————————————————————

        public static readonly RoutedEvent TimeIntervalChangedEvent;

        public event RoutedPropertyChangedEventHandler<Int32?> TimeIntervalChanged
        {
            add => AddHandler(TimeIntervalChangedEvent, value);
            remove => RemoveHandler(TimeIntervalChangedEvent, value);
        }

        #endregion ————— TimeIntervalChangedEvent

        #region ————— TimeIntervalUnitsChangedEvent ————————————————————————————————————————————————————————————————————————

        public static readonly RoutedEvent TimeIntervalUnitsChangedEvent;

        public event RoutedPropertyChangedEventHandler<ETimeIntervalUnits> TimeIntervalUnitsChanged
        {
            add => AddHandler(TimeIntervalUnitsChangedEvent, value);
            remove => RemoveHandler(TimeIntervalUnitsChangedEvent, value);
        }

        #endregion ————— TimeIntervalUnitsChangedEvent
    }
    #endregion ■■■■■ ControlParts



    #region ■■■■■ Class handlers ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class DateIntervalCtrl
    {
        #region ————— ClassHandler ————————————————————————————————————————————————————————————————————————————————

        //private static void OnTreeItemExpanded(object sender, RoutedEventArgs e)
        //{
        //    TreeViewItem treeViewItem = e.OriginalSource as TreeViewItem;
        //    if(treeViewItem == null) return;
        //    treeViewItem.ItemContainerGenerator.StatusChanged += ItemContainerGenerator_StatusChanged;

        //    void ItemContainerGenerator_StatusChanged(object sender, EventArgs e)
        //    {
        //        ItemContainerGenerator generator = sender as ItemContainerGenerator;
        //        if(generator.Status != GeneratorStatus.ContainersGenerated) return;
        //        Application.Current.Dispatcher.BeginInvoke(new Action(() => MessageBox.Show("Item expanded")));
        //        generator.StatusChanged -= ItemContainerGenerator_StatusChanged;
        //    }
        //}

        #endregion ————— ClassHandler
    }
    #endregion ■■■■■ Class handlers



    #region ■■■■■ Commands ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class DateIntervalCtrl
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
}
