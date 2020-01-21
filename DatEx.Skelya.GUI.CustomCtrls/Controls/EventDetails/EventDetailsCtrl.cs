using DatEx.Skelya.GUI.CustomCtrls.Commands;
using DatEx.Skelya.GUI.CustomCtrls.ViewModel;
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
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_EventStatus_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_EventTime_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_EventPlace_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_EventDevice_lbl))]
    [TemplatePart(Type = typeof(TextBlock), Name = nameof(Part_EventDescription_tBlock))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_Shapshot_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_ImagePlaceholder_lbl))]
    [TemplatePart(Type = typeof(Image),     Name = nameof(Part_EventSnapshot_img))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_EventId_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_EventType_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_DeviceId_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_DeviceType_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_DeviceClass_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_SnapshotId_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_SnapshotTime_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_ShapshotSizes_lbl))]
    [TemplatePart(Type = typeof(ListBox),   Name = nameof(Part_AllDevices_lBox))]


    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_edWeight_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_edPrevWeight_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_edTransaction_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_edPrevTransaction_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_edTransactionDataCount_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_edStable_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_edDuration_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_edTransactionMaxWeight_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_edPerimetr_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_edDriverInCar_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_edStatus_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_edReader_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_edTruck1_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_edTruck2_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_edItemName_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_edOperationName_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_edOparationType_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_edOn_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_edWaybill_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_edTransactionWaybill_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_edState_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_Weight1_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_edWeight2_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_edWeghtCenter_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_edCommand_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_edUser_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_edId_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_edEPC_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_edAccessLevel_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_edAmbientTemperature_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_edAnalysisCounter_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_edSubsampleId_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_edSubsampleNrOfSubsamples_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_edSampleHash_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_edProtDM_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_edMoisture_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_edOilDM_lbl))]
    [TemplatePart(Type = typeof(Label),     Name = nameof(Part_dmStarchDM_lbl))]
    public partial class EventDetailsCtrl : Control
    {
        public EventDetailsCtrl()
        {

        }

        static EventDetailsCtrl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EventDetailsCtrl), new FrameworkPropertyMetadata(typeof(EventDetailsCtrl)));

            #region ————— Local methods ———————————————————————————————————————————————————————————————————————————————
            static DependencyProperty RegisterProperty<T>(String propName, T defaultValue, Action<DependencyObject, DependencyPropertyChangedEventArgs> propChangedCallback)
                => DependencyProperty.Register(propName, typeof(T), typeof(EventDetailsCtrl), new FrameworkPropertyMetadata(defaultValue, new PropertyChangedCallback(propChangedCallback)));

            static RoutedEvent RegisterEvent<T>(String handlerName)
                => EventManager.RegisterRoutedEvent(handlerName, RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<T>), typeof(EventDetailsCtrl));

            static void BindCommand(RoutedCommand command, ExecutedRoutedEventHandler executedHandler, CanExecuteRoutedEventHandler canExecuteHandler)
                => CommandManager.RegisterClassCommandBinding(typeof(EventDetailsCtrl), new CommandBinding(command, executedHandler, canExecuteHandler));
            #endregion ————— Local methods

            #region ————— Dependency property & routed events registration ————————————————————————————————————————————
            //
            EventRecordProperty = RegisterProperty<VM_EventLogRecord>(nameof(EventRecord), default(VM_EventLogRecord), OnDependencyPropChanged_EventRecord);
            EventRecordChangedEvent = RegisterEvent<VM_EventLogRecord>(nameof(EventRecordChanged));
            //
            #endregion ————— Dependency property & routed events registration

            #region ————— Class handlers registraiton —————————————————————————————————————————————————————————————————

            EventManager.RegisterClassHandler(typeof(EventsTreeCtrl), TreeViewItem.ExpandedEvent, new RoutedEventHandler(OnTreeItemExpanded));

            #endregion ————— Class handlers registraiton
        }
    }
    #endregion ■■■■■ Base



    #region ■■■■■ ControlParts ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class EventDetailsCtrl
    {
        private Label Part_EventStatus_lbl; //■
        private Label Part_EventTime_lbl; //■
        private Label Part_EventPlace_lbl; //■
        private Label Part_EventDevice_lbl; //■
        private TextBlock Part_EventDescription_tBlock; //■
        private Label Part_Shapshot_lbl;
        private Label Part_ImagePlaceholder_lbl;
        private Image Part_EventSnapshot_img;
        private Label Part_EventId_lbl;
        private Label Part_EventType_lbl;
        private Label Part_DeviceId_lbl;
        private Label Part_DeviceType_lbl;
        private Label Part_DeviceClass_lbl;
        private Label Part_SnapshotId_lbl;
        private Label Part_SnapshotTime_lbl;
        private Label Part_ShapshotSizes_lbl;
        private ListBox Part_AllDevices_lBox;
        private Label Part_edWeight_lbl;
        private Label Part_edPrevWeight_lbl;
        private Label Part_edTransaction_lbl;
        private Label Part_edPrevTransaction_lbl;
        private Label Part_edTransactionDataCount_lbl;
        private Label Part_edStable_lbl;
        private Label Part_edDuration_lbl;
        private Label Part_edTransactionMaxWeight_lbl;
        private Label Part_edPerimetr_lbl;
        private Label Part_edDriverInCar_lbl;
        private Label Part_edStatus_lbl;
        private Label Part_edReader_lbl;
        private Label Part_edTruck1_lbl;
        private Label Part_edTruck2_lbl;
        private Label Part_edItemName_lbl;
        private Label Part_edOperationName_lbl;
        private Label Part_edOparationType_lbl;
        private Label Part_edOn_lbl;
        private Label Part_edWaybill_lbl;
        private Label Part_edTransactionWaybill_lbl;
        private Label Part_edState_lbl;
        private Label Part_Weight1_lbl;
        private Label Part_edWeight2_lbl;
        private Label Part_edWeghtCenter_lbl;
        private Label Part_edCommand_lbl;
        private Label Part_edUser_lbl;
        private Label Part_edId_lbl;
        private Label Part_edEPC_lbl;
        private Label Part_edAccessLevel_lbl;
        private Label Part_edAmbientTemperature_lbl;
        private Label Part_edAnalysisCounter_lbl;
        private Label Part_edSubsampleId_lbl;
        private Label Part_edSubsampleNrOfSubsamples_lbl;
        private Label Part_edSampleHash_lbl;
        private Label Part_edProtDM_lbl;
        private Label Part_edMoisture_lbl;
        private Label Part_edOilDM_lbl;
        private Label Part_dmStarchDM_lbl;

        private T FindTemplatePart<T>(String templatePartName) where T : DependencyObject
            => (GetTemplateChild(templatePartName) as T) ?? throw new NullReferenceException(templatePartName);

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            //
            Part_EventStatus_lbl = FindTemplatePart<Label>(nameof(Part_EventStatus_lbl));
            Part_EventTime_lbl = FindTemplatePart<Label>(nameof(Part_EventTime_lbl));
            Part_EventPlace_lbl = FindTemplatePart<Label>(nameof(Part_EventPlace_lbl));
            Part_EventDevice_lbl = FindTemplatePart<Label>(nameof(Part_EventDevice_lbl));
            Part_EventDescription_tBlock = FindTemplatePart<TextBlock>(nameof(Part_EventDescription_tBlock));
            Part_Shapshot_lbl = FindTemplatePart<Label>(nameof(Part_Shapshot_lbl));
            Part_ImagePlaceholder_lbl = FindTemplatePart<Label>(nameof(Part_ImagePlaceholder_lbl));
            Part_EventSnapshot_img = FindTemplatePart<Image>(nameof(Part_EventSnapshot_img));
            Part_EventId_lbl = FindTemplatePart<Label>(nameof(Part_EventId_lbl));
            Part_EventType_lbl = FindTemplatePart<Label>(nameof(Part_EventType_lbl));
            Part_DeviceId_lbl = FindTemplatePart<Label>(nameof(Part_DeviceId_lbl));
            Part_DeviceType_lbl = FindTemplatePart<Label>(nameof(Part_DeviceType_lbl));
            Part_DeviceClass_lbl = FindTemplatePart<Label>(nameof(Part_DeviceClass_lbl));
            Part_SnapshotId_lbl = FindTemplatePart<Label>(nameof(Part_SnapshotId_lbl));
            Part_SnapshotTime_lbl = FindTemplatePart<Label>(nameof(Part_SnapshotTime_lbl));
            Part_ShapshotSizes_lbl = FindTemplatePart<Label>(nameof(Part_ShapshotSizes_lbl));
            Part_AllDevices_lBox = FindTemplatePart<ListBox>(nameof(Part_AllDevices_lBox));
            Part_edWeight_lbl = FindTemplatePart<Label>(nameof(Part_edWeight_lbl));
            Part_edPrevWeight_lbl = FindTemplatePart<Label>(nameof(Part_edPrevWeight_lbl));
            Part_edTransaction_lbl = FindTemplatePart<Label>(nameof(Part_edTransaction_lbl));
            Part_edPrevTransaction_lbl = FindTemplatePart<Label>(nameof(Part_edPrevTransaction_lbl));
            Part_edTransactionDataCount_lbl = FindTemplatePart<Label>(nameof(Part_edTransactionDataCount_lbl));
            Part_edStable_lbl = FindTemplatePart<Label>(nameof(Part_edStable_lbl));
            Part_edDuration_lbl = FindTemplatePart<Label>(nameof(Part_edDuration_lbl));
            Part_edTransactionMaxWeight_lbl = FindTemplatePart<Label>(nameof(Part_edTransactionMaxWeight_lbl));
            Part_edPerimetr_lbl = FindTemplatePart<Label>(nameof(Part_edPerimetr_lbl));
            Part_edDriverInCar_lbl = FindTemplatePart<Label>(nameof(Part_edDriverInCar_lbl));
            Part_edStatus_lbl = FindTemplatePart<Label>(nameof(Part_edStatus_lbl));
            Part_edReader_lbl = FindTemplatePart<Label>(nameof(Part_edReader_lbl));
            Part_edTruck1_lbl = FindTemplatePart<Label>(nameof(Part_edTruck1_lbl));
            Part_edTruck2_lbl = FindTemplatePart<Label>(nameof(Part_edTruck2_lbl));
            Part_edItemName_lbl = FindTemplatePart<Label>(nameof(Part_edItemName_lbl));
            Part_edOperationName_lbl = FindTemplatePart<Label>(nameof(Part_edOperationName_lbl));
            Part_edOparationType_lbl = FindTemplatePart<Label>(nameof(Part_edOparationType_lbl));
            Part_edOn_lbl = FindTemplatePart<Label>(nameof(Part_edOn_lbl));
            Part_edWaybill_lbl = FindTemplatePart<Label>(nameof(Part_edWaybill_lbl));
            Part_edTransactionWaybill_lbl = FindTemplatePart<Label>(nameof(Part_edTransactionWaybill_lbl));
            Part_edState_lbl = FindTemplatePart<Label>(nameof(Part_edState_lbl));
            Part_Weight1_lbl = FindTemplatePart<Label>(nameof(Part_Weight1_lbl));
            Part_edWeight2_lbl = FindTemplatePart<Label>(nameof(Part_edWeight2_lbl));
            Part_edWeghtCenter_lbl = FindTemplatePart<Label>(nameof(Part_edWeghtCenter_lbl));
            Part_edCommand_lbl = FindTemplatePart<Label>(nameof(Part_edCommand_lbl));
            Part_edUser_lbl = FindTemplatePart<Label>(nameof(Part_edUser_lbl));
            Part_edId_lbl = FindTemplatePart<Label>(nameof(Part_edId_lbl));
            Part_edEPC_lbl = FindTemplatePart<Label>(nameof(Part_edEPC_lbl));
            Part_edAccessLevel_lbl = FindTemplatePart<Label>(nameof(Part_edAccessLevel_lbl));
            Part_edAmbientTemperature_lbl = FindTemplatePart<Label>(nameof(Part_edAmbientTemperature_lbl));
            Part_edAnalysisCounter_lbl = FindTemplatePart<Label>(nameof(Part_edAnalysisCounter_lbl));
            Part_edSubsampleId_lbl = FindTemplatePart<Label>(nameof(Part_edSubsampleId_lbl));
            Part_edSubsampleNrOfSubsamples_lbl = FindTemplatePart<Label>(nameof(Part_edSubsampleNrOfSubsamples_lbl));
            Part_edSampleHash_lbl = FindTemplatePart<Label>(nameof(Part_edSampleHash_lbl));
            Part_edProtDM_lbl = FindTemplatePart<Label>(nameof(Part_edProtDM_lbl));
            Part_edMoisture_lbl = FindTemplatePart<Label>(nameof(Part_edMoisture_lbl));
            Part_edOilDM_lbl = FindTemplatePart<Label>(nameof(Part_edOilDM_lbl));
            Part_dmStarchDM_lbl = FindTemplatePart<Label>(nameof(Part_dmStarchDM_lbl));
            //
            SetUpTemplateParts();
        }

        private void SetUpTemplateParts()
        {
            Binding eventStatusBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.EventCriticality)}"),
                Mode = BindingMode.OneWay
                //TODO change EventCriticality type to Enum
                //TODO for view use diferent Icon, Label, and Color
            };
            BindingOperations.SetBinding(Part_EventStatus_lbl, Label.ContentProperty, eventStatusBinding);
            //
            //
            //
            Binding eventTimeBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.EventTime)}"),
                Converter = new ValConverter_DateTime_String(),
                ConverterParameter = "yyyy.MM.dd-ddd HH:mm:ss",
                ConverterCulture = new CultureInfo("ru-RU"),
                Mode = BindingMode.OneWay
            };
            BindingOperations.SetBinding(Part_EventTime_lbl, Label.ContentProperty, eventTimeBinding);
            //
            //
            //
            Binding eventPlaceBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.DataSectorName)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_EventPlace_lbl, Label.ContentProperty, eventPlaceBinding);
            //
            //
            //
            Binding eventDeviceBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.DeviceName)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_EventDevice_lbl, Label.ContentProperty, eventDeviceBinding);
            //
            //
            //            
            Binding eventDescriptionBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath($"{nameof(EventRecord)}.{nameof(EventRecord.EventDescription)}"),
                Mode = BindingMode.OneWay,
            };
            BindingOperations.SetBinding(Part_EventDescription_tBlock, TextBlock.TextProperty, eventDescriptionBinding);
        }

        private String GetToolTipText(RoutedUICommand command)
        {
            KeyGesture keyGesture = command.InputGestures[0] as KeyGesture;
            return keyGesture == null ? command.Text : $"{command.Text} [{keyGesture.GetDisplayStringForCulture(CultureInfo.CurrentCulture)}]";
        }
    }
    #endregion ■■■■■ ControlParts



    #region ■■■■■ Properties & Events ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class EventDetailsCtrl
    {
        #region ————— EventRecord ———————————————————————————————————————————————————————————————————————————————————————————
        public static DependencyProperty EventRecordProperty;

        public VM_EventLogRecord EventRecord
        {
            get => (VM_EventLogRecord)GetValue(EventRecordProperty);
            set => SetValue(EventRecordProperty, value);
        }

        private static void OnDependencyPropChanged_EventRecord(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            EventDetailsCtrl ctrl = sender as EventDetailsCtrl;
            if(ctrl == null) return;
            VM_EventLogRecord oldValue = (VM_EventLogRecord)e.OldValue;
            VM_EventLogRecord newValue = (VM_EventLogRecord)e.NewValue;
            RoutedPropertyChangedEventArgs<VM_EventLogRecord> args = new RoutedPropertyChangedEventArgs<VM_EventLogRecord>(oldValue, newValue);
            args.RoutedEvent = EventDetailsCtrl.EventRecordChangedEvent;
            ctrl.RaiseEvent(args);
        }

        public static readonly RoutedEvent EventRecordChangedEvent;

        public event RoutedPropertyChangedEventHandler<VM_EventLogRecord> EventRecordChanged
        {
            add => AddHandler(EventRecordChangedEvent, value);
            remove => RemoveHandler(EventRecordChangedEvent, value);
        }
        #endregion ————— EventRecord
    }
    #endregion ■■■■■ Properties & Events



    #region ■■■■■ Class handlers ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class EventDetailsCtrl
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
    public partial class EventDetailsCtrl
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