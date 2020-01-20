using DatEx.Skelya.GUI.CustomCtrls.Commands;
using DatEx.Skelya.GUI.CustomCtrls.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    
    [TemplatePart(Name = nameof(Part_ApplyFilter_Btn), Type = typeof(Button))]
    public partial class EventFilterCtrl : Control
    {
        public EventFilterCtrl()
        {
            //CurrentFilter = new VM_FilterInfo();
            //CurrentFilter.TimeFrom = DateTime.Now;
            //CurrentFilter.TimeTill = DateTime.Now;
        }

        static EventFilterCtrl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EventFilterCtrl), new FrameworkPropertyMetadata(typeof(EventFilterCtrl)));

            #region ————— Local methods ———————————————————————————————————————————————————————————————————————————————            
            static DependencyProperty RegisterProperty<T>(String propName, T defaultValue, Action<DependencyObject, DependencyPropertyChangedEventArgs> propChangedCallback)
                => DependencyProperty.Register(propName, typeof(T), typeof(EventFilterCtrl), new FrameworkPropertyMetadata(defaultValue, new PropertyChangedCallback(propChangedCallback)));

            static RoutedEvent RegisterEvent<T>(String handlerName)
                => EventManager.RegisterRoutedEvent(handlerName, RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<T>), typeof(EventFilterCtrl));

            static void BindCommand(RoutedCommand command, ExecutedRoutedEventHandler executedHandler, CanExecuteRoutedEventHandler canExecuteHandler)
                => CommandManager.RegisterClassCommandBinding(typeof(EventFilterCtrl), new CommandBinding(command, executedHandler, canExecuteHandler));
            #endregion ————— Local methods

            #region ————— Dependency property & routed events registration ————————————————————————————————————————————
            //
            CurrentFilterProperty = RegisterProperty<VM_FilterInfo>(nameof(CurrentFilterProperty), default(VM_FilterInfo), OnDependencyPropChanged_CurrentFilter);
            CurrentFilterChangedEvent = RegisterEvent<VM_FilterInfo>(nameof(CurrentFilterChanged));
            //
            AppliedFilterProperty = RegisterProperty<VM_FilterInfo>(nameof(AppliedFilterProperty), default(VM_FilterInfo), OnDependencyPropChanged_AppliedFilter);
            AppliedFilterChangedEvent = RegisterEvent<VM_FilterInfo>(nameof(AppliedFilterChanged));
            //
            #endregion ————— Dependency property & routed events registration

            #region ————— Commands registration ———————————————————————————————————————————————————————————————————————
            BindCommand(EventFilterCommands.ApplyFilter, ApplyFilter_Executed, ApplyFilter_CanExecute);
            #endregion ————— Commands registration
        }
    }
    #endregion ■■■■■ Base



    #region ■■■■■ ControlParts ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class EventFilterCtrl
    {
        private Button Part_ApplyFilter_Btn;        
        //
        private T FindTemplatePart<T>(String templatePartName) where T : DependencyObject
            => (GetTemplateChild(templatePartName) as T) ?? throw new NullReferenceException(templatePartName);

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            //
            Part_ApplyFilter_Btn = FindTemplatePart<Button>(nameof(Part_ApplyFilter_Btn));            
            //
            SetUpTemplateParts();
        }

        private void SetUpTemplateParts()
        {
            Part_ApplyFilter_Btn.Command = EventFilterCommands.ApplyFilter;
            Part_ApplyFilter_Btn.ToolTip = GetToolTipText(EventFilterCommands.ApplyFilter);
        }

        private String GetToolTipText(RoutedUICommand command)
        {
            KeyGesture keyGesture = command.InputGestures[0] as KeyGesture;
            return keyGesture == null ? command.Text : $"{command.Text} [{keyGesture.GetDisplayStringForCulture(CultureInfo.CurrentCulture)}]";
        }
    }
    #endregion ■■■■■ ControlParts



    #region ■■■■■ Properties & Events ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class EventFilterCtrl
    {
        #region ————— CurrentFilter ———————————————————————————————————————————————————————————————————————————————————

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

        public static readonly RoutedEvent CurrentFilterChangedEvent;

        public event RoutedPropertyChangedEventHandler<VM_FilterInfo> CurrentFilterChanged
        {
            add => AddHandler(CurrentFilterChangedEvent, value);
            remove => RemoveHandler(CurrentFilterChangedEvent, value);
        }
        #endregion ————— CurrentFilter

        #region ————— AppliedFilter ———————————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty AppliedFilterProperty;

        public VM_FilterInfo AppliedFilter
        {
            get => (VM_FilterInfo)GetValue(AppliedFilterProperty);
            private set => SetValue(AppliedFilterProperty, value);
        }

        private static void OnDependencyPropChanged_AppliedFilter(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            EventFilterCtrl ctrl = sender as EventFilterCtrl;
            if (ctrl == null) return;
            VM_FilterInfo oldValue = (VM_FilterInfo)e.OldValue;
            VM_FilterInfo newValue = (VM_FilterInfo)e.NewValue;
            RoutedPropertyChangedEventArgs<VM_FilterInfo> args = new RoutedPropertyChangedEventArgs<VM_FilterInfo>(oldValue, newValue);
            args.RoutedEvent = EventFilterCtrl.AppliedFilterChangedEvent;
            ctrl.RaiseEvent(args);
        }

        public static readonly RoutedEvent AppliedFilterChangedEvent;

        public event RoutedPropertyChangedEventHandler<VM_FilterInfo> AppliedFilterChanged
        {
            add => AddHandler(AppliedFilterChangedEvent, value);
            remove => RemoveHandler(AppliedFilterChangedEvent, value);
        }
        #endregion ————— AppliedFilter
    }
    #endregion ■■■■■ Properties & Events



    #region ■■■■■ Commands ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class EventFilterCtrl
    {
        #region ————— CommandName —————————————————————————————————————————————————————————————————————————————————————
        private static void ApplyFilter_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            EventFilterCtrl ctrl = sender as EventFilterCtrl;
            ctrl.AppliedFilter = ctrl.CurrentFilter;
            MessageBox.Show("Применить фильтр");
        }

        private static void ApplyFilter_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            EventFilterCtrl ctrl = sender as EventFilterCtrl;
            e.CanExecute = ctrl != null && ctrl.CurrentFilter != null;
            e.CanExecute = true;
        }
        #endregion ————— CommandName
    }
    #endregion ■■■■■ Commands
}
