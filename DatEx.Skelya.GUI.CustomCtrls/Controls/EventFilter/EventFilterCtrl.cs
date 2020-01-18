using DatEx.Skelya.GUI.CustomCtrls.ViewModel;
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

namespace DatEx.Skelya.GUI.CustomCtrls.Controls
{
    #region ■■■■■ Base ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class EventFilterCtrl : Control
    {
        public EventFilterCtrl()
        {
            CurrentFilter = new VM_FilterInfo();
            CurrentFilter.TimeFrom = DateTime.Now;
            CurrentFilter.TimeTill = DateTime.Now;

        }

        static EventFilterCtrl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EventFilterCtrl), new FrameworkPropertyMetadata(typeof(EventFilterCtrl)));

            #region ————— Dependency property registration ————————————————————————————————————————————————————————————

            CurrentFilterProperty = DependencyProperty.Register(nameof(CurrentFilterProperty), typeof(VM_FilterInfo), typeof(EventFilterCtrl),
                new FrameworkPropertyMetadata(default(VM_FilterInfo), new PropertyChangedCallback(OnDependencyPropChanged_CurrentFilter)));

            #endregion ————— Dependency property registration

            #region ————— Routed events registraiton ——————————————————————————————————————————————————————————————————

            CurrentFilterChangedEvent = EventManager.RegisterRoutedEvent(nameof(CurrentFilterChanged), RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<VM_FilterInfo>), typeof(EventFilterCtrl));

            #endregion ————— Routed events registraiton
        }

        private static void BindCommand(RoutedCommand command, ExecutedRoutedEventHandler executedHandler, CanExecuteRoutedEventHandler canExecuteHandler)
        {
            CommandManager.RegisterClassCommandBinding(typeof(EventFilterCtrl), new CommandBinding(command, executedHandler, canExecuteHandler));
        }
    }
    #endregion ■■■■■ Base



    #region ■■■■■ ControlParts ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class EventFilterCtrl
    {

    }
    #endregion ■■■■■ ControlParts



    #region ■■■■■ Properties ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class EventFilterCtrl
    {
        #region ————— PropNameChanged —————————————————————————————————————————————————————————————————————————————————

        public static DependencyProperty CurrentFilterProperty;

        public VM_FilterInfo CurrentFilter
        {
            get => (VM_FilterInfo)GetValue(CurrentFilterProperty);
            set => SetValue(CurrentFilterProperty, value);
        }

        private static void OnDependencyPropChanged_CurrentFilter(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            EventFilterCtrl ctrl = sender as EventFilterCtrl;
            if (ctrl == null) return;
            VM_FilterInfo oldValue = (VM_FilterInfo)e.OldValue;
            VM_FilterInfo newValue = (VM_FilterInfo)e.NewValue;
            RoutedPropertyChangedEventArgs<VM_FilterInfo> args = new RoutedPropertyChangedEventArgs<VM_FilterInfo>(oldValue, newValue);
            args.RoutedEvent = EventFilterCtrl.CurrentFilterChangedEvent;
            ctrl.RaiseEvent(args);
        }

        #endregion ————— PropNameChanged
    }
    #endregion ■■■■■ ControlParts



    #region ■■■■■ Events ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class EventFilterCtrl
    {
        #region ————— EventNameChanged ————————————————————————————————————————————————————————————————————————————————

        public static readonly RoutedEvent CurrentFilterChangedEvent;

        public event RoutedPropertyChangedEventHandler<VM_FilterInfo> CurrentFilterChanged
        {
            add => AddHandler(CurrentFilterChangedEvent, value);
            remove => RemoveHandler(CurrentFilterChangedEvent, value);
        }

        #endregion ————— EventNameChanged
    }
    #endregion ■■■■■ ControlParts



    #region ■■■■■ Commands ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class EventFilterCtrl
    {
        #region ————— CommandName —————————————————————————————————————————————————————————————————————————————————————
        #endregion ————— CommandName
    }
    #endregion ■■■■■ ControlParts
}
