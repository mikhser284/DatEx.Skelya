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

namespace DatEx.Skelya.GUI.CustomCtrls
{
    #region ■■■■■ Base ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class EventFilterCtrl : Control
    {
        public EventFilterCtrl()
        {

        }

        static EventFilterCtrl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EventFilterCtrl), new FrameworkPropertyMetadata(typeof(EventFilterCtrl)));

            #region ————— Dependency property registration ————————————————————————————————————————————————————————————

            PropNameProperty = DependencyProperty.Register(nameof(Prop), typeof(String), typeof(EventFilterCtrl),
                new FrameworkPropertyMetadata(default(String), new PropertyChangedCallback(OnDependencyPropChanged_Prop)));

            #endregion ————— Dependency property registration
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

        public static DependencyProperty PropNameProperty;

        public String Prop
        {
            get => (String)GetValue(PropNameProperty);
            set => SetValue(PropNameProperty, value);
        }

        private static void OnDependencyPropChanged_Prop(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            EventFilterCtrl ctrl = sender as EventFilterCtrl;
            if (ctrl == null) return;
            String oldValue = (String)e.OldValue;
            String newValue = (String)e.NewValue;
            RoutedPropertyChangedEventArgs<String> args = new RoutedPropertyChangedEventArgs<String>(oldValue, newValue);
            args.RoutedEvent = EventFilterCtrl.PropChangedEvent;
            ctrl.RaiseEvent(args);
        }

        #endregion ————— PropNameChanged
    }
    #endregion ■■■■■ ControlParts



    #region ■■■■■ Events ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class EventFilterCtrl
    {
        #region ————— EventNameChanged ————————————————————————————————————————————————————————————————————————————————

        public static readonly RoutedEvent PropChangedEvent;

        public event RoutedPropertyChangedEventHandler<String> PropChanged
        {
            add => AddHandler(PropChangedEvent, value);
            remove => RemoveHandler(PropChangedEvent, value);
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
