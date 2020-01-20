using DatEx.Skelya.GUI.CustomCtrls.Commands;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace DatEx.Skelya.GUI.CustomCtrls.Controls
{
    #region ■■■■■ Base ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    [TemplatePart(Name = nameof(Part_Value_tBox), Type = typeof(TextBox))]
    [TemplatePart(Name = nameof(Part_ResetValue_Btn), Type = typeof(Button))]
    public partial class CtrlTemplate : Control
    {
        public CtrlTemplate()
        {
            
        }

        static CtrlTemplate()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CtrlTemplate), new FrameworkPropertyMetadata(typeof(CtrlTemplate)));

            #region ————— Local methods ———————————————————————————————————————————————————————————————————————————————
            static DependencyProperty RegisterProperty<T>(String propName, T defaultValue, Action<DependencyObject, DependencyPropertyChangedEventArgs> propChangedCallback)
                => DependencyProperty.Register(propName, typeof(T), typeof(CtrlTemplate), new FrameworkPropertyMetadata(defaultValue, new PropertyChangedCallback(propChangedCallback)));

            static RoutedEvent RegisterEvent<T>(String handlerName)
                => EventManager.RegisterRoutedEvent(handlerName, RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<T>), typeof(CtrlTemplate));

            static void BindCommand(RoutedCommand command, ExecutedRoutedEventHandler executedHandler, CanExecuteRoutedEventHandler canExecuteHandler)
                => CommandManager.RegisterClassCommandBinding(typeof(CtrlTemplate), new CommandBinding(command, executedHandler, canExecuteHandler));
            #endregion ————— Local methods

            #region ————— Dependency property & routed events registration ————————————————————————————————————————————
            //
            ValueProperty = RegisterProperty<String>(nameof(Value), default(String), OnDependencyPropChanged_Value);
            ValueChangedEvent = RegisterEvent<String>(nameof(ValueChanged));
            //
            #endregion ————— Dependency property & routed events registration

            #region ————— Class handlers registraiton —————————————————————————————————————————————————————————————————

            EventManager.RegisterClassHandler(typeof(EventsTreeCtrl), TreeViewItem.ExpandedEvent, new RoutedEventHandler(OnTreeItemExpanded));

            #endregion ————— Class handlers registraiton

            #region ————— Commands registration ———————————————————————————————————————————————————————————————————————

            BindCommand(CtrlTemplateCommands.ResetValue, ResetValue_Executed, ResetValue_CanExecute);

            #endregion ————— Commands registration
        }
    }
    #endregion ■■■■■ Base



    #region ■■■■■ ControlParts ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class CtrlTemplate
    {
        private ContentControl Part_Value_tBox;
        private Button Part_ResetValue_Btn;

        private T FindTemplatePart<T>(String templatePartName) where T : DependencyObject
            => (GetTemplateChild(templatePartName) as T) ?? throw new NullReferenceException(templatePartName);

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            //
            Part_Value_tBox = FindTemplatePart<ContentControl>(nameof(Part_Value_tBox));
            Part_ResetValue_Btn = FindTemplatePart<Button>(nameof(Part_ResetValue_Btn));
            //
            SetUpTemplateParts();            
        }

        private void SetUpTemplateParts()
        {
            Binding valueBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath(nameof(Value)),
                Mode = BindingMode.TwoWay
            };
            BindingOperations.SetBinding(Part_Value_tBox, TextBox.TextProperty, valueBinding);
            //
            //
            Part_ResetValue_Btn.Command = CtrlTemplateCommands.ResetValue;
            Part_ResetValue_Btn.ToolTip = GetToolTipText(CtrlTemplateCommands.ResetValue);
        }

        private String GetToolTipText(RoutedUICommand command)
        {
            KeyGesture keyGesture = command.InputGestures[0] as KeyGesture;
            return keyGesture == null ? command.Text : $"{command.Text} [{keyGesture.GetDisplayStringForCulture(CultureInfo.CurrentCulture)}]";
        }
    }
    #endregion ■■■■■ ControlParts



    #region ■■■■■ Properties & Events ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class CtrlTemplate
    {
        #region ————— Value ———————————————————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty ValueProperty;

        public String Value
        {
            get => (String)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        private static void OnDependencyPropChanged_Value(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            CtrlTemplate ctrl = sender as CtrlTemplate;
            if (ctrl == null) return;
            String oldValue = (String)e.OldValue;
            String newValue = (String)e.NewValue;
            RoutedPropertyChangedEventArgs<String> args = new RoutedPropertyChangedEventArgs<String>(oldValue, newValue);
            args.RoutedEvent = CtrlTemplate.ValueChangedEvent;
            ctrl.RaiseEvent(args);
        }

        public static readonly RoutedEvent ValueChangedEvent;

        public event RoutedPropertyChangedEventHandler<String> ValueChanged
        {
            add => AddHandler(ValueChangedEvent, value);
            remove => RemoveHandler(ValueChangedEvent, value);
        }
        #endregion ————— Value
    }
    #endregion ■■■■■ Properties & Events



    #region ■■■■■ Class handlers ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class CtrlTemplate
    {
        #region ————— ClassHandler ————————————————————————————————————————————————————————————————————————————————

        private static void OnTreeItemExpanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem treeViewItem = e.OriginalSource as TreeViewItem;
            if (treeViewItem == null) return;
            treeViewItem.ItemContainerGenerator.StatusChanged += ItemContainerGenerator_StatusChanged;

            void ItemContainerGenerator_StatusChanged(object sender, EventArgs e)
            {
                ItemContainerGenerator generator = sender as ItemContainerGenerator;
                if (generator.Status != GeneratorStatus.ContainersGenerated) return;
                Application.Current.Dispatcher.BeginInvoke(new Action(() => MessageBox.Show("Item expanded")));
                generator.StatusChanged -= ItemContainerGenerator_StatusChanged;
            }
        }

        #endregion ————— ClassHandler
    }
    #endregion ■■■■■ Class handlers



    #region ■■■■■ Commands ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class CtrlTemplate
    {
        #region ————— ResetValue ——————————————————————————————————————————————————————————————————————————————————————

        private static void ResetValue_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show($"Command {((RoutedUICommand)e.Command).Text} not implemented");
        }

        private static void ResetValue_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
        }

        #endregion ————— ResetValue
    }
    #endregion ■■■■■ Commands
}