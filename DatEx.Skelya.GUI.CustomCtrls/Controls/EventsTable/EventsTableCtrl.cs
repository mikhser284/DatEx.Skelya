using System;
using System.Collections.Generic;
using System.Data;
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
using DatEx.Skelya.GUI.CustomCtrls.ViewModel;

namespace DatEx.Skelya.GUI.CustomCtrls
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

            #region ————— Dependency property registration ————————————————————————————————————————————————————————————

            FilterProperty = DependencyProperty.Register(nameof(Filter), typeof(VM_FilterInfo), typeof(EventsTableCtrl),
                new FrameworkPropertyMetadata(default(String), new PropertyChangedCallback(OnDependencyPropChanged_Filter)));

            #endregion ————— Dependency property registration

            #region ————— Routed events registraiton ——————————————————————————————————————————————————————————————————

            PropChangedEvent = EventManager.RegisterRoutedEvent(nameof(FilterChanged), RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<VM_FilterInfo>), typeof(EventsTableCtrl));


            #endregion ————— Routed events registraiton

            #region ————— Class handlers registraiton —————————————————————————————————————————————————————————————————

            EventManager.RegisterClassHandler(typeof(EventsTableCtrl), TreeViewItem.ExpandedEvent, new RoutedEventHandler(OnTreeItemExpanded));

            #endregion ————— Class handlers registraiton

            #region ————— Commands registration ———————————————————————————————————————————————————————————————————————

            BindCommand(CtrlTemplateCommands.CommandName, CommandName_Executed, CommandName_CanExecute);

            #endregion ————— Commands registration
        }

        private static void BindCommand(RoutedCommand command, ExecutedRoutedEventHandler executedHandler, CanExecuteRoutedEventHandler canExecuteHandler)
        {
            CommandManager.RegisterClassCommandBinding(typeof(EventsTableCtrl), new CommandBinding(command, executedHandler, canExecuteHandler));
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
            Part_DataTable_dGrid.ToolTip = GetToolTipText(CtrlTemplateCommands.CommandName);
        }

        private String GetToolTipText(RoutedUICommand command)
        {
            KeyGesture keyGesture = command.InputGestures[0] as KeyGesture;
            return keyGesture == null ? command.Text : $"{command.Text} [{keyGesture.GetDisplayStringForCulture(CultureInfo.CurrentCulture)}]";
        }
    }
    #endregion ■■■■■ ControlParts



    #region ■■■■■ Properties ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class EventsTableCtrl
    {
        #region ————— PropNameChanged —————————————————————————————————————————————————————————————————————————————————

        public static DependencyProperty FilterProperty;

        public VM_FilterInfo Filter
        {
            get => (VM_FilterInfo)GetValue(FilterProperty);
            set => SetValue(FilterProperty, value);
        }

        private static void OnDependencyPropChanged_Filter(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            EventsTableCtrl ctrl = sender as EventsTableCtrl;
            if(ctrl == null) return;
            VM_FilterInfo oldValue = (VM_FilterInfo)e.OldValue;
            VM_FilterInfo newValue = (VM_FilterInfo)e.NewValue;
            RoutedPropertyChangedEventArgs<VM_FilterInfo> args = new RoutedPropertyChangedEventArgs<VM_FilterInfo>(oldValue, newValue);
            args.RoutedEvent = EventsTableCtrl.PropChangedEvent;
            ctrl.RaiseEvent(args);
        }

        #endregion ————— PropNameChanged
    }
    #endregion ■■■■■ ControlParts



    #region ■■■■■ Events ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class EventsTableCtrl
    {
        #region ————— EventNameChanged ————————————————————————————————————————————————————————————————————————————————

        public static readonly RoutedEvent PropChangedEvent;

        public event RoutedPropertyChangedEventHandler<VM_FilterInfo> FilterChanged
        {
            add => AddHandler(PropChangedEvent, value);
            remove => RemoveHandler(PropChangedEvent, value);
        }

        #endregion ————— EventNameChanged
    }
    #endregion ■■■■■ ControlParts



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
}
