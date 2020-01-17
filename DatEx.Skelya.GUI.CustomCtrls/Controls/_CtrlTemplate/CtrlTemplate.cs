using DatEx.Skelya.GUI.CustomCtrls.Commands;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace DatEx.Skelya.GUI.CustomCtrls.Controls
{
    #region ■■■■■ Base ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    [TemplatePart(Name = nameof(Part_Name_Ctrl), Type = typeof(ContentControl))]
    public partial class CtrlTemplate : Control
    {
        public CtrlTemplate()
        {
            
        }

        static CtrlTemplate()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CtrlTemplate), new FrameworkPropertyMetadata(typeof(CtrlTemplate)));

            #region ————— Dependency property registration ————————————————————————————————————————————————————————————

            PropNameProperty = DependencyProperty.Register(nameof(Prop), typeof(String), typeof(CtrlTemplate),
                new FrameworkPropertyMetadata(default(String), new PropertyChangedCallback(OnDependencyPropChanged_Prop)));

            #endregion ————— Dependency property registration

            #region ————— Routed events registraiton ——————————————————————————————————————————————————————————————————

            PropChangedEvent = EventManager.RegisterRoutedEvent(nameof(PropChanged), RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventArgs<String>), typeof(CtrlTemplate));


            #endregion ————— Routed events registraiton

            #region ————— Class handlers registraiton —————————————————————————————————————————————————————————————————

            EventManager.RegisterClassHandler(typeof(EventsTreeCtrl), TreeViewItem.ExpandedEvent, new RoutedEventHandler(OnTreeItemExpanded));

            #endregion ————— Class handlers registraiton

            #region ————— Commands registration ———————————————————————————————————————————————————————————————————————

            BindCommand(CtrlTemplateCommands.CommandName, CommandName_Executed, CommandName_CanExecute);

            #endregion ————— Commands registration
        }
        
        private static void BindCommand(RoutedCommand command, ExecutedRoutedEventHandler executedHandler, CanExecuteRoutedEventHandler canExecuteHandler)
        {
            CommandManager.RegisterClassCommandBinding(typeof(CtrlTemplate), new CommandBinding(command, executedHandler, canExecuteHandler));
        }
    }
    #endregion ■■■■■ Base



    #region ■■■■■ ControlParts ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class CtrlTemplate
    {
        private ContentControl Part_Name_Ctrl;

        private T FindTemplatePart<T>(String templatePartName) where T : DependencyObject
            => (GetTemplateChild(templatePartName) as T) ?? throw new NullReferenceException(templatePartName);

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            //
            Part_Name_Ctrl = FindTemplatePart<ContentControl>(nameof(Part_Name_Ctrl));
            //
            SetUpTemplateParts();            
        }

        private void SetUpTemplateParts()
        {
            Part_Name_Ctrl.ToolTip = GetToolTipText(CtrlTemplateCommands.CommandName);
        }

        private String GetToolTipText(RoutedUICommand command)
        {
            KeyGesture keyGesture = command.InputGestures[0] as KeyGesture;
            return keyGesture == null ? command.Text : $"{command.Text} [{keyGesture.GetDisplayStringForCulture(CultureInfo.CurrentCulture)}]";
        }
    }
    #endregion ■■■■■ ControlParts



    #region ■■■■■ Properties ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class CtrlTemplate
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
            CtrlTemplate ctrl = sender as CtrlTemplate;
            if (ctrl == null) return;
            String oldValue = (String)e.OldValue;
            String newValue = (String)e.NewValue;
            RoutedPropertyChangedEventArgs<String> args = new RoutedPropertyChangedEventArgs<String>(oldValue, newValue);
            args.RoutedEvent = CtrlTemplate.PropChangedEvent;
            ctrl.RaiseEvent(args);
        }

        #endregion ————— PropNameChanged
    }
    #endregion ■■■■■ ControlParts



    #region ■■■■■ Events ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class CtrlTemplate
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